using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using CompilingPrinciples.LexicalAnalyzer;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.SyntaxAnalyzer;

namespace CompilingPrinciples.TestCase
{   
    public class ParserTest
    {
        private static void ShowProductions(Grammar grammar)
        {
            Console.WriteLine("========================= Productions ======================");
            foreach (var prod in grammar.Productions)
                Console.WriteLine(prod.ToString());
        }

        private static void ShowSymbols(Grammar grammar)
        {
            Console.WriteLine("======================= Non-Terminals ======================");
            foreach (var e in grammar.NonTerminals.Select((value, index) => new { value, index }))
                Console.Write(e.value.ToString() + (e.index < grammar.NonTerminals.Count - 1 ? ", " : "\n"));

            Console.WriteLine("========================= Terminals ========================");
            foreach (var e in grammar.Terminals.Select((value, index) => new { value, index }))
                Console.Write(e.value.ToString() + (e.index < grammar.Terminals.Count - 1 ? ", " : "\n"));
        }

        private static void ShowFirstFollowSets(Grammar grammar, FirstFollowSet firstSet, FirstFollowSet followSet)
        {
            Console.WriteLine("========================= First Set ========================");
            foreach (var sym in grammar.NonTerminals)
            {
                Console.Write("First(" + sym.ToString() + ") = { ");
                foreach (var e in firstSet.Get(sym).Select((value, index) => new { value, index }))
                {
                    if (e.value.ToString() == "@")
                        Console.Write("ε");
                    else
                        Console.Write(e.value.ToString());

                    if (e.index < firstSet.Get(sym).Count - 1)
                        Console.Write(", ");
                }

                Console.WriteLine(" }");
            }

            Console.WriteLine("========================= Follow Set ========================");
            foreach (var sym in grammar.NonTerminals)
            {
                Console.Write("Follow(" + sym.ToString() + ") = { ");
                foreach (var e in followSet.Get(sym).Select((value, index) => new { value, index }))
                {
                    if (e.value.ToString() == "@")
                        Console.Write("ε");
                    else
                        Console.Write(e.value.ToString());

                    if (e.index < followSet.Get(sym).Count - 1)
                        Console.Write(", ");
                }

                Console.WriteLine(" }");
            }
        }

        private static void ShowParseTable(Grammar grammar, ParseTable pt)
        {
            Console.WriteLine("================== Action Table ===================================");
            for (int i = 0; i <= pt.StateCount; i++)
            {
                if (pt.Action.ContainsKey(i))
                    foreach (var term in grammar.TerminalsWithEndMarker.Where(e => pt.Action[i].ContainsKey(e)))
                        Console.WriteLine("ACTION[{0}, {1}]={2}", i, term, pt.Action[i][term]);
                Console.WriteLine("===============================================================");
            }

            Console.WriteLine("================== Goto Table ===================================");
            for (int i = 0; i <= pt.StateCount; i++)
            {
                if (pt.Goto.ContainsKey(i))
                    foreach (var nonTerm in grammar.NonTerminalsWithoutAugmentedS.Where(e => pt.Goto[i].ContainsKey(e)))
                        Console.WriteLine("GOTO[{0}, {1}]={2}", i, nonTerm, pt.Goto[i][nonTerm]);
                Console.WriteLine("===============================================================");
            }
        }

        private static void ShowItems<T>(LRCollection<T> coll) where T : LR0Item
        {
            var curIndex = -1;
            foreach (var e in coll.Items().Select((value, index) => new { index, value }))
            {
                if (curIndex != e.index)
                {
                    Console.WriteLine("==================== I[" + e.index + "] ====================");
                    curIndex = e.index;
                }

                foreach (var i in e.value)
                    Console.WriteLine(i);
            }
        }

        private static void TestFirstFollow(string fileName)
        {
            Grammar grammar = new Grammar(new SymbolTable());
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                grammar.Parse(stream);
                stream.Close();
            }

            ShowProductions(grammar);
            ShowSymbols(grammar);

            FirstFollowGenerator gen = new FirstFollowGenerator(grammar);
            ShowFirstFollowSets(grammar, gen.First, gen.Follow);

            Console.ReadLine();
        }

        private static void TestSLRParser()
        {
            var symbolTable = new SymbolTable();

            Grammar grammar = new Grammar(symbolTable);
            using (var stream = new FileStream("Grammar-4.40.txt", FileMode.Open))
            {
                grammar.Parse(stream);
                stream.Close();
            }

            var coll = new LR0Collection(grammar);

            // Cope with file Grammar-4.40.txt
            Console.WriteLine("============== Closure({E' -> ·E}) ===============");
            var closure = coll.Closure(new HashSet<LR0Item>(new LR0Item[] { new LR0Item(grammar, grammar.FirstProduction, 0) }));
            foreach (var e in closure)
                Console.WriteLine(e);

            Console.WriteLine("============== GOTO({E' -> E·, E -> E· + T}, +) ===============");
            var gotoSet = coll.Goto(new HashSet<LR0Item>(new LR0Item[] { new LR0Item(grammar, grammar.Productions[0], 1), new LR0Item(grammar, grammar.Productions[1], 1) }),
                new ProductionSymbol(grammar, ProductionSymbol.SymbolType.Terminal, grammar.TerminalTable.IndexOf("+")));
            foreach (var e in gotoSet)
                Console.WriteLine(e);

            Console.WriteLine("============== LR(0) Collection ============================");
            ShowItems(coll);

            var parseTable = SLRParseTable.Create(coll);
            ShowParseTable(grammar, parseTable);

            Console.WriteLine("=============== Parse: a * b ====================================");
            var sb = new StringBuilder();
            sb.Append("a * b");
            sb.Append((byte)0xFF);

            byte[] array = Encoding.ASCII.GetBytes(sb.ToString());
            var parser = new Parser<LR0Item>(symbolTable, grammar, parseTable, null);
            var ops = parser.Parse(new MemoryStream(array));

            foreach (var op in ops)
                Console.WriteLine(op);

            Console.ReadLine();
        }

        private static void TestLR1Parser()
        {
            var symbolTable = new SymbolTable();
            Grammar grammar = new Grammar(symbolTable);
            using (var stream = new FileStream("Grammar-4.55.txt", FileMode.Open))
            {
                grammar.Parse(stream);
                stream.Close();
            }

            var coll = new LR1Collection(grammar);
            var gen = new FirstFollowGenerator(grammar);

            // Cope with file Grammar-4.55.txt
            Console.WriteLine("============== Closure({S' -> ·S, $}) ===============");
            var closure = coll.Closure(new HashSet<LR1Item>(new LR1Item[] { new LR1Item(grammar, grammar.FirstProduction, 0, grammar.EndMarker) }));
            foreach (var e in closure)
                Console.WriteLine(e);

            Console.WriteLine("============== LR(1) Collection ============================");
            ShowItems(coll);

            var parseTable = LR1ParseTable.Create(coll);
            ShowParseTable(grammar, parseTable);
            
            Console.ReadLine();
        }

        private static void AutoSelectAmbiguousAction<T>(Grammar grammar, ParseTable<T> parseTable, LRCollection<T> coll) 
            where T : LR0Item
        {
            Console.WriteLine("======================== Select Prefer Entry =======================");
            foreach (var pend in parseTable.Action.Select((value, index) => new { index, value }))
                foreach (var term in grammar.TerminalsWithEndMarker)
                    if (pend.value.Value.ContainsKey(term) && !pend.value.Value[term].PreferedEntrySpecified)
                    {
                        Console.WriteLine("=========I[" + pend.index + "]==============");
                        foreach (var e in coll.Items().Select((value, index) => new { index, value }).Where(e => e.index == pend.index))
                            foreach (var it in e.value)
                                Console.WriteLine(it);

                        Console.WriteLine("============================================");
                        Console.WriteLine("Select for ACTION[" + pend.index + ", " + term + "]");
                        Console.WriteLine("=========== Actions pending =======");
                        foreach (var e in pend.value.Value[term].Entries.Select((value, index) => new { index, value }))
                        {
                            Console.WriteLine(e.index.ToString() + "." + e.value);
                            if (e.value.Type == ActionTableEntry.ActionType.Shift)
                            {
                                foreach (var col in coll.Items().Select((value, index) => new { index, value }).Where(c => c.index == e.value.ShiftState))
                                    foreach (var it in col.value)
                                        Console.WriteLine(it);
                            }
                        }

                        foreach (var e in pend.value.Value[term].Entries.Select((value, index) => new { index, value })
                                                                       .Where(e => e.value.Type == ActionTableEntry.ActionType.Reduce))
                        {
                            if (term.ToString() == "else" && pend.value.Value[term].Entries.ToList()[e.index].ReduceProduction.ToString() == "S -> if ( C ) S")
                            {
                                // just skip
                            }
                            else
                                pend.value.Value[term].SetPreferEntry(pend.value.Value[term].Entries.ToList()[e.index]);
                        }

                        if (!pend.value.Value[term].PreferedEntrySpecified)
                        {
                            foreach (var e in pend.value.Value[term].Entries.Select((value, index) => new { index, value })
                                                                       .Where(e => e.value.Type == ActionTableEntry.ActionType.Shift))
                            {
                                pend.value.Value[term].SetPreferEntry(pend.value.Value[term].Entries.ToList()[e.index]);
                            }
                        }

                        Console.WriteLine("Choosed: " + pend.value.Value[term].PreferEntry);

                    }
        }

        private static void ManualSelectAmbiguousAction<T>(ParseTable<T> parseTable, Grammar grammar, LRCollection<T> coll)
            where T : LR0Item
        {
            Console.WriteLine("======================== Select Prefer Entry =======================");
            foreach (var pend in parseTable.Action.Select((value, index) => new { index, value }))
                foreach (var term in grammar.TerminalsWithEndMarker)
                    if (pend.value.Value.ContainsKey(term) && !pend.value.Value[term].PreferedEntrySpecified)
                    {
                        Console.WriteLine("=========I[" + pend.index + "]==============");
                        foreach (var e in coll.Items().Select((value, index) => new { index, value }).Where(e => e.index == pend.index))
                            foreach (var it in e.value)
                                Console.WriteLine(it);

                        Console.WriteLine("============================================");
                        Console.WriteLine("Select for ACTION[" + pend.index + ", " + term + "]");
                        Console.WriteLine("=========== Actions pending =======");
                        foreach (var e in pend.value.Value[term].Entries.Select((value, index) => new { index, value }))
                        {
                            Console.WriteLine(e.index.ToString() + "." + e.value);
                            if (e.value.Type == ActionTableEntry.ActionType.Shift)
                            {
                                foreach (var col in coll.Items().Select((value, index) => new { index, value }).Where(c => c.index == e.value.ShiftState))
                                    foreach (var it in col.value)
                                        Console.WriteLine(it);
                            }
                        }

                        int sel = int.Parse(Console.ReadLine());
                        pend.value.Value[term].SetPreferEntry(pend.value.Value[term].Entries.ToList()[sel]);
                    }
        }

        private static void ParseSampleCode(Parser parser)
        {
            var fs = new FileStream("ParserTest.lc", FileMode.Open);
            var ops = parser.Parse(fs);

            Console.WriteLine("{0,-40} {1}", "SYMBOLS", "ACTION");
            foreach (var op in ops)
                Console.WriteLine("{0,-40} {1}", op.Item2, op.Item1);

            fs.Close();
        }

        private static void TestExperiment_SLR()
        {
            var symbolTable = new SymbolTable();
            Grammar grammar = new Grammar(symbolTable);
            using (var stream = new FileStream("Grammar-Ex.txt", FileMode.Open))
            {
                grammar.Parse(stream);
                stream.Close();
            }

            ShowProductions(grammar);
            ShowSymbols(grammar);
                
            var coll = new LR0Collection(grammar);
            var parseTable = SLRParseTable.Create(coll);

            ShowFirstFollowSets(grammar, coll.First, coll.Follow);

            Console.WriteLine("State count: " + parseTable.StateCount);
            
            AutoSelectAmbiguousAction(grammar, parseTable, coll);

            Console.WriteLine("=============== Parse Sample Code ===================================");
            var parser = new Parser<LR0Item>(symbolTable, grammar, parseTable, null);
            ParseSampleCode(parser);

            Console.WriteLine("==================== Serialization =================================");
            var outStream = new FileStream("SLRParserContext.ctx", FileMode.Create);
            parser.SaveContext(outStream);
            outStream.Close();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        private static void TestExperiment_LR1()
        {
            var symbolTable = new SymbolTable();
            Grammar grammar = new Grammar(symbolTable);
            using (var stream = new FileStream("Grammar-Ex.txt", FileMode.Open))
            {
                grammar.Parse(stream);
                stream.Close();
            }   

            ShowProductions(grammar);
            ShowSymbols(grammar);

            var coll = new LR1Collection(grammar);
            var parseTable = LR1ParseTable.Create(coll);

            ShowFirstFollowSets(grammar, coll.First, coll.Follow);

            Console.WriteLine("State count: " + parseTable.StateCount);

            AutoSelectAmbiguousAction(grammar, parseTable, coll);

            Console.WriteLine("=============== Parse Sample Code ===================================");
            var parser = new Parser<LR1Item>(symbolTable, grammar, parseTable, null);
            ParseSampleCode(parser);

            Console.WriteLine("==================== Serialization =================================");
            var outStream = new FileStream("LR1ParserContext.ctx", FileMode.Create);
            parser.SaveContext(outStream);
            outStream.Close();

            Console.WriteLine("Done.");

            Console.ReadLine();
        }

        private static void TestDanglingElse_SLR(string fileName)
        {
            var symbolTable = new SymbolTable();
            Grammar grammar = new Grammar(symbolTable);

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                grammar.Parse(stream);
                stream.Close();
            }

            var coll = new LR0Collection(grammar);
            Console.WriteLine("============== LR(0) Collection ============================");
            ShowItems(coll);

            var parseTable = SLRParseTable.Create(coll);

            ManualSelectAmbiguousAction(parseTable, grammar, coll);

            ShowParseTable(grammar, parseTable);

            Console.ReadLine();
        }

        private static void TestDanglingElse_LR1(string fileName)
        {
            var symbolTable = new SymbolTable();
            Grammar grammar = new Grammar(symbolTable);

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                grammar.Parse(stream);
                stream.Close();
            }

            var coll = new LR1Collection(grammar);
            Console.WriteLine("============== LR(1) Collection ============================");
            ShowItems(coll);

            var parseTable = LR1ParseTable.Create(coll);

            ManualSelectAmbiguousAction(parseTable, grammar, coll);
            
            ShowParseTable(grammar, parseTable);

            Console.ReadLine();
        }

        private static void TestDeserialization_SLR()
        {
            var se_stream = new FileStream("SLRParserContext.ctx", FileMode.Open);

            Console.WriteLine("==================== SLR Deserialization =================================");
            var st = new SymbolTable();
            var parser = Parser.CreateFromContext(se_stream, st, null);

            Console.WriteLine("=============== Parse Sample Code ===================================");

            var fs = new FileStream("ParserTest.lc", FileMode.Open);
            var newOps = parser.Parse(fs);

            Console.WriteLine("{0,-40} {1}", "SYMBOLS", "ACTION");
            foreach (var op in newOps)
                Console.WriteLine("{0,-40} {1}", op.Item2, op.Item1);

            fs.Close();
            se_stream.Close();

            Console.ReadLine();
        }

        private static void TestDeserialization_LR1()
        {
            var se_stream = new FileStream("LR1ParserContext.ctx", FileMode.Open);

            Console.WriteLine("==================== LR(1) Deserialization =================================");
            var st = new SymbolTable();
            var parser = Parser.CreateFromContext(se_stream, st, null);

            Console.WriteLine("=============== Parse Sample Code ===================================");

            var fs = new FileStream("ParserTest.lc", FileMode.Open);
            var newOps = parser.Parse(fs);

            Console.WriteLine("{0,-40} {1}", "SYMBOLS", "ACTION");
            foreach (var op in newOps)
                Console.WriteLine("{0,-40} {1}", op.Item2, op.Item1);

            fs.Close();
            se_stream.Close();

            Console.ReadLine();
        }

        public static void LaunchTest()
        {
            //TestFirstFollow("Grammar-4.28.txt");
            //TestFirstFollow("Grammar-Ex.txt");

            //TestSLRParser();
            //TestLR1Parser();

            //TestDanglingElse_SLR("Grammar-4.3.txt");
            //TestDanglingElse_SLR("Grammar-4.67.txt");

            //TestDanglingElse_LR1("Grammar-4.3.txt");
            //TestDanglingElse_LR1("Grammar-4.67.txt");

            TestExperiment_SLR();
            TestDeserialization_SLR();

            //TestExperiment_LR1();
            //TestDeserialization_LR1();
        }
    }
}
