using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.IntermediateCodeGenCore
{
    public class IntermediateCodeGenException : Exception
    {
        private int line;
        private string reason;

        public IntermediateCodeGenException(int line, string reason)
        {
            this.line = line;
            this.reason = reason;
        }

        public override string ToString()
        {
            return "line " + line + ": " + reason;
        }
    }
}
