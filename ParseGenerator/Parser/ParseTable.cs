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

        protected Dictionary<int, Dictionary<ProductionSymbol, ActionTableEntry>> actionTable;
        protected Dictionary<int, Dictionary<ProductionSymbol, int>> gotoTable;

        protected int initialState;
        public int InitialState
        {
            get { return initialState; }
        }

        // Use Factory Pattern to create
        protected ParseTable()
        {
            actionTable = new Dictionary<int, Dictionary<ProductionSymbol, ActionTableEntry>>();
            gotoTable = new Dictionary<int, Dictionary<ProductionSymbol, int>>();
        }

        public ActionTableEntry Action(int state, ProductionSymbol sym)
        {
            return actionTable[state][sym];
        }

        public int Goto(int state, ProductionSymbol sym)
        {
            return gotoTable[state][sym];
        }
    }
}
