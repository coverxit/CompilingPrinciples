using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserModule;

namespace CompilingPrinciples.Utility
{
    public class ProgressReporter : IReportProgress
    {
        private Label label;
        private Form owner;

        public ProgressReporter(Form owner, Label label)
        {
            this.owner = owner;
            this.label = label;
        }
        
        public void ReportProgress(string progress)
        {
            owner.Invoke((MethodInvoker)delegate { label.Text = progress; }); 
        }
    }

    public class ParserHelper
    {
        private static readonly string[] Grammar =
        {
            "P' -> P",
            "P -> D S",

            "D -> L id ; D",
            "D -> @",

            "L -> int",
            "L -> float",

            "S -> id = E ;",
            "S -> if ( C ) S",
            "S -> if ( C ) S else S",
            "S -> while ( C ) S do S",
            "S -> S S",

            "C -> E > E",
            "C -> E < E",
            "C -> E == E",
            "C -> E >= E",
            "C -> E <= E",
            "C -> E != E",

            "E -> E + T",
            "E -> E - T",
            "E -> T",

            "T -> F",
            "T -> T * F",
            "T -> T / F",

            "F -> ( E )",
            "F -> id",
            "F -> decimal"
        };

        private const string SLRParserContextFileName = "SLRParserContext.ctx";
        private const string LR1ParserContextFileName = "LR1ParserContext.ctx";

        private SymbolTable symbolTable = new SymbolTable();
        private IReportParseStep stepReporter;

        private Parser<LR0Item> slrParser;
        private Parser<LR1Item> lr1Parser;

        private bool contextLoaded = false;
        private Form owner;

        public SymbolTable SymbolTable
        {
            get { return symbolTable; }
        }

        public Parser<LR0Item> SLRParser
        {
            get { return slrParser; }
        }

        public Parser<LR1Item> LR1Parser
        {
            get { return lr1Parser; }
        }

        public bool CotextLoaded
        {
            get { return contextLoaded; }
        }

        public ParserHelper(Form owner, IReportParseStep reporter = null)
        {
            this.owner = owner;
            this.stepReporter = reporter;
        }

        public void CreateParserFromContext()
        {
            if (File.Exists(SLRParserContextFileName) && File.Exists(LR1ParserContextFileName))
            {
                using (var stream = new FileStream(SLRParserContextFileName, FileMode.Open))
                {
                    slrParser = Parser.CreateFromContext(stream, symbolTable, new ExperimentGrammarPanicErrorRoutine(), stepReporter) as Parser<LR0Item>;
                    stream.Close();
                }

                using (var stream = new FileStream(LR1ParserContextFileName, FileMode.Open))
                {
                    lr1Parser = Parser.CreateFromContext(stream, symbolTable, new ExperimentGrammarPanicErrorRoutine(), stepReporter) as Parser<LR1Item>;
                    stream.Close();
                }

                contextLoaded = true;
            }
        }

        public void CreateParserFromGrammar()
        {
            var grammarStream = new MemoryStream();
            using (var writer = new StreamWriter(grammarStream, new UTF8Encoding(), bufferSize: 1024, leaveOpen: true))
            {
                foreach (var prod in Grammar)
                    writer.WriteLine(prod);
                writer.Flush();
            }

            // Show waiting form
            var waitingForm = new GenerateWaitingForm();
            var showFormTask = Task.Run(() => { owner.Invoke((MethodInvoker)delegate { waitingForm.ShowDialog(owner); }); });

            // Generate Contexts
            var slrTask = Task.Run(() =>
            {
                var reporter = new ProgressReporter(owner, waitingForm.lblSLRProcess);
                var grammar = new Grammar(symbolTable, reporter);
                using (var streamCopy = new MemoryStream())
                {
                    grammarStream.WriteTo(streamCopy);
                    streamCopy.Position = 0;

                    grammar.Parse(streamCopy);
                }

                var collection = new LR0Collection(grammar, reporter);
                var parseTable = SLRParseTable.Create(collection, reporter);

                DetermineAmbiguousAction(grammar, parseTable, collection);
                slrParser = new Parser<LR0Item>(symbolTable, grammar, parseTable, new ExperimentGrammarPanicErrorRoutine(), stepReporter);

                using (var stream = new FileStream(SLRParserContextFileName, FileMode.Create))
                    slrParser.SaveContext(stream);

                owner.Invoke((MethodInvoker)delegate
                {
                    waitingForm.lblSLRProcess.Text = "Done";
                    waitingForm.lblSLRProcess.ForeColor = Color.Green;
                });
            });

            var lr1Task = Task.Run(() =>
            {
                var reporter = new ProgressReporter(owner, waitingForm.lblLR1Process);
                var grammar = new Grammar(symbolTable, reporter);
                using (var streamCopy = new MemoryStream())
                {
                    grammarStream.WriteTo(streamCopy);
                    streamCopy.Position = 0;

                    grammar.Parse(streamCopy);
                }

                var collection = new LR1Collection(grammar, reporter);
                var parseTable = LR1ParseTable.Create(collection, reporter);

                DetermineAmbiguousAction(grammar, parseTable, collection);
                lr1Parser = new Parser<LR1Item>(symbolTable, grammar, parseTable, new ExperimentGrammarPanicErrorRoutine(), stepReporter);

                using (var stream = new FileStream(LR1ParserContextFileName, FileMode.Create))
                    lr1Parser.SaveContext(stream);

                owner.Invoke((MethodInvoker)delegate
                {
                    waitingForm.lblLR1Process.Text = "Done";
                    waitingForm.lblLR1Process.ForeColor = Color.Green;
                });
            });

            Task.Run(() =>
            {
                // Generally, slrTask is faster than lr1Task
                Task.WaitAll(new Task[] { slrTask, lr1Task });

                // Cleanup
                grammarStream.Close();
                owner.Invoke((MethodInvoker)delegate 
                {
                    waitingForm.PermitClose = true;
                    waitingForm.Close();
                });

                contextLoaded = true;
            });
        }

        private void DetermineAmbiguousAction<T>(Grammar grammar, ParseTable<T> parseTable, LRCollection<T> coll)
            where T : LR0Item
        {
            // var debug = new FileStream("determine.txt", FileMode.Create);
            // var writer = new StreamWriter(debug);

            foreach (var pendAction in parseTable.Action)
                foreach (var term in grammar.TerminalsWithEndMarker)
                    if (pendAction.Value.ContainsKey(term) && !pendAction.Value[term].PreferedEntrySpecified)
                    {
                        // Actually, should be no more than 2 entries
                        if (pendAction.Value[term].Entries.Count > 2)
                            throw new ApplicationException("Pending Action Entries Count Exceeded.");

                        // Try reduce, but no ACTION[i, a] = reduce by "S -> if ( C ) S"
                        var reduce = pendAction.Value[term].Entries.Where(e => e.Type == ActionTableEntry.ActionType.Reduce).Single();
                        if (reduce.ReduceProduction.ToString() != "S -> if ( C ) S")
                            pendAction.Value[term].SetPreferEntry(reduce);
                            
                        // Otherwise, we choose shift
                        if (!pendAction.Value[term].PreferedEntrySpecified)
                            pendAction.Value[term].SetPreferEntry(
                                pendAction.Value[term].Entries.Where(e => e.Type == ActionTableEntry.ActionType.Shift).Single()
                            );

                        // writer.WriteLine("==== I[" + pendAction.Key + "] ===");
                        // foreach (var e in parseTable.Items[pendAction.Key])
                        //    writer.WriteLine(e);
                        // writer.WriteLine("=== Pending for ACTION[" + pendAction.Key + ", " + term + "] ===");
                        // foreach (var e in pendAction.Value[term].Entries)
                        //    writer.WriteLine(e + (e.Equals(pendAction.Value[term].PreferredEntry) ? " [selected]" : ""));
                    }

             // writer.Flush();
             // writer.Close();
        }
    }
}
