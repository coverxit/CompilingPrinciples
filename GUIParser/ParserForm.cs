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
using System.Runtime.Serialization;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserCore;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.GUIParser
{
    public partial class ParserForm : Form
    {
        private const int ErrorIndicatorIndex = 8;

        private ParserGenHelper parserHelper;

        private Parser customParser = null;
        private SymbolTable customSymbolTable = new SymbolTable();

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

            parserHelper = new ParserGenHelper(this, new ParseStepReporter(this, listParse));
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
            Parser parser = null;

            if (rbLR1.Checked)
                parser = parserHelper.LR1Parser;
            else if (rbCustom.Checked)
                parser = customParser;
            else
                parser = parserHelper.SLRParser;

            // failsafe
            if (parser == null)
            {
                rbSLR.Checked = true;
                parser = parserHelper.SLRParser;
            }

            var analyseTask = new Task(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listParse.BeginUpdate();

                    try
                    {
                        parser.Parse(new MemoryStream(inputArray));
                    }
                    catch (ApplicationException ex)
                    {
                        ListViewItem lvItem = new ListViewItem();
                        lvItem.Text = string.Empty;
                        lvItem.UseItemStyleForSubItems = false;

                        lvItem.SubItems.Add(string.Empty);
                        lvItem.SubItems.Add(ex.Message);
                        lvItem.SubItems[2].ForeColor = Color.Red;

                        listParse.Items.Add(lvItem);
                    }

                    if (listParse.Items.Count > 0)
                        listParse.EnsureVisible(listParse.Items.Count - 1);

                    listParse.EndUpdate();

                    var newlinePos = new List<int>();
                    newlinePos.Add(0);
                    newlinePos.AddRange(inputArray.Select((value, index) => new { index, value }).Where(el => el.value == '\n').Select(el => el.index));
                    newlinePos.Add(inputArray.Count());

                    textCode.IndicatorCurrent = ErrorIndicatorIndex;
                    foreach (var l in parser.ErrorLines)
                        textCode.IndicatorFillRange(newlinePos[l - 1], newlinePos[l] - newlinePos[l - 1]);
                });
            });

            analyseTask.ContinueWith((lastTask) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listSymbolTable.BeginUpdate();
                    foreach (var s in (rbCustom.Checked ? customSymbolTable : parserHelper.SymbolTable).ToList().Select((value, index) => new { index, value }))
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = s.index.ToString();
                        item.SubItems.Add(s.value.Lexeme);

                        if (s.value.Tag != LexerCore.Tag.Identifier)
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

        private void rbCustom_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openCtxDialog.ShowDialog())
                try
                {
                    customParser = Parser.CreateFromContext(openCtxDialog.OpenFile(), customSymbolTable, null, new ParseStepReporter(this, listParse));
                }
                catch (Exception ex) when (ex is SerializationException || ex.ToString() == "Invalid Magic Number!")
                {
                    MessageBox.Show("Invalid Magic Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rbCustom_Click(sender, e);
                }
        }
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

        public void ReportStep(bool error, string action, string stack, string symbol)
        {
            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = stack;
            lvItem.UseItemStyleForSubItems = false;

            lvItem.SubItems.Add(symbol);
            lvItem.SubItems.Add(action);

            if (error)
                lvItem.SubItems[2].ForeColor = Color.Red;
            else if (action == "accept")
                lvItem.SubItems[2].ForeColor = Color.Green;

            owner.Invoke((MethodInvoker)delegate { lvParseStep.Items.Add(lvItem); });
        }
    }
}
