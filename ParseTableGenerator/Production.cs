using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseTableGenerator
{
    public class Production
    {
        public enum SymbolType
        {
            Terminal = 0,
            NonTerminal = 1
        }
        
        public class Symbol
        {
            private SymbolType type;
            private int id;
            private Grammar grammar;

            public SymbolType Type
            {
                get { return type; }
            }

            public int Id
            {
                get { return id; }
            }

            public Symbol(Grammar grammar, SymbolType type, int id)
            {
                this.grammar = grammar;
                this.type = type;
                this.id = id;
            }

            public Symbol(Symbol rhs)
            {
                this.grammar = rhs.grammar;
                this.type = rhs.type;
                this.id = rhs.id;
            }

            public override int GetHashCode()
            {
                return grammar.GetHashCode() * 100 + type.GetHashCode() * 10 + id.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is Symbol))
                    return false;

                Symbol rhs = obj as Symbol;
                return rhs.grammar == grammar && rhs.type == type && rhs.id == id;
            }

            public override string ToString()
            {
                switch (type)
                {
                    case SymbolType.Terminal:
                        return grammar.TerminalTable[id];

                    case SymbolType.NonTerminal:
                        return grammar.NonTerminalTable[id];

                    default: // Should never be called
                        return null;
                }
            }
        }

        private Grammar grammar;

        private int leftTerminalId;
        public Symbol Left
        {
            get { return new Symbol(grammar, SymbolType.Terminal, leftTerminalId); }
        }

        private List<Symbol> rightExpression;
        public List<Symbol> Right
        {
            get { return new List<Symbol>(rightExpression); }
        }

        public Production(Grammar grammar)
        {
            this.grammar = grammar;
            rightExpression = new List<Symbol>();
        }

        public void SetLeftTerminal(int id)
        {
            this.leftTerminalId = id;
        }

        public void AppendRightSymbol(SymbolType type, int id)
        {
            rightExpression.Add(new Symbol(grammar, type, id));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Left);
            sb.Append(" ->");

            foreach (var e in rightExpression)
            {
                sb.Append(" ");
                sb.Append(e);
            }
            
            return sb.ToString();
        }
    }
}
