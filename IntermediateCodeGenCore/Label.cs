using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.IntermediateCodeGenCore
{
    public class Label
    {
        private int no;

        public int Number
        {
            get { return no; }
        }

        public Label(int no)
        {
            this.no = no;
        }

        public override string ToString()
        {
            return "L" + no + ":";
        }
    }

    public class LabelManager
    {
        private int count = 0;

        public Label Create()
        {
            return new Label(++count);
        }
    }
}
