using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompilingPrinciples.LexerModule;

namespace CompilingPrinciples.ParserModule
{
    public class SolveOperation
    {
        public enum OpType
        {
            PushState = 0,
            SkipToken = 1,
        }

        private OpType opType;
        public OpType Type
        {
            get { return opType; }
        }

        private int state;
        public int StateToPush
        {
            get { return state; }
        }

        private string diagnostic;
        public string Diagnostic
        {
            get { return diagnostic; }
        }

        private SolveOperation(OpType type, string diagnostic)
        {
            this.opType = type;
            this.diagnostic = diagnostic;
        }

        public static SolveOperation PushState(int state, string diagnostic = "")
        {
            var op = new SolveOperation(OpType.PushState, diagnostic);
            op.state = state;
            return op;
        }

        public static SolveOperation SkipToken(string diagnostic = "")
        {
            var op = new SolveOperation(OpType.SkipToken, diagnostic);
            return op;
        }
    }

    public interface IParserErrorRoutine
    {
        SolveOperation ErrorRoutine(int topState, ProductionSymbol symbol, Token currentToken, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack);
    }

    public interface IReportProgress
    {
        void ReportProgress(string progress);
    }

    public interface IReportParseStep
    {
        void ReportStep(bool error, string action, string symbol, string state);
    }
}
