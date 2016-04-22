using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.SyntaxAnalyzer
{
    public class PrintableStack<T>
    {
        private Stack<T> symbolStack = new Stack<T>();

        public int Count
        {
            get { return symbolStack.Count; }
        }

        public void Push(T sym)
        {
            symbolStack.Push(sym);
        }

        public T Pop()
        {
            return symbolStack.Pop();
        }

        public T Peek()
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
