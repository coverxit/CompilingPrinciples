using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.ParserCore
{
    public class PrintableStack<T> : Stack<T>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e in this.Reverse())
                sb.Append(e.ToString() + " ");
            return sb.ToString(0, sb.Length - 1);
        }
    }
}
