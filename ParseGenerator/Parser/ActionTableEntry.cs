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

        private int shiftState;
        public int ShiftState
        {
            get { return shiftState; }
        }

        private Production reduceProd;
        public Production ReduceProduction
        {
            get { return reduceProd; }
        }

        // Use Factory Pattern to create
        private ActionTableEntry() { }

        public static ActionTableEntry Shift(int state)
        {
            var entry = new ActionTableEntry();
            entry.type = ActionType.Shift;
            entry.shiftState = state;
            return entry;
        }

        public static ActionTableEntry Reduce(Production reduceProd)
        {
            var entry = new ActionTableEntry();
            entry.type = ActionType.Reduce;
            entry.reduceProd = new Production(reduceProd);
            return entry;
        }

        public static ActionTableEntry Accept()
        {
            var entry = new ActionTableEntry();
            entry.type = ActionType.Accept;
            return entry;
        }

        public static ActionTableEntry Error()
        {
            var entry = new ActionTableEntry();
            entry.type = ActionType.Error;
            return entry;
        }

        public ActionTableEntry(ActionTableEntry rhs)
        {
            this.type = rhs.type;
            this.shiftState = rhs.shiftState;
        }

        public override string ToString()
        {
            switch (type)
            {
                case ActionType.Shift:
                    return "shift to " + shiftState;

                case ActionType.Reduce:
                    return "reduce by " + reduceProd;

                case ActionType.Accept:
                    return "accept";

                case ActionType.Error:
                    return "error";

                default:
                    throw new ApplicationException("Action Type Mismatch.");
            }
        }
    }
}
