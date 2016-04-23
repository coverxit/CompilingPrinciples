using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CompilingPrinciples.ParserModule
{ 
    [Serializable]
    public class Production
    {
        [NonSerialized]
        private Grammar grammar;

        private int leftNonTerminalId;
        private List<ProductionSymbol> rightExpression;

        [NonSerialized]
        private int line; // used for showing short string
        
        public int LineInGrammar
        {
            get { return line; }
        }

        public ProductionSymbol Left
        {
            get { return new ProductionSymbol(grammar, ProductionSymbol.SymbolType.NonTerminal, leftNonTerminalId); }
        }
     
        public List<ProductionSymbol> Right
        {
            get { return new List<ProductionSymbol>(rightExpression); }
        }

        public Production(Grammar grammar, int line = -1)
        {
            this.line = line;
            this.grammar = grammar;
            rightExpression = new List<ProductionSymbol>();
        }

        public Production(Production rhs)
        {
            this.line = rhs.line;
            this.grammar = rhs.grammar;
            this.leftNonTerminalId = rhs.leftNonTerminalId;
            this.rightExpression = new List<ProductionSymbol>(rhs.rightExpression);
        }

        internal void SetLeftNonTerminal(int id)
        {
            this.leftNonTerminalId = id;
        }

        public bool IsRightEpsilon()
        {
            return Right.Count == 1 && Right[0].Equals(grammar.Epsilon);
        }

        internal void AppendRightSymbol(ProductionSymbol.SymbolType type, int id)
        {
            rightExpression.Add(new ProductionSymbol(grammar, type, id));
        }

        internal void AppendRightSymbol(ProductionSymbol sym)
        {
            rightExpression.Add(sym);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Production rhs = obj as Production;
            return rhs.grammar == grammar && rhs.ToString() == this.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Left);
            sb.Append(" ->");

            foreach (var e in rightExpression)
            {
                sb.Append(" ");
                sb.Append(e.ToString());
            }

            return sb.ToString();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.grammar = context.Context as Grammar;
        }
    }
}
