using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CompilingPrinciples.ParserModule
{
    [Serializable]
    public abstract class LRCollection<T> where T: LR0Item
    {
        [NonSerialized]
        protected Grammar grammar;

        protected FirstFollowSet firstSet, followSet;

        [NonSerialized]
        protected IReportProgress reporter;

        public Grammar Grammar
        {
            get { return grammar; }
        }

        public FirstFollowSet First
        {
            get { return firstSet; }
        }

        public FirstFollowSet Follow
        {
            get { return followSet; }
        }

        public LRCollection(Grammar grammar, IReportProgress reporter = null)
        {
            this.grammar = grammar;
            this.reporter = reporter;

            var gen = new FirstFollowGenerator(grammar, reporter);
            firstSet = gen.First;
            followSet = gen.Follow;
        }

        public abstract HashSet<T> Closure(HashSet<T> I);

        public abstract HashSet<T> Goto(HashSet<T> I, ProductionSymbol X);

        public abstract HashSet<HashSet<T>> Items();

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.grammar = context.Context as Grammar;
        }
    }
}
