using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAnalyzer
{ 
    public class Production
    {
        private Grammar grammar;

        private int leftNonTerminalId;
        public ProductionSymbol Left
        {
            get { return new ProductionSymbol(grammar, ProductionSymbol.SymbolType.NonTerminal, leftNonTerminalId); }
        }

        private List<ProductionSymbol> rightExpression;
        public List<ProductionSymbol> Right
        {
            get { return new List<ProductionSymbol>(rightExpression); }
        }

        public Production(Grammar grammar)
        {
            this.grammar = grammar;
            rightExpression = new List<ProductionSymbol>();
        }

        public Production(Production rhs)
        {
            this.grammar = rhs.grammar;
            this.leftNonTerminalId = rhs.leftNonTerminalId;
            this.rightExpression = new List<ProductionSymbol>(rhs.rightExpression);
        }

        public void SetLeftNonTerminal(int id)
        {
            this.leftNonTerminalId = id;
        }

        public bool IsRightEpsilon()
        {
            return Right.Count == 1 && Right[0].Equals(grammar.Epsilon);
        }

        public void AppendRightSymbol(ProductionSymbol.SymbolType type, int id)
        {
            rightExpression.Add(new ProductionSymbol(grammar, type, id));
        }

        public void AppendRightSymbol(ProductionSymbol sym)
        {
            rightExpression.Add(new ProductionSymbol(sym));
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
                sb.Append(e);
            }
            
            return sb.ToString();
        }
    }
}
