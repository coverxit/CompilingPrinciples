using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.Utility
{
    public static class ThreeAddrCodeFormatter
    {
        public static List<string> ToPlainText(List<string> code)
        {
            var ret = new List<string>();

            int i = 0;
            var temp = string.Empty;

            while (i < code.Count)
            {
                if (code[i].StartsWith("L")) // Label
                    temp += code[i];
                else
                {
                    temp += "\t" + code[i];
                    ret.Add(temp);
                    temp = string.Empty;
                }
                i++;
            }

            if (temp != string.Empty)
                ret.Add(temp);

            return ret;
        }

        public static List<Tuple<string, string>> ToPair(List<string> code)
        {
            var ret = new List<Tuple<string, string>>();

            int i = 0;
            var label = string.Empty;

            while (i < code.Count)
            {
                if (code[i].StartsWith("L")) // Label
                    label += code[i];
                else
                {
                    ret.Add(Tuple.Create(label, code[i]));
                    label = string.Empty;
                }
                i++;
            }

            if (label != string.Empty)
                ret.Add(Tuple.Create(label, string.Empty));

            return ret;
        }
    }
}
