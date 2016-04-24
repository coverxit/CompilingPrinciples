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

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserModule;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.GUIParser
{
    public partial class ParserForm : Form
    {
        private const int ErrorIndicatorIndex = 8;

        private ParserHelper parserHelper;

        public ParserForm()
        {
            InitializeComponent();

            // Show Line Number
            textCode.Margins[0].Width = 36;

            textCode.StyleResetDefault();
            textCode.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            textCode.Styles[ScintillaNET.Style.Default].Size = 10;
            textCode.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            textCode.Styles[ScintillaNET.Style.Cpp.Default].ForeColor = Color.Silver;
            textCode.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            textCode.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            textCode.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            textCode.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = Color.Olive;
            textCode.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = Color.Blue;
            textCode.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = Color.Blue;
            textCode.Styles[ScintillaNET.Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            textCode.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            textCode.Styles[ScintillaNET.Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            textCode.Styles[ScintillaNET.Style.Cpp.StringEol].BackColor = Color.Pink;
            textCode.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = Color.Purple;
            textCode.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = Color.Maroon;

            // Set Keywords
            textCode.SetKeywords(0, "if then else while do");
            textCode.SetKeywords(1, "int float");

            // Set Indicator
            textCode.Indicators[ErrorIndicatorIndex].Style = ScintillaNET.IndicatorStyle.Squiggle;
            textCode.Indicators[ErrorIndicatorIndex].HoverStyle = ScintillaNET.IndicatorStyle.CompositionThick;
            textCode.Indicators[ErrorIndicatorIndex].ForeColor = Color.Red;

            parserHelper = new ParserHelper(this, new ParseStepReporter(this, listParse));
            parserHelper.CreateParserFromContext();
        }

        private void ParserForm_Shown(object sender, EventArgs e)
        {
            if (!parserHelper.CotextLoaded)
                parserHelper.CreateParserFromGrammar();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                string fileName = openFileDialog.FileName;
                textCode.Text = File.ReadAllText(fileName);

                btnAnalyze.Focus();
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            listParse.Items.Clear();
            listSymbolTable.Items.Clear();

            textCode.IndicatorCurrent = ErrorIndicatorIndex;
            textCode.IndicatorClearRange(0, textCode.Text.Length);

            var inputArray = Encoding.ASCII.GetBytes(textCode.Text);
            var useSLRParser = !rbLR1.Checked;

            var analyseTask = new Task(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listParse.BeginUpdate();

                    if (useSLRParser)
                        parserHelper.SLRParser.Parse(new MemoryStream(inputArray));
                    else
                        parserHelper.LR1Parser.Parse(new MemoryStream(inputArray));

                    if (listParse.Items.Count > 0)
                        listParse.EnsureVisible(listParse.Items.Count - 1);

                    listParse.EndUpdate();

                    var newlinePos = new List<int>();
                    newlinePos.Add(0);
                    newlinePos.AddRange(inputArray.Select((value, index) => new { index, value }).Where(el => el.value == '\n').Select(el => el.index));
                    newlinePos.Add(inputArray.Count());

                    textCode.IndicatorCurrent = ErrorIndicatorIndex;
                    foreach (var l in useSLRParser ? parserHelper.SLRParser.ErrorLines : parserHelper.LR1Parser.ErrorLines)
                        textCode.IndicatorFillRange(newlinePos[l - 1], newlinePos[l] - newlinePos[l - 1]);
                });
            });

            analyseTask.ContinueWith((lastTask) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listSymbolTable.BeginUpdate();
                    foreach (var s in parserHelper.SymbolTable.ToList().Select((value, index) => new { index, value }))
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = s.index.ToString();
                        item.SubItems.Add(s.value.Lexeme);

                        if (s.value.Tag != LexerModule.Tag.Identifier)
                            item.Group = listSymbolTable.Groups["lvGroupKeyword"];
                        else
                            item.Group = listSymbolTable.Groups["lvGroupIdentifier"];

                        listSymbolTable.Items.Add(item);
                    }
                    listSymbolTable.EndUpdate();
                });
            });

            analyseTask.Start();
        }

        /*
        private void textCode_KeyUp(object sender, KeyEventArgs e)
        {
            btnAnalyze_Click(sender, e);
        }
        */
    }

    public class ParseStepReporter : IReportParseStep
    {
        private Form owner;
        private ListView lvParseStep;

        public ParseStepReporter(Form owner, ListView lvParseStep)
        {
            this.owner = owner;
            this.lvParseStep = lvParseStep;
        }

        public void ReportStep(bool error, string action, string symbol, string state)
        {
            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = state;
            lvItem.UseItemStyleForSubItems = false;

            lvItem.SubItems.Add(symbol);
            lvItem.SubItems.Add(action);

            if (error)
                lvItem.SubItems[2].ForeColor = Color.Red;

            owner.Invoke((MethodInvoker)delegate { lvParseStep.Items.Add(lvItem); });
        }
    }
}
