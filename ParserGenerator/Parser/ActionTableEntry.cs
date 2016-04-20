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
            this.reduceProd = rhs.reduceProd;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var rhs = obj as ActionTableEntry;
            return rhs.ToString() == this.ToString();
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
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

    public class MultipleEntry
    {
        private HashSet<ActionTableEntry> entries;
 
        private ActionTableEntry preferEntry = null;
        
        public HashSet<ActionTableEntry> Entries
        {
            get { return new HashSet<ActionTableEntry>(entries); }
        }

        public int Count
        {
            get { return entries.Count; }
        }

        public bool PreferedEntrySpecified
        {
            get { return preferEntry != null; }
        }

        public MultipleEntry()
        {
            this.entries = new HashSet<ActionTableEntry>();
        }

        public ActionTableEntry PreferEntry
        {
            get
            {
                if (preferEntry == null)
                    throw new ApplicationException("Prefer entry not specified.");
    
                return new ActionTableEntry(preferEntry);
            }
        }

        public void Add(ActionTableEntry entry)
        {
            preferEntry = entry;
            entries.Add(entry);

            // Multiple Entries
            if (entries.Count > 1)
                preferEntry = null;
        }

        public void SetPreferEntry(ActionTableEntry entry)
        {
            preferEntry = entry;
        }

        public override string ToString()
        {
            return PreferEntry.ToString();
        }
    }
}
