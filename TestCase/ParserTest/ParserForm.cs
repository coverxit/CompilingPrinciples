using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

using CompilingPrinciples.LexicalAnalyzer;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.SyntaxAnalyzer;

namespace CompilingPrinciples.TestCase
{
    public partial class ParserForm : Form
    {
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

        private bool isContextLoaded = false;

        public ParserForm()
        {
            InitializeComponent();
        }

        private void ParserForm_Shown(object sender, EventArgs e)
        {
            if (!isContextLoaded)
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
                new Task(() => { this.Invoke((MethodInvoker)delegate { waitingForm.ShowDialog(this); }); }).Start();

                // Generate Contexts
                var slrTask = Task.Run(() =>
                {
                    var streamCopy = new MemoryStream();
                    grammarStream.WriteTo(streamCopy);
                    streamCopy.Position = 0;



                    this.Invoke((MethodInvoker)delegate
                    {
                        waitingForm.lblSLRProcess.Text = "Done";
                        waitingForm.lblSLRProcess.ForeColor = Color.Green;
                    });
                });

                var lr1Task = Task.Run(() =>
                {
                    var streamCopy = new MemoryStream();
                    grammarStream.WriteTo(streamCopy);
                    streamCopy.Position = 0;

                    this.Invoke((MethodInvoker)delegate
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
                    this.Invoke((MethodInvoker)delegate { waitingForm.Dispose(); });
                });
            }
        }

        private void ParserForm_Load(object sender, EventArgs e)
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

                isContextLoaded = true;
            }
        }
    }
}
