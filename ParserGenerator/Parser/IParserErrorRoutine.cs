using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserGenerator
{
    interface IParserErrorRoutine
    {
        string ErrorRoutine(int state, ProductionSymbol symbol);
    }
}
