using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParseGenerator
{
    public class Parser
    {
        private ParseTable table;

        public Parser(ParseTable pt)
        {
            this.table = pt;
        }
        
        public void Parse(Stream input)
        {
            // Let a be the first symbol of w$
            int nextByte = input.ReadByte();
            while (true)
            {

            }
        }
    }
}
