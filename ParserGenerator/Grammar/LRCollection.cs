using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserGenerator
{
    public abstract class LRCollection<T> where T: LR0Item
    {
        protected Grammar grammar;
        public Grammar Grammar
        {
            get { return grammar; }
        }

        protected FirstFollowSet firstSet, followSet;
        public FirstFollowSet First
        {
            get { return firstSet; }
        }

        public FirstFollowSet Follow
        {
            get { return followSet; }
        }


        public LRCollection(Grammar grammar)
        {
            this.grammar = grammar;

            var gen = new FirstFollowGenerator(grammar);
            firstSet = gen.FirstSet;
            followSet = gen.FollowSet;
        }

        public abstract HashSet<T> Closure(HashSet<T> I);

        public abstract HashSet<T> Goto(HashSet<T> I, ProductionSymbol X);

        public abstract HashSet<HashSet<T>> Items();
    }
}
