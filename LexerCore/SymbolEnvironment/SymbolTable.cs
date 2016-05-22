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
        private int offset;

        public string Lexeme
        {
            get { return lexeme; }
        }

        public Tag Tag
        {
            get { return tag; }
        }

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public SymbolTableEntry(string lexeme, Tag tag)
        {
            this.lexeme = lexeme;
            this.tag = tag;
            this.offset = 0;
        }

        public SymbolTableEntry(SymbolTableEntry other)
        {
            this.lexeme = other.lexeme;
            this.tag = other.tag;
        }

        public override string ToString()
        {
            return lexeme;
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

        public SymbolTableEntry Get(int index)
        {
            // Debug.Assert(index >= 0 && index < symbols.Count, "Index Overflow");
            return symbols[index];
        }

        public void Clear()
        {
            symbols.Clear();
        }

        public Tuple<SymbolTableEntry, int> Get(string symbol)
        {
            for (int i = 0; i < symbols.Count; i++)
                if (symbols[i].Lexeme == symbol)
                    return Tuple.Create(symbols[i], i);

            return null;
        }

        public List<SymbolTableEntry> ToList()
        {
            return new List<SymbolTableEntry>(symbols);
        }
    }
}
