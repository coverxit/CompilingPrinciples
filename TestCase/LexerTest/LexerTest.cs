using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserCore;

namespace CompilingPrinciples.TestCase
{
    public class LexerTest
    {
        public static void LaunchTest()
        {
            Console.WriteLine("----------------Lexer Test---------------");
            using (var stream = new FileStream("LexerTest.lc", FileMode.Open))
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
                            lexer.SymbolTable.Get(token.GetValue()).ToString() + "'>");
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
    }
}
