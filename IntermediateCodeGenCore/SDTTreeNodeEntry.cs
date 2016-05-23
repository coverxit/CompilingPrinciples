using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompilingPrinciples.ParserCore;
using CompilingPrinciples.SymbolEnvironment;

namespace CompilingPrinciples.IntermediateCodeGenCore
{
    public class SDTTreeNodeEntry
    {
        public enum TypeEnum
        {
            Action = 0,
            Symbol = 1
        }

        private TypeEnum type;
        public TypeEnum Type
        {
            get { return type; }
        }

        private ParseTreeNodeEntry symbol;
        public ParseTreeNodeEntry Symbol
        {
            get { return symbol; }
        }

        // Use Tuple for multiple attributes
        private object attrs;
        public object Attributes
        {
            get { return attrs; }
            set { attrs = value; }
        }

        private Action<SDTTreeNodeEntry, SDTTreeNodeEntry[]> action;
        public Action<SDTTreeNodeEntry, SDTTreeNodeEntry[]> Action
        {
            get { return action; }
        }

        public SDTTreeNodeEntry(ParseTreeNodeEntry symbol, object attrs = null)
        {
            this.type = TypeEnum.Symbol;
            this.symbol = symbol;
            this.attrs = attrs;
        }

        public SDTTreeNodeEntry(Action<SDTTreeNodeEntry, SDTTreeNodeEntry[]> action, object data = null)
        {
            this.type = TypeEnum.Action;
            this.action = action;
            this.attrs = data;
        }
    }
}
