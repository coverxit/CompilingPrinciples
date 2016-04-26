using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserCore;

namespace CompilingPrinciples.Utility
{
    public class LR1PhaseLevelParserErrorRoutine : IPhaseLevelParserErrorRoutine
    {
        public PhaseLevelOperation ErrorRoutine(int topState, ProductionSymbol symbol, Token currentToken, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack)
        {
            throw new NotImplementedException();
        }
    }
}
