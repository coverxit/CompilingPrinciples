using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using CompilingPrinciples.LexicalAnalyzer;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.SyntaxAnalyzer;

namespace CompilingPrinciples.TestCase
{
    public class ParserHelper
    {
        private const string ShiftOnProduction = "S -> if ( C ) S";

        private static readonly string[] Grammar =
        {
            "P' -> P",
            "P -> D S",

            "D -> L id ; D",
            "D -> @",

            "L -> int",
            "L -> float",

            "S -> id = E",
            "S -> if ( C ) S ;",
            "S -> if ( C ) S ; else S",
            "S -> while ( C ) S ; S",
            "S -> do S ; while ( C ) ; S",
            "S -> S ; S",

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

        private Parser<LR0Item> slrParser;
        private Parser<LR1Item> lr1Parser;

        private bool contextLoaded = false;
        private Form owner;

        public ParserHelper(Form owner)
        {
            this.owner = owner;
        }

        public SymbolTable SymbolTable
        {
            get { return new SymbolTable(symbolTable); }
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

        public void CreateParserFromContext()
        {
            if (File.Exists(SLRParserContextFileName) && File.Exists(LR1ParserContextFileName))
            {
                using (var stream = new FileStream(SLRParserContextFileName, FileMode.Open))
                {
                    slrParser = Parser.CreateFromContext(stream, symbolTable, new SLRParserErrorRoutine()) as Parser<LR0Item>;
                    stream.Close();
                }

                using (var stream = new FileStream(LR1ParserContextFileName, FileMode.Open))
                {
                    lr1Parser = Parser.CreateFromContext(stream, symbolTable, new LR1ParserErrorRoutine()) as Parser<LR1Item>;
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
            new Task(() => { owner.Invoke((MethodInvoker)delegate { waitingForm.ShowDialog(owner); }); }).Start();

            // Generate Contexts
            var slrTask = Task.Run(() =>
            {
                var grammar = new Grammar(symbolTable);
                using (var streamCopy = new MemoryStream())
                {
                    grammarStream.WriteTo(streamCopy);
                    streamCopy.Position = 0;

                    grammar.Parse(streamCopy);
                }

                var collection = new LR0Collection(grammar);
                var parseTable = SLRParseTable.Create(collection);

                DetermineAmbiguousAction(grammar, parseTable, collection);
                slrParser = new Parser<LR0Item>(symbolTable, grammar, parseTable, new SLRParserErrorRoutine());

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
                var grammar = new Grammar(symbolTable);
                using (var streamCopy = new MemoryStream())
                {
                    grammarStream.WriteTo(streamCopy);
                    streamCopy.Position = 0;

                    grammar.Parse(streamCopy);
                }

                var collection = new LR1Collection(grammar);
                var parseTable = LR1ParseTable.Create(collection);

                DetermineAmbiguousAction(grammar, parseTable, collection);
                lr1Parser = new Parser<LR1Item>(symbolTable, grammar, parseTable, new SLRParserErrorRoutine());

                using (var stream = new FileStream(SLRParserContextFileName, FileMode.Create))
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
                owner.Invoke((MethodInvoker)delegate { waitingForm.Dispose(); });
            });
        }

        private void DetermineAmbiguousAction<T>(Grammar grammar, ParseTable<T> parseTable, LRCollection<T> coll)
            where T : LR0Item
        {
            foreach (var pendAction in parseTable.Action)
                foreach (var term in grammar.TerminalsWithEndMarker)
                    if (pendAction.Value.ContainsKey(term) && !pendAction.Value[term].PreferedEntrySpecified)
                    {
                        //pendAction.Value[term].Entries;
                    }
        }
    }
}
