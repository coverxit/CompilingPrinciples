using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.SyntaxAnalyzer
{
    [Serializable]
    public class SLRParseTable : ParseTable<LR0Item>
    {
        // Use Factory Pattern to create
        protected SLRParseTable(LR0Collection collection, LR0Item initialItem, IReportProgress reporter = null) : base(collection, initialItem, reporter) { }

        protected override void GenerateActionTable()
        {
            foreach (var e in itemsList.Select((value, index) => new { index, value }))
            {
                if (reporter != null) reporter.ReportProgress("Generating ACTION table: " + e.index + "/" + itemsList.Count + "...");

                // Foreach [A -> α·aβ] in Ii, and GOTO(Ii, a)=Ij, then
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

                // Foreach [A -> α·] in Ii, set ACTION[i, a] to "reduce A -> α"
                // for all a in FOLLOW(A), A is not S'
                // Dont forget check [A -> ·, a].
                foreach (var item in e.value.Where(i => !i.Production.Left.Equals(collection.Grammar.FirstProduction.Left))
                                            .Where(i => i.SymbolAfterDot.Equals(collection.Grammar.EndMarker) || i.SymbolAfterDot.Equals(collection.Grammar.Epsilon)))
                {
                    // For all a in Follow(A), set ACTION[i, a] to "reduct A -> α"
                    foreach (var a in collection.Follow.Get(item.Production.Left))
                        AddToActionTable(e.index, a, ActionTableEntry.Reduce(item.Production));
                }

                // If [S' -> S·] in Ii, then set ACTION[i, $] to "accept"
                if (e.value.Contains(new LR0Item(collection.Grammar, collection.Grammar.FirstProduction, 1)))
                    AddToActionTable(e.index, collection.Grammar.EndMarker, ActionTableEntry.Accept());
            }
        }

        public static SLRParseTable Create(LR0Collection collection, IReportProgress reporter = null)
        {
            return new SLRParseTable(collection, 
                                     new LR0Item(collection.Grammar, collection.Grammar.FirstProduction, 0),
                                     reporter);
        }
    }
}
