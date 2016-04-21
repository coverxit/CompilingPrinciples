using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CompilingPrinciples.SyntaxAnalyzer
{
    [Serializable]
    public class LR0Item
    {
        [NonSerialized]
        protected Grammar grammar;

        protected Production production;

        public Production Production
        {
            get { return new Production(production); }
        }

        protected int dotPos = 0;
        public int DotPosition
        {
            get { return dotPos; }
        }

        public ProductionSymbol SymbolAfterDot
        {
            get
            {
                if (dotPos < production.Right.Count)
                    return production.Right[dotPos];
                else // dot is at the right end
                    return grammar.EndMarker;
            }
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
            return this.ToString().GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var rhs = obj as LR0Item;
            return rhs.grammar == grammar && rhs.ToString() == this.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + production.Left + " ->");

            foreach (var e in production.Right.Select((value, index) => new { index, value }))
            {
                sb.Append(" ");
                if (dotPos == e.index) sb.Append("·");
                sb.Append(e.value);
            }

            // Dot is at the right end
            if (dotPos == production.Right.Count) sb.Append("·");
            sb.Append("]");
            return sb.ToString();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.grammar = context.Context as Grammar;
        }
    }
}
