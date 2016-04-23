using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserModule;

namespace CompilingPrinciples.UnitTest
{
    public static class StringToMemory
    {
        public static MemoryStream Create(string line)
        {
            return Create(new string[] { line });
        }

        public static MemoryStream Create(string[] lines)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, new UTF8Encoding(), 1024, true))
            {
                foreach (var line in lines)
                    writer.WriteLine(line);

                writer.Flush();
            }

            stream.Position = 0;
            return stream;
        }
    }

    public static class TestHelper
    {
        private static SymbolTable symbolTable = new SymbolTable();

        public static Parser<LR0Item> SLRParser = CreateParser();

        public static Parser<LR0Item> CreateParser()
        {
            Parser<LR0Item> ret = null;
            using (var stream = new FileStream("SLRParserContext.ctx", FileMode.Open))
                ret = Parser.CreateFromContext(stream, symbolTable, null) as Parser<LR0Item>;
            return ret;
        }
    }

    [TestClass]
    public class ParserTest
    {
        private Parser<LR0Item> parser = TestHelper.SLRParser;

        [TestMethod]
        public void D_Valid()
        {
            var stream = StringToMemory.Create(new string[]
            {
                "int a;",
                "int b;",
                "a=2;", // this line is required by syntax
            });

            var ops = parser.Parse(stream);
            Assert.AreEqual(ops[ops.Count - 1].Item1, "accept");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void D_Id_Missing()
        {
            var stream = StringToMemory.Create("int ;");
            parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void D_Semicolon_Missing()
        {
            var stream = StringToMemory.Create("int a int b");
            parser.Parse(stream);
        }

        [TestMethod]
        public void SampleCode()
        {
            var stream = StringToMemory.Create(new string[]
            {
                "int a;",
                "int b;",
                "int c;",
                "a = 2;",
                "b = 1;",
                "if (a>b)",
                "   c=a+b;",
                "else",
                "   c=a-b;"
            });

            var ops = parser.Parse(stream);
            Assert.AreEqual(ops[ops.Count - 1].Item1, "accept");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void S_Semicolon_Missing()
        {
            var stream = StringToMemory.Create("a=b");
            parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_S_Semicolon_Missing()
        {
            var stream = StringToMemory.Create(new string[] {
                "if (a>b)",
                "  a=b"
            });
            parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_Empty_Semicolon_Missing()
        {
            var stream = StringToMemory.Create("if (a>b)");
            parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_S_Missing()
        {
            var stream = StringToMemory.Create("if (a>b) ;");
            parser.Parse(stream);
        }

        [TestMethod]
        public void If_Valid()
        {
            var stream = StringToMemory.Create("if (a>b) a=b;");
            var ops = parser.Parse(stream);
            Assert.AreEqual(ops[ops.Count - 1].Item1, "accept");
        }

        [TestMethod]
        public void If_Else_Valid()
        {
            var stream = StringToMemory.Create("if (a>b) a=b; else c=a+b;");
            var ops = parser.Parse(stream);
            Assert.AreEqual(ops[ops.Count - 1].Item1, "accept");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_Else_S1_Missing()
        {
            var stream = StringToMemory.Create(new string[] {
                "if (a>b)",
                "  ;",
                "else",
                "  b=a;"
            });
            parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_Else_S2_Missing()
        {
            var stream = StringToMemory.Create(new string[] {
                "if (a>b)",
                "   a =b;",
                "else",
                "   ;"
            });
            parser.Parse(stream);
        }

        [TestMethod]
        public void If_S_Valid()
        {
            var stream = StringToMemory.Create("if (a>b) a=b; c=a+b;");
            var ops = parser.Parse(stream);
            Assert.AreEqual(ops[ops.Count - 1].Item1, "accept");
        }
    }
}
