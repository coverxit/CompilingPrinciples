using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseGenerator
{
    public class ActionTableEntry
    {
        public enum ActionType
        {
            Shift = 0,
            Reduce = 1,
            Accept = 2,
            Error = 3
        }

        private ActionType type;
        public ActionType Type
        {
            get { return type; }
        }

        private int value;
        public int Value
        {
            get { return value; }
        }

        public ActionTableEntry(ActionType type, int value)
        {
            this.type = type;
            this.value = value;
        }

        public ActionTableEntry(ActionTableEntry rhs)
        {
            this.type = rhs.type;
            this.value = rhs.value;
        }
    }

    public abstract class ParseTable
    {
        protected Dictionary<int, Dictionary<ProductionSymbol, ActionTableEntry>> actionTable;
        protected Dictionary<int, Dictionary<ProductionSymbol, int>> gotoTable;

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
