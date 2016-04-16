using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseGenerator
{
    public class SLRParseTable : ParseTable
    {
        private static void AddToActionTable(ref Dictionary<int, Dictionary<ProductionSymbol, MultipleEntry>> actionTable, int state, ProductionSymbol sym, ActionTableEntry action)
        {
            if (!actionTable.ContainsKey(state))
                actionTable.Add(state, new Dictionary<ProductionSymbol, MultipleEntry>());

            if (!actionTable[state].ContainsKey(sym))
                actionTable[state].Add(sym, new MultipleEntry());

            actionTable[state][sym].Add(action);
        }

        private static void GenerateActionTable(LR0Collection collection, List<HashSet<LR0Item>> itemsList, ref SLRParseTable parseTable)
        {
            foreach (var e in itemsList.Select((value, index) => new { index, value }))
            {
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
                    AddToActionTable(ref parseTable.actionTable, e.index, a, ActionTableEntry.Shift(j));
                }

                // Foreach [A -> α·] in Ii, set ACTION[i, a] to "reduce A -> α"
                // for all a in FOLLOW(A), A may not be S'
                foreach (var item in e.value.Where(i => !i.Production.Left.Equals(collection.Grammar.FirstProduction.Left))
                                            .Where(i => i.SymbolAfterDot.Equals(collection.Grammar.EndMarker)))
                {
                    // For all a in Follow(A), set ACTION[i, a] to "reduct A -> α"
                    foreach (var a in collection.Follow.Get(item.Production.Left))
                        AddToActionTable(ref parseTable.actionTable, e.index, a, ActionTableEntry.Reduce(item.Production));
                }

                // If [S' -> S·] in Ii, then set ACTION[i, $] to "accept"
                if (e.value.Contains(new LR0Item(collection.Grammar, collection.Grammar.FirstProduction, 1)))
                    AddToActionTable(ref parseTable.actionTable, e.index, collection.Grammar.EndMarker, ActionTableEntry.Accept());
            }
        }

        private static void GenerateGotoTable(LR0Collection collection, List<HashSet<LR0Item>> itemsList, ref SLRParseTable parseTable)
        {
            // If GOTO(Ii,A)=Ij, then GOTO[i, A]=j, for all nontermianls A
            foreach (var e in itemsList.Select((value, index) => new { index, value }))
                foreach (var A in collection.Grammar.NonTerminals)
                {
                    // GOTO(Ii,A)=Ij, if GOTO(Ii,A) is empty, then j = -1, marks error.
                    var j = itemsList.Select((value, index) => new { index, value })
                                    .Where(i => i.value.SetEquals(collection.Goto(e.value, A)))
                                    .DefaultIfEmpty(new { index = ParseTable.ErrorGotoState, value = new HashSet<LR0Item>() })
                                    .SingleOrDefault().index;

                    // Set GOTO[i, A] = j
                    if (!parseTable.gotoTable.ContainsKey(e.index))
                        parseTable.gotoTable.Add(e.index, new Dictionary<ProductionSymbol, int>());

                    parseTable.gotoTable[e.index].Add(A, j);
                }
        }

        private static void FillErrors(LR0Collection collection, ref SLRParseTable parseTable)
        {
            // All entries not defined yet are made "error".
            // Note we have $ here
            foreach (var state in parseTable.actionTable.Keys)
                foreach (var term in collection.Grammar.TerminalsWithEndMarker)
                    if (!parseTable.actionTable[state].ContainsKey(term))
                        AddToActionTable(ref parseTable.actionTable, state, term, ActionTableEntry.Error());

            // The errors in Goto Table has been filled in GenerateGotoTable

            /*
            // Goto Table except S'
            foreach (var state in parseTable.gotoTable.Keys)
                foreach (var nonTerm in collection.Grammar.NonTerminals.Where(nt => nt.Equals(collection.Grammar.AugemntedS)))
                    if (!parseTable.gotoTable[state].ContainsKey(nonTerm))
                        parseTable.gotoTable[state].Add(nonTerm, ParseTable.ErrorGotoState);
            */
        }

        public static SLRParseTable Create(LR0Collection collection)
        {
            var parseTable = new SLRParseTable();
            var itemsList = collection.Items().ToList();
            
            GenerateActionTable(collection, itemsList, ref parseTable);
            GenerateGotoTable(collection, itemsList, ref parseTable);
            FillErrors(collection, ref parseTable);

            // The initial state is the one containing [S' -> ·S]
            parseTable.initialState = itemsList.Select((value, index) => new { index, value })
                                               .Where(e => e.value.Contains(new LR0Item(collection.Grammar, collection.Grammar.FirstProduction, 0)))
                                               .Single().index;

            return parseTable;
        }
    }
}
