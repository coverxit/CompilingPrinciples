using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParseGenerator
{
    class Program
    {
        private static void TestFirstFollow()
        {
            Grammar grammar = new Grammar();
            using (var stream = new FileStream("Grammar-4.28.txt", FileMode.Open))
                grammar.Parse(stream);

            FirstFollowGenerator gen = new FirstFollowGenerator(grammar);

            Console.WriteLine("========================= Productions ======================");
            foreach (var prod in grammar.Productions)
                Console.WriteLine(prod.ToString());

            Console.WriteLine("======================= Non-Terminals ======================");
            foreach (var e in grammar.NonTerminals.Select((value, index) => new { value, index }))
                Console.Write(e.value.ToString() + (e.index < grammar.NonTerminals.Count - 1 ? ", " : "\n"));

            Console.WriteLine("========================= Terminals ========================");
            foreach (var e in grammar.Terminals.Select((value, index) => new { value, index }))
                Console.Write(e.value.ToString() + (e.index < grammar.Terminals.Count - 1 ? ", " : "\n"));

            Console.WriteLine("========================= First Set ========================");
            foreach (var sym in grammar.NonTerminals)
            {
                Console.Write("First(" + sym.ToString() + ") = { ");
                foreach (var e in gen.FirstSet.Get(sym).Select((value, index) => new { value, index }))
                {
                    if (e.value.ToString() == "@")
                        Console.Write("ε");
                    else
                        Console.Write(e.value.ToString());

                    if (e.index < gen.FirstSet.Get(sym).Count - 1)
                        Console.Write(", ");
                }

                Console.WriteLine(" }");
            }

            Console.WriteLine("========================= Follow Set ========================");
            foreach (var sym in grammar.NonTerminals)
            {
                Console.Write("Follow(" + sym.ToString() + ") = { ");
                foreach (var e in gen.FollowSet.Get(sym).Select((value, index) => new { value, index }))
                {
                    if (e.value.ToString() == "@")
                        Console.Write("ε");
                    else
                        Console.Write(e.value.ToString());

                    if (e.index < gen.FollowSet.Get(sym).Count - 1)
                        Console.Write(", ");
                }

                Console.WriteLine(" }");
            }

            Console.ReadLine();
        }

        private static void TestSLRParser()
        {
            Grammar grammar = new Grammar();
            using (var stream = new FileStream("Grammar-4.40.txt", FileMode.Open))
                grammar.Parse(stream);

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

            var parseTable = SLRParseTable.Create(coll);
            Console.WriteLine("================== Action Table ===================================");
            for (int i = 0; i <= curIndex; i++)
            { 
                foreach (var term in grammar.TerminalsWithEndMarker)
                    Console.WriteLine("ACTION[{0}, {1}]={2}", i, term, parseTable.Action[i][term]);
                Console.WriteLine("===============================================================");
            }

            Console.WriteLine("================== Goto Table ===================================");
            for (int i = 0; i <= curIndex; i++)
            {
                foreach (var nonTerm in grammar.NonTerminalsWithoutAugmentedS)
                    Console.WriteLine("GOTO[{0}, {1}]={2}", i, nonTerm, parseTable.Goto[i][nonTerm]);
                Console.WriteLine("===============================================================");
            }

            Console.WriteLine("=============== Parse: a * b ====================================");
            var sb = new StringBuilder();
            sb.Append("a * b");
            sb.Append((byte)0xFF);

            byte[] array = Encoding.ASCII.GetBytes(sb.ToString());
            var parser = new Parser(grammar, parseTable);
            var ops = parser.Parse(new MemoryStream(array));

            foreach (var op in ops)
                Console.WriteLine(op);

            Console.ReadLine();
        }

        private static void TestLR1Collection()
        {
            Grammar grammar = new Grammar();
            using (var stream = new FileStream("Grammar-4.55.txt", FileMode.Open))
                grammar.Parse(stream);

            var coll = new LR1Collection(grammar);
            var gen = new FirstFollowGenerator(grammar);
            
            // Cope with file Grammar-4.55.txt
            Console.WriteLine("============== Closure({S' -> ·S, $}) ===============");
            var closure = coll.Closure(new HashSet<LR1Item>(new LR1Item[] { new LR1Item(grammar, grammar.FirstProduction, 0, grammar.EndMarker) }));
            foreach (var e in closure)
                Console.WriteLine(e);

            Console.WriteLine("============== LR(1) Collection ============================");
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

            Console.ReadLine();
        }

        private static void TestExperiment_SLR()
        {
            Grammar grammar = new Grammar();
            using (var stream = new FileStream("Grammar-Ex.txt", FileMode.Open))
                grammar.Parse(stream);

            FirstFollowGenerator gen = new FirstFollowGenerator(grammar);

            Console.WriteLine("========================= Productions ======================");
            foreach (var prod in grammar.Productions)
                Console.WriteLine(prod.ToString());

            Console.WriteLine("======================= Non-Terminals ======================");
            foreach (var e in grammar.NonTerminals.Select((value, index) => new { value, index }))
                Console.Write(e.value.ToString() + (e.index < grammar.NonTerminals.Count - 1 ? ", " : "\n"));

            Console.WriteLine("========================= Terminals ========================");
            foreach (var e in grammar.Terminals.Select((value, index) => new { value, index }))
                Console.Write(e.value.ToString() + (e.index < grammar.Terminals.Count - 1 ? ", " : "\n"));

            Console.WriteLine("========================= First Set ========================");
            foreach (var sym in grammar.NonTerminals)
            {
                Console.Write("First(" + sym.ToString() + ") = { ");
                foreach (var e in gen.FirstSet.Get(sym).Select((value, index) => new { value, index }))
                {
                    if (e.value.ToString() == "@")
                        Console.Write("ε");
                    else
                        Console.Write(e.value.ToString());

                    if (e.index < gen.FirstSet.Get(sym).Count - 1)
                        Console.Write(", ");
                }

                Console.WriteLine(" }");
            }

            Console.WriteLine("========================= Follow Set ========================");
            foreach (var sym in grammar.NonTerminals)
            {
                Console.Write("Follow(" + sym.ToString() + ") = { ");
                foreach (var e in gen.FollowSet.Get(sym).Select((value, index) => new { value, index }))
                {
                    if (e.value.ToString() == "@")
                        Console.Write("ε");
                    else
                        Console.Write(e.value.ToString());

                    if (e.index < gen.FollowSet.Get(sym).Count - 1)
                        Console.Write(", ");
                }

                Console.WriteLine(" }");
            }

            var LR0coll = new LR0Collection(grammar);
            var LR1coll = new LR1Collection(grammar);

            /*
            Console.WriteLine("============== LR(0) Collection ============================");
            var LR0curIndex = -1;
            foreach (var e in LR0coll.Items().Select((value, index) => new { index, value }))
            {
                if (LR0curIndex != e.index)
                {
                    Console.WriteLine("==================== I[" + e.index + "] ====================");
                    LR0curIndex = e.index;
                }

                foreach (var i in e.value)
                    Console.WriteLine(i);
            }

            Console.WriteLine("============== LR(1) Collection ============================");
            var LR1curIndex = -1;
            foreach (var e in LR1coll.Items().Select((value, index) => new { index, value }))
            {
                if (LR1curIndex != e.index)
                {
                    Console.WriteLine("==================== I[" + e.index + "] ====================");
                    LR1curIndex = e.index;
                }

                foreach (var i in e.value)
                    Console.WriteLine(i);
            }
            */

            var slrPT = SLRParseTable.Create(LR0coll);
            Console.WriteLine("======================== Select Prefer Entry =======================");
            foreach (var pend in slrPT.Action.Select((value, index) => new { index, value }))
                foreach (var term in grammar.TerminalsWithEndMarker)
                    if (!pend.value.Value[term].PreferedEntrySpecified)
                    {
                        Console.WriteLine("=========I[" + pend.index + "]==============");
                        foreach (var e in LR0coll.Items().Select((value, index) => new { index, value }).Where(e => e.index == pend.index))
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
                                foreach (var col in LR0coll.Items().Select((value, index) => new { index, value }).Where(c => c.index == e.value.ShiftState))
                                    foreach (var it in col.value)
                                        Console.WriteLine(it);
                            }
                        }
                            

                        int sel = int.Parse(Console.ReadLine());
                        pend.value.Value[term].SetPreferEntry(pend.value.Value[term].Entries.ToList()[sel]);
                    }


            
            Console.WriteLine("================== Action Table ===================================");
            for (int i = 0; i < slrPT.StateCount; i++)
            {
                foreach (var term in grammar.TerminalsWithEndMarker)
                    Console.WriteLine("ACTION[{0}, {1}]={2}", i, term, slrPT.Action[i][term]);
                Console.WriteLine("===============================================================");
            }

            Console.WriteLine("================== Goto Table ===================================");
            for (int i = 0; i < slrPT.StateCount; i++)
            {
                foreach (var nonTerm in grammar.NonTerminalsWithoutAugmentedS)
                    Console.WriteLine("GOTO[{0}, {1}]={2}", i, nonTerm, slrPT.Goto[i][nonTerm]);
                Console.WriteLine("===============================================================");
            }

            Console.ReadLine();
        }

        private static void TestDanglingElse()
        {
            Grammar grammar = new Grammar();
            //using (var stream = new FileStream("Grammar-4.3.txt", FileMode.Open))
            using (var stream = new FileStream("Grammar-4.67.txt", FileMode.Open))
                grammar.Parse(stream);

            var LR0Coll = new LR0Collection(grammar);
            Console.WriteLine("============== LR(0) Collection ============================");
            var LR0curIndex = -1;
            foreach (var e in LR0Coll.Items().Select((value, index) => new { index, value }))
            {
                if (LR0curIndex != e.index)
                {
                    Console.WriteLine("==================== I[" + e.index + "] ====================");
                    LR0curIndex = e.index;
                }

                foreach (var i in e.value)
                    Console.WriteLine(i);
            }

            var slrPT = SLRParseTable.Create(LR0Coll);

            Console.WriteLine("======================== Select Prefer Entry =======================");
            foreach (var pend in slrPT.Action.Select((value, index) => new { index, value }))
                foreach (var term in grammar.TerminalsWithEndMarker)
                    if (!pend.value.Value[term].PreferedEntrySpecified)
                    {
                        Console.WriteLine("=========I[" + pend.index + "]==============");
                        foreach (var e in LR0Coll.Items().Select((value, index) => new { index, value }).Where(e => e.index == pend.index))
                            foreach (var it in e.value)
                                Console.WriteLine(it);

                        Console.WriteLine("============================================");
                        Console.WriteLine("Select for ACTION[" + pend.index + ", " + term + "]");
                        Console.WriteLine("=========== Actions pending =======");
                        foreach (var e in pend.value.Value[term].Entries.Select((value, index) => new { index, value }))
                            Console.WriteLine(e.index.ToString() + "." + e.value);

                        int sel = int.Parse(Console.ReadLine());
                        pend.value.Value[term].SetPreferEntry(pend.value.Value[term].Entries.ToList()[sel]);
                    }

            Console.WriteLine("================== Action Table ===================================");
            for (int i = 0; i < slrPT.StateCount; i++)
            {
                foreach (var term in grammar.TerminalsWithEndMarker)
                    Console.WriteLine("ACTION[{0}, {1}]={2}", i, term, slrPT.Action[i][term]);
                Console.WriteLine("===============================================================");
            }

            Console.WriteLine("================== Goto Table ===================================");
            for (int i = 0; i < slrPT.StateCount; i++)
            {
                foreach (var nonTerm in grammar.NonTerminalsWithoutAugmentedS)
                    Console.WriteLine("GOTO[{0}, {1}]={2}", i, nonTerm, slrPT.Goto[i][nonTerm]);
                Console.WriteLine("===============================================================");
            }

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            //TestFirstFollow();
            TestSLRParser();
            //TestLR1Collection();
            //TestDanglingElse();

            //TestExperiment_SLR();
        }
    }
}
