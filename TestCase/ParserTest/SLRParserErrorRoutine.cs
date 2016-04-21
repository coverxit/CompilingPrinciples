using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CompilingPrinciples.LexicalAnalyzer;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.SyntaxAnalyzer;

namespace CompilingPrinciples.TestCase
{
    public class SLRParserErrorRoutine : IParserErrorRoutine
    {
        public string ErrorRoutine(int state, ProductionSymbol symbol, Token previousToken, Stack<int> parseStack, SymbolStack symbolStack)
        {
            throw new NotImplementedException();
        }
    }
}
