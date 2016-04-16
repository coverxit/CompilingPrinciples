using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseGenerator
{
    public class ParseTable
    {
        public const int ErrorGotoState = -1;

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

        protected int initialState;
        public int InitialState
        {
            get { return initialState; }
        }

        public int StateCount
        {
            get { return actionTable.Count; }
        }

        // Use Factory Pattern to create
        protected ParseTable()
        {
            actionTable = new Dictionary<int, Dictionary<ProductionSymbol, MultipleEntry>>();
            gotoTable = new Dictionary<int, Dictionary<ProductionSymbol, int>>();
        }
    }
}
