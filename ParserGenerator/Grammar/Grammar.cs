﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using LexicalAnalyzer;
using SymbolEnvironment;

namespace ParserGenerator
{
    public class Grammar
    {
        private ProductionSymbol epsilon, endMarker;
        public ProductionSymbol Epsilon
        {
            get { return new ProductionSymbol(epsilon); }
        }

        public ProductionSymbol EndMarker
        {
            get { return new ProductionSymbol(endMarker); }
        }

        public Production FirstProduction
        {
            get { return new Production(productions[0]); }
        }

        public ProductionSymbol AugemntedS
        {
            get { return new ProductionSymbol(productions[0].Left); }
        }

        private List<string> terminalTable, nonTerminalTable;
        private List<Production> productions;
        private SymbolTable symbolTable;

        public List<string> TerminalTable
        {
            get { return terminalTable; }
        }

        public List<string> NonTerminalTable
        {
            get { return nonTerminalTable; }
        }

        public List<ProductionSymbol> TerminalsWithEndMarker
        {
            get
            {
                return terminalTable.Select((sym, id) => new ProductionSymbol(this, ProductionSymbol.SymbolType.Terminal, id)).ToList();
            }
        }

        /*
        public List<ProductionSymbol> Terminals
        {
            // Exclude $ and ε
            get
            {
                return terminalTable.Select((sym, id) => new ProductionSymbol(this, ProductionSymbol.SymbolType.Terminal, id))
                                    .Where(e => !e.Equals(epsilon) && !e.Equals(endMarker)).ToList();
            }
        }
        */

        public List<ProductionSymbol> Terminals
        {
            // Exclude $
            get
            {
                return terminalTable.Select((sym, id) => new ProductionSymbol(this, ProductionSymbol.SymbolType.Terminal, id))
                                    .Where(e => !e.Equals(endMarker)).ToList();
            }
        }

        public List<ProductionSymbol> NonTerminals
        {
            get { return nonTerminalTable.Select((sym, id) => new ProductionSymbol(this, ProductionSymbol.SymbolType.NonTerminal, id)).ToList(); }
        }

        public List<ProductionSymbol> NonTerminalsWithoutAugmentedS
        {
            get { return NonTerminals.Where(e => !e.Equals(AugemntedS)).ToList(); }
        }

        public List<ProductionSymbol> GrammarSymbols
        {
            get
            {
                var symbols = NonTerminals;
                symbols.AddRange(Terminals);
                return symbols;
            }
        }

        public List<Production> Productions
        {
            get { return new List<Production>(productions); }
        }

        public Grammar(SymbolTable symbolTable)
        {
            this.symbolTable = symbolTable;

            terminalTable = new List<string>();
            nonTerminalTable = new List<string>();
            productions = new List<Production>();

            epsilon = reserveSymbol("@", ProductionSymbol.SymbolType.Terminal); // ε
            endMarker = reserveSymbol("$", ProductionSymbol.SymbolType.Terminal); // $
        }

        private ProductionSymbol reserveSymbol(string sym, ProductionSymbol.SymbolType type)
        {
            int id = -1;

            switch (type)
            {
                case ProductionSymbol.SymbolType.NonTerminal:
                    lock (nonTerminalTable)
                    {
                        nonTerminalTable.Add(sym);
                        id = nonTerminalTable.Count - 1;
                    }
                    break;

                case ProductionSymbol.SymbolType.Terminal:
                    lock (terminalTable)
                    {
                        terminalTable.Add(sym);
                        id = terminalTable.Count - 1;
                    }
                    break;
            }

            return new ProductionSymbol(this, type, id);
        }

        public void Parse(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            List<Tuple<int, string>> lines = new List<Tuple<int, string>>();

            string line;

            // Read all lines first, split them into 2 parts and stored in lines
            while (reader.Peek() != -1) // Not EOF
            {
                while (string.IsNullOrEmpty(line = reader.ReadLine().Trim())) ;

                string[] split = line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 2)
                    throw new ApplicationException("Production Mismatch.");

                string left = split[0].Trim(), right = split[1].Trim();
                // Save all nonterminals in the left into nonterminal table
                int leftNonTerminalId = nonTerminalTable.IndexOf(left);
                if (leftNonTerminalId == -1)
                {
                    lock (nonTerminalTable)
                    {
                        nonTerminalTable.Add(left);
                        leftNonTerminalId = nonTerminalTable.Count - 1;
                    }
                }

                lines.Add(new Tuple<int, string>(leftNonTerminalId, right));
            }

            foreach (var pair in lines)
            {
                Production prod = new Production(this);
                prod.SetLeftNonTerminal(pair.Item1);

                var rightExpr = pair.Item2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (rightExpr.Length == 0) continue;

                dispatchRightExpression(rightExpr, ref prod);
                productions.Add(prod);
            }

            reader.Close();
        }

        private void dispatchRightExpression(string[] rightSymbols, ref Production prod)
        {
            foreach (var sym in rightSymbols)
            {
                // First, find nonterminal table
                int id = nonTerminalTable.IndexOf(sym);
                if (id != -1)
                    prod.AppendRightSymbol(ProductionSymbol.SymbolType.NonTerminal, id);
                else
                {
                    // Then, find terminal table
                    id = terminalTable.IndexOf(sym);
                    if (id == -1)
                    {
                        lock (terminalTable)
                        {
                            terminalTable.Add(sym);
                            id = terminalTable.Count - 1;
                        }
                    }

                    prod.AppendRightSymbol(ProductionSymbol.SymbolType.Terminal, id);
                }
            }
        }
        
        public int GetIdInTerminalTable(Token token)
        {
            string symbolToFind = string.Empty;

            if (token is EndMarker)
                symbolToFind = "$";
            else if (token is LexicalAnalyzer.Decimal)
                symbolToFind = "decimal";
            else if (token is Word)
            {
                var wordToken = token as Word;
                symbolToFind = wordToken is Identifier ? "id" : symbolTable.GetSymbol(wordToken.IdInSymbolTable);
            }
            else if (token is Operator)
            {
                var opToken = token as Operator;
                symbolToFind = opToken.GetValue().ToString();
            }
            else if (token is Separator)
            {
                var spToken = token as Separator;
                symbolToFind = spToken.GetValue().ToString();
            }
            else // InvalidToken
                throw new ApplicationException("Invalid Token.");

            return terminalTable.IndexOf(symbolToFind);
        }
    }
}