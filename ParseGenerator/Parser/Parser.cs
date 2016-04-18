using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Lex;
using Symbol;

namespace ParseGenerator
{
    public class SymbolStack
    {
        private Stack<ProductionSymbol> symbolStack = new Stack<ProductionSymbol>();

        public void Push(ProductionSymbol sym)
        {
            symbolStack.Push(sym);
        }

        public ProductionSymbol Pop()
        {
            return symbolStack.Pop();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e in symbolStack.Reverse())
                sb.Append(e.ToString() + " ");
            return sb.ToString(0, sb.Length - 1);
        }
    }

    public class Parser<T> where T: LR0Item
    {
        private ParseTable<T> parseTable;
        private Grammar grammar;

        public Grammar Grammar
        {
            get { return grammar; }
        }

        public Parser(Grammar grammar, ParseTable<T> pt)
        {
            this.grammar = grammar;
            this.parseTable = pt;
        }
        
        public List<Tuple<string, string>> Parse(Stream input)
        {
            var lexer = new Lexer(grammar.SymbolTable, input);
            var parseStack = new Stack<int>();
            var symbolStack = new SymbolStack();
            var ops = new List<Tuple<string, string>>();
            var accept = false;
            
            // Let a be the first symbol of w$
            Token token = lexer.ScanNextToken();

            // Push initial state
            parseStack.Push(parseTable.InitialState);
            symbolStack.Push(grammar.EndMarker);

            // Repeat forever
            while (!accept)
            {
                // let s be the state on top of the stack
                var top = parseStack.Peek();
                var action = parseTable.Action[top][new ProductionSymbol(grammar, token)].PreferEntry;

                switch (action.Type)
                {
                    // ACTION[s, a] = shift t
                    case ActionTableEntry.ActionType.Shift:
                        // push t onto stack
                        parseStack.Push(action.ShiftState);
                        symbolStack.Push(new ProductionSymbol(grammar, token));

                        // let a be the next input symbol
                        token = lexer.ScanNextToken();

                        ops.Add(new Tuple<string, string>(action.ToString(), symbolStack.ToString()));
                        break;

                    // ACTION[s, a] = reduce A -> β
                    case ActionTableEntry.ActionType.Reduce:
                        var betaLength = action.ReduceProduction.Right.Count;

                        // Check if β = ε, if so, |β| = 0
                        if (action.ReduceProduction.Right.Count == 1 && action.ReduceProduction.Right[0].Equals(grammar.Epsilon))
                            betaLength = 0;

                        // pop |β| symbols off the stack
                        for (int i = 0; i < betaLength; i++)
                        {
                            parseStack.Pop();
                            symbolStack.Pop();
                        }

                        // let state t now be on top the stack
                        top = parseStack.Peek();

                        // push GOTO[t, A] onto stack
                        parseStack.Push(parseTable.Goto[top][action.ReduceProduction.Left]);
                        symbolStack.Push(action.ReduceProduction.Left);

                        // output the production
                        ops.Add(new Tuple<string, string>(action.ToString(), symbolStack.ToString()));
                        break;

                    // ACTION[s, a] = accept
                    case ActionTableEntry.ActionType.Accept:
                        accept = true;
                        ops.Add(new Tuple<string, string>(action.ToString(), symbolStack.ToString()));
                        break;

                    // ACTION[s, a] = error
                    case ActionTableEntry.ActionType.Error:
                        throw new ApplicationException("Parser Error.");
                }
            }

            return ops;
        }
    }
}
