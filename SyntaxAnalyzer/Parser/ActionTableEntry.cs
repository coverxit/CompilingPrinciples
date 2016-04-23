using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.SyntaxAnalyzer
{
    [Serializable]
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
        private int shiftState;
        private Production reduceProd;

        public ActionType Type
        {
            get { return type; }
        }

        public int ShiftState
        {
            get { return shiftState; }
        }

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

        public string ToShortString()
        {
            switch (type)
            {
                case ActionType.Shift:
                    return "s" + shiftState;

                case ActionType.Reduce:
                    return "r" + reduceProd.LineInGrammar;

                case ActionType.Accept:
                    return "acc";

                case ActionType.Error:
                    return "err";

                default:
                    throw new ApplicationException("Action Type Mismatch.");
            }
        }
    }

    [Serializable]
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

        public ActionTableEntry PreferredEntry
        {
            get { return preferEntry == null ? null : new ActionTableEntry(preferEntry); }
            /*
            get
            {
                if (preferEntry == null)
                    throw new ApplicationException("Prefer entry not specified.");
    
                return new ActionTableEntry(preferEntry);
            }
            */
        }

        public ActionTableEntry FirstEntry
        {
            get { return new ActionTableEntry(entries.First()); }
        }

        public void Add(ActionTableEntry entry)
        {
            preferEntry = entry;
            entries.Add(entry);

            // Multiple Entries
            if (entries.Count > 1)
                preferEntry = null;
        }

        public void SetPreferEntry(int id)
        {
            preferEntry = entries.Select((value, index) => new { index, value })
                                 .Where(e => e.index == id).Single().value;
        }

        public void SetPreferEntry(ActionTableEntry entry)
        {
            preferEntry = entry;
        }

        public override string ToString()
        {
            return PreferredEntry.ToString();
        }
    }
}
