using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAnalyzer
{
    public class LR1Collection : LRCollection<LR1Item>
    {
        public LR1Collection(Grammar grammar) : base(grammar) { }
        
        public override HashSet<LR1Item> Closure(HashSet<LR1Item> I)
        {
            var closure = new HashSet<LR1Item>(I);
            var computeStack = new Stack<LR1Item>();
            
            // Push each [A -> α·Bβ, a] into stack
            foreach (var item in closure.Where(e => e.SymbolAfterDot.Type == ProductionSymbol.SymbolType.NonTerminal))
                computeStack.Push(item);
            
            while (computeStack.Count > 0)
            {
                var topItem = computeStack.Pop();

                // Compute First(βa) first
                // We genrate a sequence { β, a }
                var sequence = new List<ProductionSymbol>();
                sequence.AddRange(topItem.Production.Right.Skip(topItem.DotPosition + 1));
                sequence.Add(topItem.Lookahead);

                var firstBetaA = new HashSet<ProductionSymbol>();
                foreach (var e in sequence)
                    if (e.Type == ProductionSymbol.SymbolType.Terminal)
                    {
                        firstBetaA.Add(e);
                        break;
                    }
                    else
                    {
                        firstBetaA.UnionWith(firstSet.Get(e));
                        if (!firstSet.Get(e).Contains(grammar.Epsilon))
                            break;
                    }

                // Each production B -> γ
                foreach (var prod in grammar.Productions.Where(e => e.Left.Equals(topItem.SymbolAfterDot)))
                    // Each terminal b in First(βa), add [B -> ·γ, b]
                    foreach (var b in firstBetaA)
                    {
                        // Add [B -> ·γ, b] into closure
                        var item = new LR1Item(grammar, prod, 0, b);
                        
                        // If [B -> ·γ, b] is something like [B -> ·Cβ, b], then push into stack
                        if (!closure.Contains(item) && item.SymbolAfterDot.Type == ProductionSymbol.SymbolType.NonTerminal)
                            computeStack.Push(item);

                        closure.Add(item);
                    }
            }

            return closure;
        }

        public override HashSet<LR1Item> Goto(HashSet<LR1Item> I, ProductionSymbol X)
        {
            var gotoSet = new HashSet<LR1Item>();
            foreach (var item in I.Where(e => e.SymbolAfterDot.Equals(X)))
                gotoSet.Add(new LR1Item(grammar, item.Production, item.DotPosition + 1, item.Lookahead));
            return Closure(gotoSet);
        }

        public override HashSet<HashSet<LR1Item>> Items()
        {
            var collection = new HashSet<HashSet<LR1Item>>();
            var computeStack = new Stack<HashSet<LR1Item>>();

            // C = { CLOSURE( {[S' -> ·S, $]} ) }
            var I0 = Closure(new HashSet<LR1Item>(new LR1Item[] { new LR1Item(grammar, grammar.FirstProduction, 0, grammar.EndMarker) }));
            collection.Add(I0);
            computeStack.Push(I0);

            while (computeStack.Count > 0)
            {
                var topSet = computeStack.Pop();

                // For each grammaer symbol X
                foreach (var sym in grammar.GrammarSymbols)
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
