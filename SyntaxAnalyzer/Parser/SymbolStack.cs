using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.SyntaxAnalyzer
{
    public class SymbolStack
    {
        private Stack<ProductionSymbol> symbolStack = new Stack<ProductionSymbol>();

        public void Push(ProductionSymbol sym)
        {
            symbolStack.Push(sym);
        }

        public ProductionSymbol Pop()
        {
            return symbolStack.Pop();
        }

        public ProductionSymbol Peek()
        {
            return symbolStack.Peek();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e in symbolStack.Reverse())
                sb.Append(e.ToString() + " ");
            return sb.ToString(0, sb.Length - 1);
        }
    }
}
