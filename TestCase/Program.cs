using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;

using CompilingPrinciples.LexicalAnalyzer;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.SyntaxAnalyzer;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.TestCase
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // CLI Tests
            //LexerTest.LaunchTest();
            //ParserTest.LaunchTest();

            // GUI Tests
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new LexerForm().Show();
            new ParserForm().Show();

            Application.Run();

            //Application.Run(new LexerForm());
            //Application.Run(new ParserForm());
        }
    }
}
