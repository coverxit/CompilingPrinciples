using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.IntermediateCodeGenCore
{
    public class Label
    {
        public static Label Fall = new Label(0);

        private int no;

        public int Number
        {
            get { return no; }
        }

        public bool IsFall
        {
            get { return no == 0; }
        }

        public Label(int no)
        {
            this.no = no;
        }

        public override string ToString()
        {
            return "L" + no;
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
