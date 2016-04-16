﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParseTableGenerator
{
    public class Grammar
    {
        private Production.Symbol epsilon, endMarker;
        public Production.Symbol Epsilon
        {
            get { return new Production.Symbol(epsilon); }
        }

        public Production.Symbol EndMarker
        {
            get { return new Production.Symbol(endMarker); }
        }

        private List<string> terminalTable, nonTerminalTable;
        private List<Production> productions;

        public List<string> TerminalTable
        {
            get { return terminalTable; }
        }

        public List<string> NonTerminalTable
        {
            get { return nonTerminalTable; }
        }

        public List<Production.Symbol> Terminals
        {
            get { return terminalTable.Select((sym, id) => new Production.Symbol(this, Production.SymbolType.Terminal, id)).ToList(); }
        } 

        public List<Production.Symbol> NonTerminals
        {
            get { return nonTerminalTable.Select((sym, id) => new Production.Symbol(this, Production.SymbolType.NonTerminal, id)).ToList(); }
        }

        public List<Production> Productions
        {
            get { return new List<Production>(productions); }
        }

        public Grammar()
        {
            terminalTable = new List<string>();
            nonTerminalTable = new List<string>();
            productions = new List<Production>();

            epsilon = reserveSymbol("@", Production.SymbolType.NonTerminal); // ε
            endMarker = reserveSymbol("$", Production.SymbolType.NonTerminal); // $
        }

        private Production.Symbol reserveSymbol(string sym, Production.SymbolType type)
        {
            int id = -1;

            switch (type)
            {
                case Production.SymbolType.NonTerminal:
                    lock (nonTerminalTable)
                    {
                        nonTerminalTable.Add(sym);
                        id = nonTerminalTable.Count - 1;
                    }
                    break;

                case Production.SymbolType.Terminal:
                    lock (terminalTable)
                    {
                        terminalTable.Add(sym);
                        id = terminalTable.Count - 1;
                    }
                    break;
            }

            return new Production.Symbol(this, type, id);
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
                // Save all terminals in the left into terminal table
                int leftTerminalId = terminalTable.IndexOf(left);
                if (leftTerminalId == -1)
                {
                    lock (terminalTable)
                    {
                        terminalTable.Add(left);
                        leftTerminalId = terminalTable.Count - 1;
                    }
                }

                lines.Add(new Tuple<int, string>(leftTerminalId, right));
            }

            foreach (var pair in lines)
            {
                Production prod = new Production(this);
                prod.SetLeftTerminal(pair.Item1);

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
                // First, find terminal table
                int id = terminalTable.IndexOf(sym);
                if (id != -1)
                    prod.AppendRightSymbol(Production.SymbolType.Terminal, id);
                else
                {
                    // Then, find nonterminal table
                    id = nonTerminalTable.IndexOf(sym);
                    if (id == -1)
                    {
                        lock (nonTerminalTable)
                        {
                            nonTerminalTable.Add(sym);
                            id = nonTerminalTable.Count - 1;
                        }
                    }

                    prod.AppendRightSymbol(Production.SymbolType.NonTerminal, id);
                }
            }
        }

    }
}
