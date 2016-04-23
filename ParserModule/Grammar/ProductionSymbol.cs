using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using CompilingPrinciples.LexerModule;

namespace CompilingPrinciples.ParserModule
{
    [Serializable]
    public class ProductionSymbol
    {
        public enum SymbolType
        {
            Terminal = 0,
            NonTerminal = 1
        }

        private SymbolType type;
        private int id;

        [NonSerialized]
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

        public ProductionSymbol(Grammar grammar, Token token)
        {
            this.grammar = grammar;          
            this.type = SymbolType.Terminal;
            this.id = grammar.GetIdInTerminalTable(token);
        }

        public ProductionSymbol(ProductionSymbol rhs)
        {
            this.grammar = rhs.grammar;
            this.type = rhs.type;
            this.id = rhs.id;
        }

        public override int GetHashCode()
        {
            return new { grammar, type, id }.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ProductionSymbol rhs = obj as ProductionSymbol;
            return rhs.grammar == grammar && rhs.type == type && rhs.id == id;
        }

        public override string ToString()
        {
            switch (type)
            {
                case SymbolType.Terminal:
                    return grammar.TerminalTable[id] == "@" ? "ε" : grammar.TerminalTable[id];

                case SymbolType.NonTerminal:
                    return grammar.NonTerminalTable[id];

                default: // Should never be called
                    return null;
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.grammar = context.Context as Grammar;
        }
    }
}
