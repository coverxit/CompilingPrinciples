using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserCore;

namespace CompilingPrinciples.Utility
{
    public class ExperimentGrammarSLRPhaseLevelParserErrorRoutine : IPhaseLevelParserErrorRoutine
    {
        private const int StateCount = 58;

        private Dictionary<int, Dictionary<string, PhaseLevelOperation>> procs = new Dictionary<int, Dictionary<string, PhaseLevelOperation>>();

        private void SetDefaultOperation(int state, PhaseLevelOperation op)
        {
            var terminals = new string[] 
            {
                "$", "id", ";", "int", "float", "=", "if", "(", ")", "else",
                "while", "do", ">", "<", "==", ">=", "<=", "!=", "+", "-",
                "*", "/", "decimal"
            };

            foreach (var t in terminals)
            {
                var copy = new PhaseLevelOperation(op);
                copy.Diagnostic = string.Format(op.Diagnostic, t);
                procs[state][t] = copy;
            }
        }

        public ExperimentGrammarSLRPhaseLevelParserErrorRoutine()
        {
            var terminals = new string[]
            {
                "$", "id", ";", "int", "float", "=", "if", "(", ")", "else",
                "while", "do", ">", "<", "==", ">=", "<=", "!=", "+", "-",
                "*", "/", "decimal"
            };
            var operators = new string[] { "+", "-", "*", "/" };
            var relations = new string[] { ">", "<", "==", ">=", "<=", "!=" };
            var types = new string[] { "int", "float" };

            // init dict
            for (int i = 0; i < StateCount; i++)
                procs.Add(i, new Dictionary<string, PhaseLevelOperation>());

            // State 0
            SetDefaultOperation(0, PhaseLevelOperation.SkipToken("unexpected `{0}`"));
            procs[0][";"] = PhaseLevelOperation.SkipToken("empty statement");
            procs[0]["$"] = PhaseLevelOperation.PushState(1, "empty program");
            procs[0]["="] = PhaseLevelOperation.PushState(2, "missing left operand");
            foreach (var t in operators)
                procs[0][t] = PhaseLevelOperation.PushState(2, "missing left operand");
            
            // State 2 - 31
            SetDefaultOperation(2, PhaseLevelOperation.SkipToken("unexpected `{0}`"));
            SetDefaultOperation(3, PhaseLevelOperation.PushState(7, "missing variable name"));
            SetDefaultOperation(4, PhaseLevelOperation.ReduceBy(4, ";"));
            SetDefaultOperation(5, PhaseLevelOperation.ReduceBy(5, "id"));
            SetDefaultOperation(6, PhaseLevelOperation.ReduceBy(6, "id"));
            SetDefaultOperation(7, PhaseLevelOperation.PushState(8, "missing `;`"));
            SetDefaultOperation(8, PhaseLevelOperation.ReduceBy(8, "id"));
            SetDefaultOperation(9, PhaseLevelOperation.ReduceBy(9, "id"));
            SetDefaultOperation(10, PhaseLevelOperation.SkipToken("unexpected `{0}`"));
            SetDefaultOperation(11, PhaseLevelOperation.PushState(55, "missing `=`"));
            SetDefaultOperation(12, PhaseLevelOperation.PushState(49, "missing `(`"));
            SetDefaultOperation(13, PhaseLevelOperation.PushState(14, "missing `(`"));
            SetDefaultOperation(14, PhaseLevelOperation.SkipToken("unexpected `{0}`"));
            SetDefaultOperation(15, PhaseLevelOperation.PushState(44, "missing `)`"));
            SetDefaultOperation(16, PhaseLevelOperation.SkipToken("unexpected `{0}`"));
            SetDefaultOperation(17, PhaseLevelOperation.ReduceBy(17, ";"));
            SetDefaultOperation(18, PhaseLevelOperation.ReduceBy(18, ";"));
            SetDefaultOperation(19, PhaseLevelOperation.ReduceBy(19, ";"));
            SetDefaultOperation(20, PhaseLevelOperation.PushState(19, "missing expression"));
            SetDefaultOperation(21, PhaseLevelOperation.ReduceBy(21, ";"));
            SetDefaultOperation(22, PhaseLevelOperation.PushState(23, "missing `)`"));
            SetDefaultOperation(23, PhaseLevelOperation.ReduceBy(23, ";"));
            SetDefaultOperation(24, PhaseLevelOperation.PushState(19, "missing right operand"));
            SetDefaultOperation(25, PhaseLevelOperation.PushState(19, "missing right operand"));
            SetDefaultOperation(26, PhaseLevelOperation.ReduceBy(26, ";"));
            SetDefaultOperation(27, PhaseLevelOperation.PushState(19, "missing right operand"));
            SetDefaultOperation(28, PhaseLevelOperation.PushState(19, "missing right operand"));
            SetDefaultOperation(29, PhaseLevelOperation.ReduceBy(29, ";"));
            SetDefaultOperation(30, PhaseLevelOperation.ReduceBy(30, ";"));
            SetDefaultOperation(31, PhaseLevelOperation.ReduceBy(31, ";"));

            // State 46
            SetDefaultOperation(46, PhaseLevelOperation.ReduceBy(46, "$"));

            // State 48
            SetDefaultOperation(48, PhaseLevelOperation.ReduceBy(48, "$"));

            // State 52 - 57
            SetDefaultOperation(52, PhaseLevelOperation.ReduceBy(52, "$"));
            SetDefaultOperation(53, PhaseLevelOperation.SkipToken("unexpected `{0}`"));
            SetDefaultOperation(54, PhaseLevelOperation.ReduceBy(54, "$"));
            SetDefaultOperation(55, PhaseLevelOperation.PushState(19, "missing right operand"));
            SetDefaultOperation(56, PhaseLevelOperation.PushState(57, "missing `;`"));
            SetDefaultOperation(57, PhaseLevelOperation.ReduceBy(57, "$"));
        }

        public PhaseLevelOperation ErrorRoutine(int topState, ProductionSymbol symbol, Token currentToken, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack)
        {
            // we haven't finish this yet.
            throw new NotImplementedException();

            // return procs[topState][symbol.ToString()];
        }
    }

    public class ExperimentGrammarLR1PhaseLevelParserErrorRoutine : IPhaseLevelParserErrorRoutine
    {
        public PhaseLevelOperation ErrorRoutine(int topState, ProductionSymbol symbol, Token currentToken, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack)
        {
            throw new NotImplementedException();
        }
    }

    public class ExperimentGrammarPanicErrorRoutine : IPanicErrorRoutine
    {
        public ProductionSymbol ParticularNonTerminal(Grammar grammar)
        {
            return new ProductionSymbol(grammar, ProductionSymbol.SymbolType.NonTerminal, "S");
        }
    }
}
