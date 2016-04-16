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

        public HashSet<LR0Item> Goto(HashSet<LR0Item> I, ProductionSymbol X)
        {
            var gotoSet = new HashSet<LR0Item>();
            foreach (var item in I.Where(e => e.SymbolAfterDot.Equals(X)))
                gotoSet.Add(new LR0Item(grammar, item.Production, item.DotPosition + 1));
            return Closure(gotoSet);
        }

        public HashSet<HashSet<LR0Item>> Items()
        {
            var collection = new HashSet<HashSet<LR0Item>>();
            var computeStack = new Stack<HashSet<LR0Item>>();

            // C = { CLOSURE( {[S' -> ·S]} ) }
            var I0 = Closure(new HashSet<LR0Item>(new LR0Item[] { new LR0Item(grammar, grammar.Productions[0], 0) }));
            collection.Add(I0);
            computeStack.Push(I0);
            
            while (computeStack.Count > 0)
            {
                var topSet = computeStack.Pop();

                // For each grammaer symbol X
                foreach (var sym in grammar.Symbols)
                {
                    var gotoSet = Goto(topSet, sym);

                    // If GOTO(I,X) not null and not in C
                    if (gotoSet.Count > 0 && collection.Where(e => e.SetEquals(gotoSet)).Count() == 0)
                    {
                        // Add GOTO(I,X) to C
                        collection.Add(gotoSet);
                        computeStack.Push(gotoSet);
                    }
                }
            }

            return collection;
        }
    }
}
