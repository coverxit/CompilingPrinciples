using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseTableGenerator
{
    public class FirstFollowSet
    {
        private Grammar grammar;

        private Dictionary<Production.Symbol, HashSet<Production.Symbol>> sets;

        public FirstFollowSet(Grammar grammar)
        {
            this.grammar = grammar;
            sets = new Dictionary<Production.Symbol, HashSet<Production.Symbol>>();
        }

        public HashSet<Production.Symbol> Get(Production.SymbolType type, int id)
        {
            return Get(new Production.Symbol(grammar, type, id));
        }

        public HashSet<Production.Symbol> Get(Production.Symbol sym)
        {
            return new HashSet<Production.Symbol>(sets[sym]);
        }

        public bool Contains(Production.Symbol sym)
        {
            return sets.ContainsKey(sym);
        }

        public void Put(Production.Symbol sym, Production.Symbol val)
        {
            if (!sets.ContainsKey(sym))
                sets.Add(sym, new HashSet<Production.Symbol>());
            sets[sym].Add(val);
        }

        public void Put(Production.Symbol sym, HashSet<Production.Symbol> val)
        {
            if (!sets.ContainsKey(sym))
                sets.Add(sym, new HashSet<Production.Symbol>());
            sets[sym].UnionWith(val);
        }
    }
}
