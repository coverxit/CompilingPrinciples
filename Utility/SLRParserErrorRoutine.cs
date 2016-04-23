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
            var operators = new string[] { "+", "-", "*", "/" };
            var relations = new string[] { ">", "<", "==", ">=", "<=", "!=" };
            var types = new string[] { "int", "float" };

            // init dict
            for (int i = 0; i < StateCount; i++)
                procs.Add(i, new Dictionary<string, SolveOperation>());

            // State 0
            procs[0][";"] = SolveOperation.SkipToken("empty statement");
            procs[0]["$"] = SolveOperation.PushState(1, "empty program");
            procs[0]["="] = SolveOperation.PushState(2, "missing left operand");
            procs[0]["("] = SolveOperation.SkipToken("unexpected left parenthesis");
            procs[0][")"] = SolveOperation.SkipToken("unexpected right parenthesis");
            procs[0]["else"] = SolveOperation.SkipToken("unexpected `else`");
            procs[0]["do"] = SolveOperation.SkipToken("unexpected `do`");
            foreach (var c in relations)
                procs[0][c] = SolveOperation.SkipToken("unexpected `" + c + "`");
            foreach (var c in operators)
                procs[0][c] = SolveOperation.PushState(2, "missing left operand");
            procs[0]["decimal"] = SolveOperation.SkipToken("unexpected constant");

            // State 3
            procs[3]["$"] = SolveOperation.PushState(7, "missing variable name");
            
            // State 5
            procs[5]["$"] = SolveOperation.PushState(3);

            // State 7
            procs[7]["$"] = SolveOperation.PushState(8, "missing `;`");

            // State 8
            procs[8]["$"] = SolveOperation.PushState(9);
            
        }

        public SolveOperation ErrorRoutine(int topState, ProductionSymbol symbol, Token currentToken, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack)
        {
            return procs[topState][symbol.ToString()];
        }
    }
}
