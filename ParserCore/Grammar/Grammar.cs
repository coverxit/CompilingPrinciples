using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;

namespace CompilingPrinciples.ParserCore
{
    [Serializable]
    public class Grammar
    {
        [NonSerialized]
        private ProductionSymbol epsilon, endMarker;
    
        private List<string> terminalTable, nonTerminalTable;
        private List<Production> productions;

        [NonSerialized]
        private SymbolTable symbolTable;

        [NonSerialized]
        private IReportProgress reporter;

        public ProductionSymbol Epsilon
        {
            get { return epsilon; }
        }

        public ProductionSymbol EndMarker
        {
            get { return endMarker; }
        }

        public Production FirstProduction
        {
            get { return new Production(productions[0]); }
        }

        public ProductionSymbol AugemntedS
        {
            get { return productions[0].Left; }
        }

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

        public List<ProductionSymbol> TerminalsWithoutEpsilonWithEndMarker
        {
            // Exclude ε
            get
            {
                return terminalTable.Select((sym, id) => new ProductionSymbol(this, ProductionSymbol.SymbolType.Terminal, id))
                                    .Where(e => !e.Equals(epsilon)).ToList();
            }
        }

        public List<ProductionSymbol> TerminalsWithoutEpsilon
        {
            // Exclude ε
            get
            {
                return terminalTable.Select((sym, id) => new ProductionSymbol(this, ProductionSymbol.SymbolType.Terminal, id))
                                    .Where(e => !e.Equals(epsilon) && !e.Equals(endMarker)).ToList();
            }
        }

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

        public Grammar(SymbolTable symbolTable, IReportProgress reporter = null)
        {
            this.symbolTable = symbolTable;
            this.reporter = reporter;

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
            List<Tuple<int, int, int, string>> lines = new List<Tuple<int, int, int, string>>();

            if (reporter != null) reporter.ReportProgress("Parsing grammar...");

            string line;
            int lineCount = 1;

            // Read all lines first, split them into 2 parts and stored in lines
            while (reader.Peek() != -1) // Not EOF
            {
                while (string.IsNullOrEmpty(line = reader.ReadLine().Trim())) lineCount++;

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

                lines.Add(new Tuple<int, int, int, string>(lineCount++, lines.Count, leftNonTerminalId, right));
            }

            foreach (var l in lines)
            {
                Production prod = new Production(this, l.Item2, l.Item1);
                prod.SetLeftNonTerminal(l.Item3);

                var rightExpr = l.Item4.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
            else if (token is LexerCore.Decimal)
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
        
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.symbolTable = context.Context as SymbolTable;

            epsilon = new ProductionSymbol(this, ProductionSymbol.SymbolType.Terminal, terminalTable.IndexOf("@")); // ε
            endMarker = new ProductionSymbol(this, ProductionSymbol.SymbolType.Terminal, terminalTable.IndexOf("$")); // $
        }
    }
}
