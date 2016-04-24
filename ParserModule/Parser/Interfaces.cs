using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompilingPrinciples.LexerModule;

namespace CompilingPrinciples.ParserModule
{
    public class PhaseLevelOperation
    {
        public enum OpType
        {
            PushState = 0,
            SkipToken = 1,
            ReduceBy = 2,
        }

        public struct ReduceContext
        {
            public int State;
            public string Symbol;

            public ReduceContext(int state, string symbol)
            {
                this.State = state;
                this.Symbol = symbol;
            }
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
            set { diagnostic = value; }
        }

        private ReduceContext reduce;
        public ReduceContext Reduce
        {
            get { return reduce; }
        }

        private PhaseLevelOperation(OpType type, string diagnostic)
        {
            this.opType = type;
            this.diagnostic = diagnostic;
        }

        public PhaseLevelOperation(PhaseLevelOperation rhs)
        {
            this.opType = rhs.Type;
            this.diagnostic = rhs.diagnostic;
            this.state = rhs.state;
            this.reduce = rhs.reduce;
        }

        public static PhaseLevelOperation PushState(int state, string diagnostic = "")
        {
            var op = new PhaseLevelOperation(OpType.PushState, diagnostic);
            op.state = state;
            return op;
        }

        public static PhaseLevelOperation SkipToken(string diagnostic = "")
        {
            var op = new PhaseLevelOperation(OpType.SkipToken, diagnostic);
            return op;
        }

        public static PhaseLevelOperation ReduceBy(int state, string symbol, string diagnostic = "")
        {
            var op = new PhaseLevelOperation(OpType.ReduceBy, diagnostic);
            op.reduce = new ReduceContext(state, symbol);
            return op;
        }
    }

    public interface IPhaseLevelParserErrorRoutine
    {
        PhaseLevelOperation ErrorRoutine(int topState, ProductionSymbol symbol, Token currentToken, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack);
    }

    public interface IPanicErrorRoutine
    {
        ProductionSymbol ParticularNonTerminal(Grammar grammar);
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
