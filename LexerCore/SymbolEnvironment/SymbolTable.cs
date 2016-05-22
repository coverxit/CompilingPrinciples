using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CompilingPrinciples.LexerCore;

namespace CompilingPrinciples.SymbolEnvironment
{
    public class SymbolTableEntry
    {
        private string lexeme;
        private Tag tag;

        public string Lexeme
        {
            get { return lexeme; }
        }

        public Tag Tag
        {
            get { return tag; }
        }

        public SymbolTableEntry(string lexeme, Tag tag)
        {
            this.lexeme = lexeme;
            this.tag = tag;
        }

        public SymbolTableEntry(SymbolTableEntry other)
        {
            this.lexeme = other.lexeme;
            this.tag = other.tag;
        }
    }

    public class SymbolTable
    {
        private List<SymbolTableEntry> symbols;

        public SymbolTable()
        {
            symbols = new List<SymbolTableEntry>();
        }

        public SymbolTable(SymbolTable rhs)
        {
            symbols = new List<SymbolTableEntry>(rhs.symbols);
        }

        public int Put(string symbol, Tag tag)
        {
            lock (symbols)
            {
                symbols.Add(new SymbolTableEntry(symbol, tag));
                return symbols.Count - 1;
            }
        }

        public string Get(int index)
        {
            // Debug.Assert(index >= 0 && index < symbols.Count, "Index Overflow");
            return symbols[index].Lexeme;
        }

        public void Clear()
        {
            symbols.Clear();
        }

        public Tuple<SymbolTableEntry, int> Get(string symbol)
        {
            for (int i = 0; i < symbols.Count; i++)
                if (symbols[i].Lexeme == symbol)
                    return Tuple.Create(new SymbolTableEntry(symbols[i]), i);

            return null;
        }

        public List<SymbolTableEntry> ToList()
        {
            return new List<SymbolTableEntry>(symbols);
        }
    }
}
