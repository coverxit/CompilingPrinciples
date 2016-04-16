﻿using System;
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

        private static void TestLR0Collection()
        {
            Grammar grammar = new Grammar();
            using (var stream = new FileStream("Grammar-4.40.txt", FileMode.Open))
                grammar.Parse(stream);

            var coll = new LR0Collection(grammar);

            // Cope with file Grammar-4.40.txt
            Console.WriteLine("============== Closure({E' -> ·E}) ===============");
            var closure = coll.Closure(new HashSet<LR0Item>(new LR0Item[] { new LR0Item(grammar, grammar.Productions[0], 0) }));
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
            var closure = coll.Closure(new HashSet<LR1Item>(new LR1Item[] { new LR1Item(grammar, grammar.Productions[0], 0, grammar.EndMarker) }));
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

        private static void TestExperiment()
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

            var LR1coll = new LR1Collection(grammar);
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

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            //TestFirstFollow();
            //TestLR0Collection();
            //TestLR1Collection();

            TestExperiment();
        }
    }
}
