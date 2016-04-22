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
        string ErrorRoutine(int topState, ProductionSymbol symbol, Token previousToken, PrintableStack<int> parseStack, PrintableStack<ProductionSymbol> symbolStack);
    }

    public interface IReportProgress
    {
        void ReportProgress(string progress);
    }

    public interface IReportParseStep
    {
        void ReportStep(string action, string symbol, string state);
    }
}
