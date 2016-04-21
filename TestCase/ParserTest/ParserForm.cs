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
        private ParserHelper parserHelper;

        public ParserForm()
        {
            InitializeComponent();

            parserHelper = new ParserHelper(this);
        }

        private void ParserForm_Shown(object sender, EventArgs e)
        {
            if (!parserHelper.CotextLoaded)
                parserHelper.CreateParserFromGrammar();
        }

        private void ParserForm_Load(object sender, EventArgs e)
        {
            parserHelper.CreateParserFromContext();
        }
    }
}
