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
using CompilingPrinciples.IntermediateCodeGenCore;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.GUICodeGen
{
    public partial class CodeGenForm : Form
    {
        private const int ErrorIndicatorIndex = 8;

        private ExperimentParserHelper parserHelper;

        public CodeGenForm()
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

            parserHelper = new ExperimentParserHelper(this);
            parserHelper.CreateParserFromContext(slr: true, lr1: false);
        }

        private void ParserForm_Shown(object sender, EventArgs e)
        {
            if (!parserHelper.SLRContextLoaded)
                parserHelper.CreateParserFromGrammar(genSLR: true, genLR1: false);
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
            listInterCode.Items.Clear();
            listSymbolTable.Items.Clear();

            btnAnalyze.Enabled = false;
            btnOpen.Enabled = false;
            textCode.Enabled = false;

            textCode.IndicatorCurrent = ErrorIndicatorIndex;
            textCode.IndicatorClearRange(0, textCode.Text.Length);

            var inputArray = Encoding.ASCII.GetBytes(textCode.Text);
            IntermediateCodeGen codeGen = null;
            var codeGenErr = string.Empty;

            var analyseTask = new Task(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listInterCode.BeginUpdate();

                    try
                    {
                        var parseTreeRoot = parserHelper.SLRParser.Parse(new MemoryStream(inputArray));
                        codeGen = new IntermediateCodeGen(parserHelper.SymbolTable, parseTreeRoot);

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

                    foreach (var pair in ThreeAddrCodeFormatter.ToPair(codeGen.ThreeAddrCode))
                    {
                        ListViewItem lvItem = new ListViewItem();
                        lvItem.Text = pair.Item1;
                        lvItem.UseItemStyleForSubItems = false;

                        if (!string.IsNullOrEmpty(pair.Item1))
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
                    textCode.Focus();
                });
            });

            analyseTask.Start();
        }
    }
}
