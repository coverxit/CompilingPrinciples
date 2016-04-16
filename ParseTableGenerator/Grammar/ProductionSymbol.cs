using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseTableGenerator
{
    public class ProductionSymbol
    {
        public enum SymbolType
        {
            Terminal = 0,
            NonTerminal = 1
        }

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

        public ProductionSymbol(Grammar grammar, SymbolType type, int id)
        {
            this.grammar = grammar;
            this.type = type;
            this.id = id;
        }

        public ProductionSymbol(ProductionSymbol rhs)
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
            if (obj == null || !(obj is ProductionSymbol))
                return false;

            ProductionSymbol rhs = obj as ProductionSymbol;
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
}
