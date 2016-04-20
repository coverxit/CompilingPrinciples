using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using LexicalAnalyzer;
using SymbolEnvironment;

namespace CompilingPrinciples
{
    class Program
    {
        static void LexerCLITest()
        {
            Console.WriteLine("----------------Lexer Test---------------");
            using (var stream = new FileStream("test.txt", FileMode.Open))
            {
                Lexer lexer = new Lexer(new SymbolTable(), stream);
                Token token = null;
                int curLine = 1;

                Console.WriteLine("============== Line 1 ==============");
                while (!(token is EndMarker))
                {
                    token = lexer.ScanNextToken();
                    if (curLine != lexer.CurrentLine)
                        Console.WriteLine("============== Line " + lexer.CurrentLine + " ==============");

                    if (token is Identifier)
                        Console.WriteLine("<" + token.GetTokenType() + ", '" +
                            lexer.SymbolTable.GetSymbol(token.GetValue()) + "'>");
                    else
                        Console.WriteLine(token.ToString());

                    curLine = lexer.CurrentLine;

                }

                Console.WriteLine();
                Console.WriteLine("===============Symbol Table================");
                foreach (var e in lexer.SymbolTable.ToList())
                    Console.WriteLine(e.Lexeme);
            }
        }

        static void LexerGUITest()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LexerForm());
        }

        [STAThread]
        static void Main(string[] args)
        {
            LexerCLITest();
            LexerGUITest();
        }
    }
}
