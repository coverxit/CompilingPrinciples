using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.SymbolEnvironment;

namespace CompilingPrinciples.SyntaxAnalyzer
{
    public abstract class Parser
    {
        // (S)hindo's (P)arser (C)ontext (D)ata - 0x44435053
        public const int ContextMagicNumber = 0x44435053;

        protected Grammar grammar;
        protected List<Tuple<int, int>> invalidRegions = new List<Tuple<int, int>>();

        public Grammar Grammar
        {
            get { return grammar; }
        }

        public List<Tuple<int, int>> InvalidRegions
        {
            get { return new List<Tuple<int, int>>(invalidRegions); }
        }

        public abstract List<Tuple<string, string>> Parse(Stream input);
        public abstract void SaveContext(Stream stream);

        public static Parser CreateFromContext(Stream stream, SymbolTable symbolTable, IParserErrorRoutine errRoutine = null, IReportParseStep reporter = null)
        {
            // Deserialization
            var formatter = new BinaryFormatter();

            if ((int)formatter.Deserialize(stream) != ContextMagicNumber)
                throw new ApplicationException("Invalid Magic Number!");

            formatter.Context = new StreamingContext(StreamingContextStates.All, symbolTable);
            var grammar = formatter.Deserialize(stream) as Grammar;

            formatter.Context = new StreamingContext(StreamingContextStates.All, null);
            var itemType = formatter.Deserialize(stream) as Type;

            formatter.Context = new StreamingContext(StreamingContextStates.All, grammar);
            if (itemType.FullName == typeof(LR0Item).FullName)
            {
                var parseTable = formatter.Deserialize(stream) as SLRParseTable;
                return new Parser<LR0Item>(symbolTable, grammar, parseTable, errRoutine, reporter);
            }
            else // LR1Item
            {
                var parseTable = formatter.Deserialize(stream) as LR1ParseTable;
                return new Parser<LR1Item>(symbolTable, grammar, parseTable, errRoutine, reporter);
            }
        }
    }

    public class Parser<T> : Parser where T: LR0Item
    {
        private ParseTable<T> parseTable;
        private SymbolTable symbolTable;
        private IParserErrorRoutine errRoutine;
        private IReportParseStep reporter;
        
        public Parser(SymbolTable st, Grammar grammar, ParseTable<T> pt, IParserErrorRoutine routine = null, IReportParseStep reporter = null)
        {
            this.symbolTable = st;
            this.grammar = grammar;
            this.parseTable = pt;
            this.errRoutine = routine;
            this.reporter = reporter;
        }
        
        public override List<Tuple<string, string>> Parse(Stream input)
        {
            // Clear symbol table
            // Note that clear symbol table
            // must before create Lexer
            // because lexer will reverse keywords
            symbolTable.Clear();

            var lexer = new Lexer(symbolTable, input);
            var parseStack = new PrintableStack<int>();
            var symbolStack = new PrintableStack<ProductionSymbol>();
            var ops = new List<Tuple<string, string>>();
            var accept = false;
            
            // Let a be the first symbol of w$
            Token token = lexer.ScanNextToken(), prevToken = null;
            accept = token is EndMarker; // EndMarker?

            // Push initial state
            parseStack.Push(parseTable.InitialState);
            symbolStack.Push(grammar.EndMarker);

            // Repeat forever
            while (!accept)
            {
                // process invalid token first
                if (token is InvalidToken)
                {
                    int regionStart = token.Position;

                    // Panic-mode Error Recovery
                    while (!(token is Separator))
                        token = lexer.ScanNextToken();

                    invalidRegions.Add(new Tuple<int, int>(regionStart, token.Position - regionStart));
                }
                
                // let s be the state on top of the stack
                var top = parseStack.Peek();
                var symbol = new ProductionSymbol(grammar, token);
                var action = parseTable.Action[top][symbol].PreferredEntry;

                switch (action.Type)
                {
                    // ACTION[s, a] = shift t
                    case ActionTableEntry.ActionType.Shift:
                        // push t onto stack
                        parseStack.Push(action.ShiftState);
                        symbolStack.Push(new ProductionSymbol(grammar, token));

                        // let a be the next input symbol
                        prevToken = token;
                        token = lexer.ScanNextToken();

                        ops.Add(new Tuple<string, string>(action.ToString(), symbolStack.ToString()));
                        if (reporter != null) reporter.ReportStep(action.ToString(), symbolStack.ToString(), parseStack.ToString());
                        break;

                    // ACTION[s, a] = reduce A -> β
                    case ActionTableEntry.ActionType.Reduce:
                        // Check if β = ε, if so, |β| = 0
                        var betaLength = action.ReduceProduction.IsRightEpsilon() ? 0 : action.ReduceProduction.Right.Count;
                        
                        // pop |β| symbols off the stack
                        for (int i = 0; i < betaLength; i++)
                            parseStack.Pop();

                        // For symbol stack, we pop it until its count equals to parseStack's
                        // Because the error routine may push new state into parseStack,
                        // without new symbol pushed into symbolStack
                        while (symbolStack.Count > parseStack.Count)
                            symbolStack.Pop();

                        // let state t now be on top the stack
                        top = parseStack.Peek();

                        // push GOTO[t, A] onto stack
                        parseStack.Push(parseTable.Goto[top][action.ReduceProduction.Left]);
                        symbolStack.Push(action.ReduceProduction.Left);

                        // output the production
                        ops.Add(new Tuple<string, string>(action.ToString(), symbolStack.ToString()));
                        if (reporter != null) reporter.ReportStep(action.ToString(), symbolStack.ToString(), parseStack.ToString());
                        break;

                    // ACTION[s, a] = accept
                    case ActionTableEntry.ActionType.Accept:
                        accept = true;

                        ops.Add(new Tuple<string, string>(action.ToString(), symbolStack.ToString()));
                        if (reporter != null) reporter.ReportStep(action.ToString(), symbolStack.ToString(), parseStack.ToString());
                        break;

                    // ACTION[s, a] = error
                    case ActionTableEntry.ActionType.Error:
                        if (errRoutine == null)
                            throw new ApplicationException("Syntax Error near Line " + token.Line);

                        var ret = errRoutine.ErrorRoutine(top, symbol, prevToken, parseStack, symbolStack);
                        var sym = symbolStack.ToString();
                        var state = parseStack.ToString();

                        ops.Add(new Tuple<string, string>(ret, sym));
                        if (reporter != null) reporter.ReportStep(ret, sym, state);
                        break;
                }
            }

            return ops;
        }

        public override void SaveContext(Stream stream)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, ContextMagicNumber);
            formatter.Serialize(stream, grammar);
            formatter.Serialize(stream, parseTable.ItemType);
            formatter.Serialize(stream, parseTable);
        }
    }
}
