using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using CompilingPrinciples.IntermediateCodeGenCore;
using CompilingPrinciples.ParserCore;
using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.TestCase
{
    public class InterCodeGenTest
    {
        public static void LaunchTest()
        {
            var se_stream = new FileStream("SLRParserContext.ctx", FileMode.Open);

            Console.WriteLine("==================== SLR Deserialization =================================");
            var st = new SymbolTable();
            var parser = Parser.CreateFromContext(se_stream, st, null);

            Console.WriteLine("=============== Parse Sample Code ===================================");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TreeTest());

            var fs = new FileStream("ParserTest.lc", FileMode.Open);
            var gen = new IntermediateCodeGen(st, parser.Parse(fs));

            foreach (var e in gen.Generate())
                Console.WriteLine(e);

            fs.Close();
            se_stream.Close();

            Console.ReadLine();
        }
    }
}
