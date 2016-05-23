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

        public static List<Tuple<string, string>> ToAddressed(List<string> code)
        {
            var addrCode = new Dictionary<int, string>();
            var labelDict = new Dictionary<string, int>();

            int addr = 100;

            foreach (var instr in code)
            {
                if (instr.StartsWith("L"))
                    labelDict.Add(instr.Substring(0, instr.Length - 1), addr);
                else
                    addrCode.Add(addr++, instr);
            }

            foreach (var key in addrCode.Where(e => e.Value.Contains("goto L")).Select(e => e.Key).ToArray())
            {
                var pos = addrCode[key].LastIndexOf("goto L") + 5;
                var label = addrCode[key].Substring(pos);
                addrCode[key] = addrCode[key].Substring(0, pos) + labelDict[label];
            }

            addrCode.Add(addr, ""); // an empty instr in the last address
            return addrCode.Select((pair) => Tuple.Create(pair.Key.ToString(), pair.Value)).ToList();
        }
    }
}
