﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;

namespace CompilingPrinciples.ParserCore
{
    public class ParseTreeNodeEntry
    {
        private ProductionSymbol symbol;
        private object data;

        public ProductionSymbol Symbol
        {
            get { return symbol; }
        }

        // NonTerminal - Reduce Production
        // Terminal - Token
        public object Data
        {
            get { return data; }
        }

        public ParseTreeNodeEntry(ProductionSymbol sym, object data)
        {
            this.symbol = sym;
            this.data = data;
        }

        public override string ToString()
        {
            return symbol.ToString();
        }
    }

    public abstract class Parser
    {
        // (S)hindo's (P)arser (C)ontext (D)ata - 0x44435053
        public const int ContextMagicNumber = 0x44435053;

        protected Grammar grammar;
        public Grammar Grammar
        {
            get { return grammar; }
        }

        protected List<int> errLines = new List<int>();
        public List<int> ErrorLines
        {
            get { return errLines; }
        }

        public abstract TreeNode<ParseTreeNodeEntry> Parse(Stream input);
        public abstract void SaveContext(Stream stream);

        public static Parser CreateFromContext(Stream stream, SymbolTable symbolTable, IPanicErrorRoutine errRoutine = null, IReportParseStep reporter = null)
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
        //private IPhaseLevelParserErrorRoutine phaseLevelRoutine;
        private IPanicErrorRoutine panicRoutine;
        private IReportParseStep stepReporter;
        
        /*
        // Leave Phase Level away!
        public Parser(SymbolTable st, Grammar grammar, ParseTable<T> pt, IPhaseLevelParserErrorRoutine routine, IReportParseStep reporter = null)
        {
            this.symbolTable = st;
            this.grammar = grammar;
            this.parseTable = pt;
            this.phaseLevelRoutine = routine;
            this.reporter = reporter;
        }
        */

        public Parser(SymbolTable st, Grammar grammar, ParseTable<T> pt, IPanicErrorRoutine routine, IReportParseStep reporter = null)
        {
            this.symbolTable = st;
            this.grammar = grammar;
            this.parseTable = pt;
            this.panicRoutine = routine;
            this.stepReporter = reporter;
        }

        public override TreeNode<ParseTreeNodeEntry> Parse(Stream input)
        {
            // Clear symbol table
            // Note that clear symbol table
            // must before create Lexer
            // because lexer will reverse keywords
            symbolTable.Clear();
            errLines.Clear();

            var lexer = new Lexer(symbolTable, input);
            var parseStack = new PrintableStack<int>();
            var symbolStack = new PrintableStack<ProductionSymbol>();
            var treeNodeStack = new Stack<TreeNode<ParseTreeNodeEntry>>();
            var accept = false;
            TreeNode<ParseTreeNodeEntry> rootNode = null;
            
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
                        symbolStack.Push(symbol);
                        treeNodeStack.Push(new TreeNode<ParseTreeNodeEntry>(new ParseTreeNodeEntry(symbol, token)));

                        // let a be the next input symbol
                        prevToken = token;
                        token = lexer.ScanNextToken();
                        
                        if (stepReporter != null) stepReporter.ReportStep(false, action.ToString(), parseStack.ToString(), symbolStack.ToString());
                        break;

                    // ACTION[s, a] = reduce A -> β
                    case ActionTableEntry.ActionType.Reduce:
                        // Check if β = ε, if so, |β| = 0
                        var betaLength = action.ReduceProduction.IsRightEpsilon() ? 0 : action.ReduceProduction.Right.Count;

                        // pop |β| symbols off the stack
                        for (int i = 0; i < betaLength; i++)
                            parseStack.Pop();

                        // Build tree
                        var node = new TreeNode<ParseTreeNodeEntry>(new ParseTreeNodeEntry(action.ReduceProduction.Left, action.ReduceProduction.ToString()));
                        
                        // For symbol stack, we pop it until its count equals to parseStack's
                        // Because the error routine may push new state into parseStack,
                        // without new symbol pushed into symbolStack
                        // and the same as treeNodeStack
                        while (symbolStack.Count > parseStack.Count)
                        {
                            symbolStack.Pop();
                            node.InsertFirst(treeNodeStack.Pop());
                        }

                        // When betaLength equals 0, ε is insert to tree
                        if (betaLength == 0)
                            node.AddChild(new ParseTreeNodeEntry(grammar.Epsilon, null));
                            
                        // let state t now be on top the stack
                        top = parseStack.Peek();

                        // push GOTO[t, A] onto stack
                        parseStack.Push(parseTable.Goto[top][action.ReduceProduction.Left]);
                        symbolStack.Push(action.ReduceProduction.Left);
                        treeNodeStack.Push(node);

                        // output the production
                        if (stepReporter != null) stepReporter.ReportStep(false, action.ToString(), parseStack.ToString(), symbolStack.ToString());
                        break;

                    // ACTION[s, a] = accept
                    case ActionTableEntry.ActionType.Accept:
                        accept = true;
                        rootNode = treeNodeStack.Pop();

                        if (stepReporter != null) stepReporter.ReportStep(false, action.ToString(), parseStack.ToString(), symbolStack.ToString());
                        break;

                    // ACTION[s, a] = error
                    case ActionTableEntry.ActionType.Error:
                        if (panicRoutine == null)
                            throw new ApplicationException("Syntax Error at Line " + token.Line);

                        // The phase level error recovery is somewhat fxxking...
                        // Instead, we use panic error recovery!
                        var isReturn = true;
                        var line = token.Line;
                        var A = panicRoutine.ParticularNonTerminal(grammar);

                        // Scan down the parse tack, until a GOTO(s, A) exist.
                        // A is a particular nonterminal carefully choosed.
                        var scanDownState = -1;

                        // Scan down the stack, but reserve the bottom of stack
                        foreach (var e in parseStack.Reverse().Where(e => e > 0))
                        {
                            if (parseTable.Goto[e][A] > 0)
                            {
                                scanDownState = e;
                                break;
                            }

                            // pop states after s
                            parseStack.Pop();
                        }

                        // Nothing in the stack can GOTO[S], so just leave it
                        if (scanDownState == -1)
                            goto addOp;

                        // check for a token can legitimately follow S.
                        // current token is considered as well.
                        while (!(token is EndMarker))
                        {
                            if (!(token is InvalidToken) &&
                                parseTable.Collection.Follow.Get(A).Contains(new ProductionSymbol(grammar, token)))
                                break;

                            prevToken = token;
                            token = lexer.ScanNextToken();
                        }

                        // EOF?
                        if (token is EndMarker)
                            goto addOp;

                        // Push GOTO(s, A), then resumes parsing
                        // Check if GOTO(s, A) equals to top, avoid infinite loop here
                        if (parseTable.Goto[scanDownState][A] != top)
                        {
                            parseStack.Push(parseTable.Goto[scanDownState][A]);
                            isReturn = false;
                        }

                    addOp:
                        if (stepReporter != null)
                            stepReporter.ReportStep(true, "syntax error near line " + line, parseStack.ToString(), symbolStack.ToString());
                        errLines.Add(line);

                        if (isReturn) return null;
                        else break;

                        // Below is our phase level try...
                        /*
                        // Add to invalid, before routine modifies token
                        invalidRegions.Add(new Tuple<int, int>(token.Position, token.Length));

                        var ret = phaseLevelRoutine.ErrorRoutine(top, symbol, token, prevToken, parseStack, symbolStack);
                        switch (ret.Type)
                        {
                            case PhaseLevelOperation.OpType.PushState:
                                parseStack.Push(ret.StateToPush);
                                break;

                            case PhaseLevelOperation.OpType.SkipToken:
                                prevToken = token;
                                token = lexer.ScanNextToken();
                                break;

                            case PhaseLevelOperation.OpType.ReduceBy:
                                Reduce(ref top, 
                                    parseTable.Action[ret.Reduce.State][new ProductionSymbol(grammar, ret.Reduce.Symbol)].PreferredEntry, 
                                    parseStack, symbolStack);
                                break;
                        }

                        var sym = symbolStack.ToString();
                        var state = parseStack.ToString();
                        
                        var diagnostic = ret.Diagnostic == string.Empty ? "" : "line " + token.Line + ": " + ret.Diagnostic;

                        ops.Add(new Tuple<string, string, string>(diagnostic, state, sym));
                        if (reporter != null)
                            reporter.ReportStep(true, diagnostic, sym, state);
                        */
                }
            }

            return rootNode;
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
