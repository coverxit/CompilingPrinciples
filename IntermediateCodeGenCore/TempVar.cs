using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.IntermediateCodeGenCore
{
    public class TempVar
    {
        private int no;

        public int Number
        {
            get { return no; }
        }

        public TempVar(int no)
        {
            this.no = no;
        }

        public override string ToString()
        {
            return "t" + no;
        }
    }

    public class TempVarManager
    {
        private int count = 0;

        public TempVar Create()
        {
            return new TempVar(++count);
        }
    }
}
