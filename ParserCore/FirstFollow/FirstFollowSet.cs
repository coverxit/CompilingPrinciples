﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CompilingPrinciples.ParserCore
{
    [Serializable]
    public class FirstFollowSet
    {
        [NonSerialized]
        private Grammar grammar;

        private Dictionary<ProductionSymbol, HashSet<ProductionSymbol>> sets;

        public FirstFollowSet(Grammar grammar)
        {
            this.grammar = grammar;
            sets = new Dictionary<ProductionSymbol, HashSet<ProductionSymbol>>();
        }

        public HashSet<ProductionSymbol> Get(ProductionSymbol.SymbolType type, int id)
        {
            return Get(new ProductionSymbol(grammar, type, id));
        }

        public HashSet<ProductionSymbol> Get(ProductionSymbol sym)
        {
            return new HashSet<ProductionSymbol>(sets[sym]);
        }

        public bool Contains(ProductionSymbol sym)
        {
            return sets.ContainsKey(sym);
        }

        public void Put(ProductionSymbol sym, ProductionSymbol val)
        {
            if (!sets.ContainsKey(sym))
                sets.Add(sym, new HashSet<ProductionSymbol>());
            sets[sym].Add(val);
        }

        public void Put(ProductionSymbol sym, HashSet<ProductionSymbol> val)
        {
            if (!sets.ContainsKey(sym))
                sets.Add(sym, new HashSet<ProductionSymbol>());
            sets[sym].UnionWith(val);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.grammar = context.Context as Grammar;
        }
    }
}
