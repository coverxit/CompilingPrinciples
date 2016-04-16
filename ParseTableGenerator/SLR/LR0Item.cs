using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseTableGenerator
{
    class LR0Item
    {
        private Grammar grammar;
        private Production production;

        public Production Production
        {
            get { return new Production(production); }
        }

        private int dotPos = 0;
        public int DotPosition
        {
            get { return dotPos; }
        }

        public ProductionSymbol SymbolAfterDot
        {
            get { return production.Right[dotPos]; }
        }

        public LR0Item(Grammar grammar, Production prod, int dotPos)
        {
            this.grammar = grammar;
            this.production = prod;
            this.dotPos = dotPos;
        }

        public void SetDotPosition(int pos)
        {
            this.dotPos = pos;
        }

        public override int GetHashCode()
        {
            return new { grammar, production, dotPos }.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj is LR0Item)
                return false;

            var rhs = obj as LR0Item;
            return rhs.grammar == grammar && rhs.production == production && rhs.dotPos == dotPos;
        }

        public override string ToString()
        {
            return production.ToString().Insert(production.ToString().IndexOf("->") + 3 + dotPos, "·");
        }
    }
}
