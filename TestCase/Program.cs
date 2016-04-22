using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using CompilingPrinciples.LexicalAnalyzer;
using CompilingPrinciples.SymbolEnvironment;

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

            //Application.Run(new LexerForm());
            Application.Run(new ParserForm());
        }
    }
}
