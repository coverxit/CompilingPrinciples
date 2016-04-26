using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.ParserCore
{
    [Serializable]
    public class LR1ParseTable : ParseTable<LR1Item>
    {
        // Use Factory Pattern to create
        protected LR1ParseTable(LR1Collection collection, LR1Item initialItem, IReportProgress reporter = null) : base(collection, initialItem, reporter) { }

        protected override void GenerateActionTable()
        {
            foreach (var e in itemsList.Select((value, index) => new { index, value }))
            {
                if (reporter != null) reporter.ReportProgress("Generating ACTION table: " + e.index + "/" + itemsList.Count + "...");
 
                // Foreach [A -> α·aβ, b] in Ii, and GOTO(Ii, a)=Ij, then
                // ACTION[i, a] is "shift j", a must be temrinal
                foreach (var item in e.value.Where(i => !i.SymbolAfterDot.Equals(collection.Grammar.EndMarker)
                                                        && i.SymbolAfterDot.Type == ProductionSymbol.SymbolType.Terminal))
                {
                    var a = item.SymbolAfterDot;
                    // GOTO(Ii, a)=Ij
                    var j = itemsList.Select((value, index) => new { index, value })
                                     .Where(i => i.value.SetEquals(collection.Goto(e.value, a)))
                                     .Single().index;

                    // Set ACTION[i, a] to "shift j"
                    AddToActionTable(e.index, a, ActionTableEntry.Shift(j));
                }

                // Foreach [A -> α·, a] in Ii, A is not S', set ACTION[i, a] to "reduce A -> α"
                // Dont forget check [A -> ·, a].
                foreach (var item in e.value.Where(i => !i.Production.Left.Equals(collection.Grammar.FirstProduction.Left))
                                            .Where(i => i.SymbolAfterDot.Equals(collection.Grammar.EndMarker) || i.SymbolAfterDot.Equals(collection.Grammar.Epsilon)))
                    AddToActionTable(e.index, item.Lookahead, ActionTableEntry.Reduce(item.Production));

                // If [S' -> S·, $] in Ii, then set ACTION[i, $] to "accept"
                if (e.value.Contains(new LR1Item(collection.Grammar, collection.Grammar.FirstProduction, 1, collection.Grammar.EndMarker)))
                    AddToActionTable(e.index, collection.Grammar.EndMarker, ActionTableEntry.Accept());
            }
        }

        public static LR1ParseTable Create(LR1Collection collection, IReportProgress reporter = null)
        {
            return new LR1ParseTable(collection, 
                                     new LR1Item(collection.Grammar, collection.Grammar.FirstProduction, 0, collection.Grammar.EndMarker),
                                     reporter);
        }
    }
}
