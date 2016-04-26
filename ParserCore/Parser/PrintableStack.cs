using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.ParserCore
{
    public class PrintableStack<T>
    {
        private Stack<T> stack = new Stack<T>();

        public Stack<T> InnerStack
        {
            get { return new Stack<T>(stack); }
        }

        public int Count
        {
            get { return stack.Count; }
        }

        public void Push(T sym)
        {
            stack.Push(sym);
        }

        public T Pop()
        {
            return stack.Pop();
        }

        public T Peek()
        {
            return stack.Peek();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e in stack.Reverse())
                sb.Append(e.ToString() + " ");
            return sb.ToString(0, sb.Length - 1);
        }
    }
}
