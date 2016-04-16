using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParseTableGenerator
{
    class Program
    {
        private static void TestFirstFollow(Grammar grammar)
        {
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

        private static void TestLR0Collection(Grammar grammar)
        {
            var coll = new LR0Collection(grammar);
            var closure = coll.Closure(new HashSet<LR0Item>(new LR0Item[] { new LR0Item(grammar, grammar.Productions[0], 0) }));
            foreach (var c in closure)
                Console.WriteLine(c);

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Grammar grammar = new Grammar();
            using (var stream = new FileStream("Grammar.txt", FileMode.Open))
                grammar.Parse(stream);

            TestFirstFollow(grammar);
            TestLR0Collection(grammar);
        }
    }
}
