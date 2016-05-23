using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.Utility
{
    public static class ThreeAddrCodeFormatter
    {
        public static List<string> Format(List<string> code)
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
    }
}
