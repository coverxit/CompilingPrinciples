using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Lex;

namespace ParseGenerator
{
    public class Parser
    {
        private ParseTable parseTable;
        private Grammar grammar;

        public Parser(Grammar grammar, ParseTable pt)
        {
            this.grammar = grammar;
            this.parseTable = pt;
        }
        
        public List<string> Parse(Stream input)
        {
            var lexer = new Lexer(input);
            var parseStack = new Stack<int>();
            var ops = new List<string>();
            var accept = false;
            
            // Let a be the first symbol of w$
            Token token = lexer.ScanNextToken();

            // Push initial state
            parseStack.Push(parseTable.InitialState);

            // Repeat forever
            while (!accept)
            {
                // let s be the state on top of the stack
                var top = parseStack.Peek();
                var action = parseTable.Action(top, new ProductionSymbol(grammar, token));

                switch (action.Type)
                {
                    // ACTION[s, a] = shift t
                    case ActionTableEntry.ActionType.Shift:
                        // push t onto stack
                        parseStack.Push(action.ShiftState);

                        // let a be the next input symbol
                        token = lexer.ScanNextToken();

                        ops.Add(action.ToString());
                        break;

                    // ACTION[s, a] = reduce A -> β
                    case ActionTableEntry.ActionType.Reduce:
                        // pop |β| symbols off the stack
                        for (int i = 0; i < action.ReduceProduction.Right.Count; i++)
                            parseStack.Pop();

                        // let state t now be on top the stack
                        top = parseStack.Peek();

                        // push GOTO[t, A] onto stack
                        parseStack.Push(parseTable.Goto(top, action.ReduceProduction.Left));

                        // output the production
                        ops.Add(action.ToString());
                        break;

                    // ACTION[s, a] = accept
                    case ActionTableEntry.ActionType.Accept:
                        accept = true;
                        ops.Add(action.ToString());
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
