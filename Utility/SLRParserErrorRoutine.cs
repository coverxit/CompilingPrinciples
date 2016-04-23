using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserModule;

namespace CompilingPrinciples.Utility
{
    public class SLRParserErrorRoutine : IParserErrorRoutine
    {
        private const int StateCount = 58;

        private Dictionary<int, Dictionary<string, SolveOperation>> procs = new Dictionary<int, Dictionary<string, SolveOperation>>();

        public SLRParserErrorRoutine()
        {
            // init dict
            for (int i = 0; i < StateCount; i++)
                procs.Add(i, new Dictionary<string, SolveOperation>());

            procs[0][";"] = SolveOperation.SkipToken("empty statement");
            procs[0]["$"] = SolveOperation.PushState(1, "empty program");
        }

        public SolveOperation ErrorRoutine(int topState, ProductionSymbol symbol, Token currentToken, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack)
        {
            return procs[topState][symbol.ToString()];
        }
    }
}
