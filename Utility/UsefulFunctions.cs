using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.Utility
{
    public static class UsefulFunctions
    {
        public static string HashSetToMultilineString<T>(HashSet<T> hashSet)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e in hashSet)
                sb.Append(e.ToString() + "\r\n");
            return sb.ToString(0, sb.Length - 2);
        }
    }
}
