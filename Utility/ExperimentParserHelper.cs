using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserCore;

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

    public class ExperimentParserHelper
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
            "S -> while ( C ) S",
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

        private bool slrContextLoaded = false, lr1ContextLoaded = false;
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

        public bool ContextLoaded
        {
            get { return slrContextLoaded && lr1ContextLoaded; }
        }

        public bool SLRContextLoaded
        {
            get { return slrContextLoaded; }
        }

        public bool LR1ContextLoaded
        {
            get { return lr1ContextLoaded; }
        }

        public ExperimentParserHelper(Form owner, IReportParseStep reporter = null)
        {
            this.owner = owner;
            this.stepReporter = reporter;
        }

        public void CreateParserFromContext(bool slr, bool lr1)
        {
            if (slr && File.Exists(SLRParserContextFileName))
            {
                using (var stream = new FileStream(SLRParserContextFileName, FileMode.Open))
                {
                    slrParser = Parser.CreateFromContext(stream, symbolTable, new ExperimentGrammarPanicErrorRoutine(), stepReporter) as Parser<LR0Item>;
                    stream.Close();

                    slrContextLoaded = true;
                }
            }

            if (lr1 && File.Exists(LR1ParserContextFileName))
            {
                using (var stream = new FileStream(LR1ParserContextFileName, FileMode.Open))
                {
                    lr1Parser = Parser.CreateFromContext(stream, symbolTable, new ExperimentGrammarPanicErrorRoutine(), stepReporter) as Parser<LR1Item>;
                    stream.Close();

                    lr1ContextLoaded = true;
                }
            }
        }

        public void CreateParserFromGrammar(bool genSLR, bool genLR1)
        {
            // At least one should be generated.
            if (!genSLR && !genLR1)
                return;

            var grammarStream = new MemoryStream();
            using (var writer = new StreamWriter(grammarStream, new UTF8Encoding(), bufferSize: 1024, leaveOpen: true))
            {
                foreach (var prod in Grammar)
                    writer.WriteLine(prod);
                writer.Flush();
            }

            // Show waiting form
            var waitingForm = new GenerateWaitingForm();
            if (!genSLR) waitingForm.DisableSLR();
            if (!genLR1) waitingForm.DisableLR1();
            var showFormTask = Task.Run(() => { owner.Invoke((MethodInvoker)delegate { waitingForm.ShowDialog(owner); }); });

            // Generate Contexts
            var slrTask = new Task(() =>
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

                slrContextLoaded = true;

                owner.Invoke((MethodInvoker)delegate
                {
                    waitingForm.lblSLRProcess.Text = "Done";
                    waitingForm.lblSLRProcess.ForeColor = Color.Green;
                });
            });

            var lr1Task = new Task(() =>
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

                lr1ContextLoaded = true;

                owner.Invoke((MethodInvoker)delegate
                {
                    waitingForm.lblLR1Process.Text = "Done";
                    waitingForm.lblLR1Process.ForeColor = Color.Green;
                });
            });

            Task.Run(() => { if (genSLR) slrTask.RunSynchronously(); });
            Task.Run(() => { if (genLR1) lr1Task.RunSynchronously(); });
            Task.Run(() =>
            {
                var genTask = new List<Task>();
                if (genSLR) genTask.Add(slrTask);
                if (genLR1) genTask.Add(lr1Task);
                
                // Generally, slrTask is faster than lr1Task
                Task.WaitAll(genTask.ToArray());

                // Cleanup
                grammarStream.Close();
                owner.Invoke((MethodInvoker)delegate 
                {
                    waitingForm.PermitClose = true;
                    waitingForm.Close();
                });
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

                        // For ACTION[i, a] = reduce by "S -> if ( C ) S", and a = "else", choose shift
                        var reduce = pendAction.Value[term].Entries.Where(e => e.Type == ActionTableEntry.ActionType.Reduce).Single();
                        if (reduce.ReduceProduction.ToString() == "S -> if ( C ) S" && term.ToString() == "else")
                            pendAction.Value[term].SetPreferEntry(
                                pendAction.Value[term].Entries.Where(e => e.Type == ActionTableEntry.ActionType.Shift).Single()
                            );
                            
                        // Otherwise, we choose reduce
                        if (!pendAction.Value[term].PreferedEntrySpecified)
                            pendAction.Value[term].SetPreferEntry(reduce);

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
