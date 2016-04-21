using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompilingPrinciples.LexicalAnalyzer;

namespace CompilingPrinciples.SyntaxAnalyzer
{
    public interface IParserErrorRoutine
    {
        string ErrorRoutine(int state, ProductionSymbol symbol, Token previousToken, Stack<int> parseStack, SymbolStack symbolStack);
    }
}
