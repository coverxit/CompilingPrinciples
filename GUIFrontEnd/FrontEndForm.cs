using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserCore;
using CompilingPrinciples.IntermediateCodeGenCore;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.GUIFrontEnd
{
    public partial class FrontEndForm : Form
    {
        private const int ErrorIndicatorIndex = 8;

        private ExperimentParserHelper parserHelper;

        private IntermediateCodeGen codeGen;
        private string codeGenErr;

        public FrontEndForm()
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

            parserHelper = new ExperimentParserHelper(this, new ParseStepReporter(this, listParse));
            parserHelper.CreateParserFromContext(slr: true, lr1: true);
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

        private void FrontEndForm_Shown(object sender, EventArgs e)
        {
            if (!parserHelper.ContextLoaded)
                parserHelper.CreateParserFromGrammar(genSLR: true, genLR1: true);
        }

        private void RadioButtonAddressType_CheckedChanged(object sender, EventArgs e)
        {
            if (codeGen == null) return;

            var rb = sender as RadioButton;
            if (rb.Checked)
            {
                listInterCode.Items.Clear();

                listInterCode.BeginUpdate();
                listInterCode.Columns[0].Text = rb == rbPseudo ? "Label" : "Address";
                FillInterCodeList(codeGen.ThreeAddrCode, pseudo: rb == rbPseudo);
                listInterCode.EndUpdate();
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            listTokens.Items.Clear();
            listParse.Items.Clear();
            listInterCode.Items.Clear();
            listSymbolTable.Items.Clear();

            btnAnalyze.Enabled = false;
            btnOpen.Enabled = false;
            textCode.Enabled = false;
            rbAddressed.Enabled = false;
            rbPseudo.Enabled = false;
            rbSLR.Enabled = false;
            rbLR1.Enabled = false;

            codeGen = null;
            codeGenErr = string.Empty;
            rbPseudo.Checked = true;

            textCode.IndicatorCurrent = ErrorIndicatorIndex;
            textCode.IndicatorClearRange(0, textCode.Text.Length);

            var inputArray = Encoding.ASCII.GetBytes(textCode.Text);
            var lexerTask = new Task(LexerTask, (object) inputArray);
            var parserTask = lexerTask.ContinueWith<TreeNode<ParseTreeNodeEntry>>(ParserTask, (object) inputArray);
            var codeGenTask = parserTask.ContinueWith(CodeGenTask, (object) inputArray);

            codeGenTask.ContinueWith((lastTask) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listSymbolTable.BeginUpdate();
                    foreach (var s in parserHelper.SymbolTable.ToList().Select((value, index) => new { index, value }))
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = s.index.ToString();
                        item.SubItems.Add(s.value.Lexeme);

                        if (s.value.Tag != LexerCore.Tag.Identifier)
                            item.Group = listSymbolTable.Groups["lvGroupKeyword"];
                        else
                        {
                            item.Group = listSymbolTable.Groups["lvGroupIdentifier"];
                            item.SubItems.Add(VarType.GetTypeWithWidth(s.value.Type));

                            if (s.value.Offset >= 0)
                                item.SubItems.Add(s.value.Offset.ToString());
                            else
                            {
                                item.SubItems.Add("N/A");
                                item.ForeColor = Color.Red;
                            }
                        }

                        listSymbolTable.Items.Add(item);
                    }
                    listSymbolTable.EndUpdate();

                    btnAnalyze.Enabled = true;
                    btnOpen.Enabled = true;
                    textCode.Enabled = true;
                    rbSLR.Enabled = true;
                    rbLR1.Enabled = true;
                    rbAddressed.Enabled = true;
                    rbPseudo.Enabled = true;
                    textCode.Focus();
                });
            });

            lexerTask.Start();
        }

        private void LexerTask(object array)
        {
            // Since parser will actually run lexer agian, so we use a temp SymbolTable
            var lexer = new Lexer(new SymbolTable(), new MemoryStream(array as byte[]));
            Token token = null;

            int curLine = 0;
            ListViewGroup curGroup = null;

            this.Invoke((MethodInvoker)delegate
            {
                listTokens.BeginUpdate();
                while (!(token is EndMarker))
                {
                    token = lexer.ScanNextToken();
                    if (curLine != lexer.CurrentLine)
                    {
                        curGroup = listTokens.Groups.Add(lexer.CurrentLine.ToString(), "Line " + lexer.CurrentLine);
                        curLine = lexer.CurrentLine;
                    }

                    ListViewItem item = new ListViewItem(curGroup);
                    item.Text = token.GetTokenType();

                    // Set item text & error indicator if any
                    if (token is Identifier)
                        item.SubItems.Add(lexer.SymbolTable.Get(token.GetValue()).ToString());
                    else if (token is InvalidToken)
                    {
                        textCode.IndicatorCurrent = ErrorIndicatorIndex;
                        textCode.IndicatorFillRange(token.Position, token.Length);
                        item.SubItems.Add(token.GetValue().ToString());
                    }
                    else
                        item.SubItems.Add(token.GetValue().ToString());

                    // Set item style
                    if (token is Operator || token is Separator)
                        item.ForeColor = Color.Gray;
                    else if (token is LexerCore.Decimal)
                        item.ForeColor = Color.ForestGreen;
                    else if (token is InvalidToken)
                        item.ForeColor = Color.Red;
                    else if (token is Identifier)
                    {
                        item.UseItemStyleForSubItems = false;
                        item.SubItems[1].Font = new Font(item.Font, FontStyle.Bold);
                    }
                    else if (token is Keyword)
                    {
                        item.ForeColor = Color.Blue;
                        item.Font = new Font(item.Font, FontStyle.Bold);

                        item.UseItemStyleForSubItems = false;
                        item.SubItems[1].Font = new Font(item.Font, FontStyle.Italic);
                        item.SubItems[1].ForeColor = Color.DarkBlue;
                    }

                    listTokens.Items.Add(item);
                }
                listTokens.EndUpdate();
            });
        }

        private TreeNode<ParseTreeNodeEntry> ParserTask(Task lexerTask, object array)
        {
            Parser parser = null;
            var inputArray = array as byte[];
            TreeNode<ParseTreeNodeEntry> ret = null;

            this.Invoke((MethodInvoker)delegate
            {
                if (rbLR1.Checked)
                    parser = parserHelper.LR1Parser;
                else
                    parser = parserHelper.SLRParser;

                listParse.BeginUpdate();
                try
                {
                    ret = parser.Parse(new MemoryStream(inputArray));
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

            return ret;
        }

        private void FillInterCodeList(List<string> code, bool pseudo)
        {
            var formatted = pseudo ? ThreeAddrCodeFormatter.ToPair(code)
                                   : ThreeAddrCodeFormatter.ToAddressed(code);

            foreach (var pair in formatted)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = pair.Item1;
                lvItem.UseItemStyleForSubItems = false;

                if (pseudo && !string.IsNullOrEmpty(pair.Item1))
                    lvItem.SubItems[0].Font = new Font(lvItem.SubItems[0].Font, FontStyle.Bold);

                lvItem.SubItems.Add(pair.Item2);
                listInterCode.Items.Add(lvItem);
            }

            if (!string.IsNullOrEmpty(codeGenErr))
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = string.Empty;
                lvItem.UseItemStyleForSubItems = false;

                lvItem.SubItems.Add(codeGenErr);
                lvItem.SubItems[1].ForeColor = Color.Red;

                listInterCode.Items.Add(lvItem);
            }
        }

        private void CodeGenTask(Task<TreeNode<ParseTreeNodeEntry>> parserTask, object array)
        {
            // Is parser done without error?
            if (parserTask.Result == null) return;
            var inputArray = array as byte[];

            this.Invoke((MethodInvoker)delegate
            {
                listInterCode.BeginUpdate();

                try
                {
                    codeGen = new IntermediateCodeGen(parserHelper.SymbolTable, parserTask.Result);
                    codeGen.Generate();
                }
                catch (ApplicationException ex)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = string.Empty;
                    lvItem.UseItemStyleForSubItems = false;

                    lvItem.SubItems.Add(ex.Message);
                    lvItem.SubItems[1].ForeColor = Color.Red;

                    listInterCode.Items.Add(lvItem);
                }
                catch (IntermediateCodeGenException ex)
                {
                    codeGenErr = ex.Message;
                }

                FillInterCodeList(codeGen.ThreeAddrCode, pseudo: true);

                listInterCode.EndUpdate();

                var newlinePos = new List<int>();
                newlinePos.Add(0);
                newlinePos.AddRange(inputArray.Select((value, index) => new { index, value }).Where(el => el.value == '\n').Select(el => el.index));
                newlinePos.Add(inputArray.Count());

                textCode.IndicatorCurrent = ErrorIndicatorIndex;
                foreach (var l in parserHelper.SLRParser.ErrorLines)
                    textCode.IndicatorFillRange(newlinePos[l - 1], newlinePos[l] - newlinePos[l - 1]);
                if (codeGen.ErrorLine > 0)
                    textCode.IndicatorFillRange(newlinePos[codeGen.ErrorLine - 1], newlinePos[codeGen.ErrorLine] - newlinePos[codeGen.ErrorLine - 1]);
            });
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
