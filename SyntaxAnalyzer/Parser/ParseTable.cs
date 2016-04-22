using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.SyntaxAnalyzer
{
    [Serializable]
    public abstract class ParseTable
    {
        public const int ErrorGotoState = -1;

        protected int initialState;
        protected Dictionary<int, Dictionary<ProductionSymbol, MultipleEntry>> actionTable;
        protected Dictionary<int, Dictionary<ProductionSymbol, int>> gotoTable;

        public Dictionary<int, Dictionary<ProductionSymbol, MultipleEntry>> Action
        {
            get { return new Dictionary<int, Dictionary<ProductionSymbol, MultipleEntry>>(actionTable); }
        }

        public Dictionary<int, Dictionary<ProductionSymbol, int>> Goto
        {
            get { return new Dictionary<int, Dictionary<ProductionSymbol, int>>(gotoTable); }
        }

        public int InitialState
        {
            get { return initialState; }
        }

        public int StateCount
        {
            get { return actionTable.Count; }
        }
    }

    [Serializable]
    public abstract class ParseTable<T> : ParseTable where T: LR0Item
    {
        protected LRCollection<T> collection;
        protected List<HashSet<T>> itemsList;

        [NonSerialized]
        protected IReportProgress reporter;

        public Type ItemType
        {
            get { return typeof(T); }
        }

        public List<HashSet<T>> Items
        {
            get { return new List<HashSet<T>>(itemsList); }
        }

        // Use Factory Pattern to create
        protected ParseTable(LRCollection<T> collection, T initialItem, IReportProgress reporter = null)
        {
            actionTable = new Dictionary<int, Dictionary<ProductionSymbol, MultipleEntry>>();
            gotoTable = new Dictionary<int, Dictionary<ProductionSymbol, int>>();

            this.collection = collection;
            this.itemsList = collection.Items().ToList();
            this.reporter = reporter;

            GenerateActionTable();
            GenerateGotoTable();
            FillErrors();
            SetInitialState(initialItem);
        }

        protected void AddToActionTable(int state, ProductionSymbol sym, ActionTableEntry action)
        {
            if (!actionTable.ContainsKey(state))
                actionTable.Add(state, new Dictionary<ProductionSymbol, MultipleEntry>());

            if (!actionTable[state].ContainsKey(sym))
                actionTable[state].Add(sym, new MultipleEntry());

            actionTable[state][sym].Add(action);
        }

        protected void GenerateGotoTable()
        {
            // If GOTO(Ii,A)=Ij, then GOTO[i, A]=j, for all nontermianls A
            foreach (var e in itemsList.Select((value, index) => new { index, value }))
            {
                if (reporter != null) reporter.ReportProgress("Generting GOTO table: " + e.index + "/" + itemsList.Count + "...");

                foreach (var A in collection.Grammar.NonTerminals)
                {
                    // GOTO(Ii,A)=Ij, if GOTO(Ii,A) is empty, then j = -1, marks error.
                    var j = itemsList.Select((value, index) => new { index, value })
                                    .Where(i => i.value.SetEquals(collection.Goto(e.value, A)))
                                    .DefaultIfEmpty(new { index = ErrorGotoState, value = new HashSet<T>() })
                                    .SingleOrDefault().index;

                    // Set GOTO[i, A] = j
                    if (!gotoTable.ContainsKey(e.index))
                        gotoTable.Add(e.index, new Dictionary<ProductionSymbol, int>());

                    gotoTable[e.index].Add(A, j);
                }
            }
        }

        protected void FillErrors()
        {
            // All entries not defined yet are made "error".
            // Note we have $ here
            foreach (var state in actionTable.Keys)
                foreach (var term in collection.Grammar.TerminalsWithEndMarker)
                    if (!actionTable[state].ContainsKey(term))
                        AddToActionTable(state, term, ActionTableEntry.Error());

            // The errors in Goto Table has been filled in GenerateGotoTable
        }

        public void SetInitialState(T initialItem)
        {
            // The initial state is the one containing [S' -> ·S]
            initialState = itemsList.Select((value, index) => new { index, value })
                                    .Where(e => e.value.Contains(initialItem))
                                    .Single().index;
        }
        
        protected abstract void GenerateActionTable();
    }
}
