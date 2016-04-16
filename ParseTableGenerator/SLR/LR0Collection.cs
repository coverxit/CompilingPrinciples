using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseTableGenerator
{
    class LR0Collection
    {
        private Grammar grammar;

        public LR0Collection(Grammar grammar) { this.grammar = grammar; }

        public HashSet<LR0Item> Closure(HashSet<LR0Item> I)
        {
            var remainingSymbols = grammar.NonTerminals;
            var closure = new HashSet<LR0Item>(I);
            var computeStack = new Stack<LR0Item>();

            // Push each A -> α·Bβ into stack
            foreach (var item in closure.Where(e => e.SymbolAfterDot.Type == ProductionSymbol.SymbolType.NonTerminal))
                computeStack.Push(item);

            while (computeStack.Count > 0)
            {
                var topItem = computeStack.Pop();

                // Prodcution starts with B not added
                if (remainingSymbols.Contains(topItem.SymbolAfterDot))
                {
                    // Each B -> γ
                    foreach (var prod in grammar.Productions.Where(e => e.Left.Equals(topItem.SymbolAfterDot)))
                    {
                        // Add B -> ·γ into closure
                        var item = new LR0Item(grammar, prod, 0);
                        closure.Add(item);

                        // If B -> ·γ is something like B -> ·Cβ, then push into stack
                        if (item.SymbolAfterDot.Type == ProductionSymbol.SymbolType.NonTerminal)
                            computeStack.Push(item);
                    }

                    // All B -> γ has been added
                    remainingSymbols.Remove(topItem.SymbolAfterDot);
                }
            }

            return closure;
        }

        public HashSet<LR0Item> Goto(HashSet<LR0Item> I)
        {
            var gotoSet = new HashSet<LR0Item>();
            foreach (var item in I)
                gotoSet.Add(new LR0Item(grammar, item.Production, item.DotPosition + 1));
            return gotoSet;
        }
    }
}
