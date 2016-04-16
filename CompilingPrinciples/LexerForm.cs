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

using Lex;
using Symbol;

namespace CompilingPrinciples
{
    public partial class LexerForm : Form
    {
        private const int ErrorIndicatorIndex = 8;

        public LexerForm()
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
            listTokens.Items.Clear();
            listSymbols.Items.Clear();

            textCode.IndicatorCurrent = ErrorIndicatorIndex;
            textCode.IndicatorClearRange(0, textCode.Text.Length);
            
            byte[] array = Encoding.ASCII.GetBytes(textCode.Text);
            MemoryStream stream = new MemoryStream(array);

            Lexer lexer = new Lexer(stream);
            Token token = null;

            int curLine = 0;
            ListViewGroup curGroup = null;

            listTokens.BeginUpdate();
            while (true)
            {
                try
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
                        item.SubItems.Add(lexer.SymbolTable.GetSymbol(token.GetValue()));
                    else if (token is InvalidToken)
                    {
                        InvalidToken errToken = token as InvalidToken;
                        textCode.IndicatorFillRange(errToken.Position, errToken.Length);
                        item.SubItems.Add(token.GetValue().ToString());
                    }
                    else
                        item.SubItems.Add(token.GetValue().ToString());

                    // Set item style
                    if (token is Operator || token is Separator)
                        item.ForeColor = Color.Gray;
                    else if (token is Lex.Decimal)
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
                catch (NullReferenceException)
                {
                    break;
                }
            }
            listTokens.EndUpdate();

            listSymbols.BeginUpdate();
            foreach (var s in lexer.SymbolTable.ToList().Select((value, index) => new { index, value }))
            {
                ListViewItem item = new ListViewItem();
                item.Text = s.index.ToString();
                item.SubItems.Add(s.value.Lexeme);

                if (s.value.Tag != Lex.Tag.Identifier)
                    item.Group = listSymbols.Groups["lvGroupKeyword"];
                else
                    item.Group = listSymbols.Groups["lvGroupIdentifier"];

                listSymbols.Items.Add(item);
            }
            listSymbols.EndUpdate();
        }

        private void textCode_KeyUp(object sender, KeyEventArgs e)
        {
            btnAnalyze_Click(sender, e);
        }
    }
}
