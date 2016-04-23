using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserModule;

namespace CompilingPrinciples.Utility
{
    public class LR1ParserErrorRoutine : IParserErrorRoutine
    {
        public string ErrorRoutine(int topState, ProductionSymbol symbol, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack)
        {
            throw new NotImplementedException();
        }
    }
}
