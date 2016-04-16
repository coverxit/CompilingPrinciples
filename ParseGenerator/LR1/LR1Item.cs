using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseGenerator
{
    public class LR1Item : LR0Item
    {
        private ProductionSymbol lookahead;

        public ProductionSymbol Lookahead
        {
            get { return lookahead; }
        }

        public LR1Item(Grammar grammar, Production prod, int dotPos, ProductionSymbol lookahead) : base(grammar, prod, dotPos)
        {
            this.lookahead = lookahead;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && lookahead.Equals((obj as LR1Item).lookahead);
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
            sb.Append(", " + lookahead.ToString() + "]");
            return sb.ToString();
        }
    }
}
