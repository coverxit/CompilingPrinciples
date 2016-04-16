using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseTableGenerator
{
    public class FirstFollowGenerator
    {
        protected Grammar grammar;
        protected FirstFollowSet firstSet, followSet;

        public FirstFollowSet FirstSet
        {
            get
            {
                if (firstSet == null)
                    computeFirstSet();

                return firstSet;
            }
        }

        public FirstFollowSet FollowSet
        {
            get
            {
                if (followSet == null)
                    computeFollowSet();

                return followSet;
            }
        }

        public FirstFollowGenerator(Grammar grammar)
        {
            this.grammar = grammar;
        }

        public void Generate()
        {
            // Compute First set first
            computeFirstSet();

            // Then Follow set,
            computeFollowSet();
        }

        private void computeFirstSet()
        {
            firstSet = new FirstFollowSet(grammar);

            foreach (var sym in grammar.NonTerminals)
                if (!firstSet.Contains(sym))
                    computeFirstSet(sym);
        }

        private void computeFirstSet(ProductionSymbol curSymbol)
        {
            // X is a terminal, First(X) = { X }
            if (curSymbol.Type == ProductionSymbol.SymbolType.Terminal)
                firstSet.Put(curSymbol, curSymbol);
            else
            {
                // Consider all X -> β productions
                var prods = grammar.Productions.Where(prod => prod.Left.Equals(curSymbol));

                foreach (var prod in prods)
                {
                    // If X -> Epsilon exists, Epsilon is in First(X)
                    if (prod.Right.Count == 1 && prod.Right[0].Equals(grammar.Epsilon))
                        firstSet.Put(curSymbol, grammar.Epsilon);
                    else
                    {
                        // If X -> Y1Y2...Yk
                        int i = 0;

                        while (i < prod.Right.Count)
                        {
                            var sym = prod.Right[i++];

                            // Avoid infinite recursion
                            if (sym.Equals(curSymbol))
                            {
                                var epsilonProds = prods.Where(e => e.Right.Count == 1 && e.Right[0].Equals(grammar.Epsilon));
                                if (epsilonProds.Count() == 0) // No CurSymbol -> Epsilon, just break
                                    break;
                            }

                            // First(Yk) hasn't been computed
                            if (!firstSet.Contains(sym))
                                computeFirstSet(sym);

                            firstSet.Put(curSymbol, firstSet.Get(sym));
                            // If ε is not in First(Yk), we're done here.
                            if (!firstSet.Get(sym).Contains(grammar.Epsilon))
                                break;

                            // For all i, ε is in First(Yi), so ε is in First(X) also.
                            if (i == prod.Right.Count)
                                firstSet.Put(curSymbol, grammar.Epsilon);
                        }
                    }
                }
            }
        }

        private void computeFollowSet()
        {
            followSet = new FirstFollowSet(grammar);

            // First set has to be computed first.
            if (firstSet == null) computeFirstSet();

            // Place $ in Follow(S), S is the start symbol
            followSet.Put(grammar.Productions[0].Left, grammar.EndMarker);

            // Stores the Follow that hasn't been computed yet,
            // Such as Follow(F) = Follow(S), when Follow(S) hasn't been 
            // computed yet.
            var pendingCompute = new Dictionary<ProductionSymbol, HashSet<ProductionSymbol>>();
            
            /*
            foreach (var prod in grammar.Productions)
            {
                // This stores First(X1X2..Xn), First(X2..Xn), ..., First(Xn)
                // for computing First(β)
                var prefixFirstSet = new List<HashSet<ProductionSymbol>>();

                // Compute First(Xn) first, then First(Xn-1Xn), ...
                // For First(Xn-iXn-i+1..Xn), we compute First(Xn-i) first,
                // then union with First(Xn-i+1..Xn) if possible.
                for (int i = prod.Right.Count - 1; i >= 0; i--)
                {
                    HashSet<ProductionSymbol> curPosFirst = new HashSet<ProductionSymbol>();

                    if (prod.Right[i].Type == ProductionSymbol.SymbolType.Terminal)
                        curPosFirst.Add(prod.Right[i]);
                    else
                        curPosFirst = firstSet.Get(prod.Right[i]);

                    // Obviously, First(X1) is in First(X1X2..Xn)
                    // And First(X2) in First(X1X2..Xn) if ε in First(X1)
                    // Add ε to First(X1X2...Xn), if for all i, ε in First(Xi)
                    var combinedFirst = new HashSet<ProductionSymbol>();
                    combinedFirst.UnionWith(curPosFirst);

                    // The second condition avoid crash when ε in First(Xn). 
                    if (curPosFirst.Contains(grammar.Epsilon) && prod.Right.Count - i - 2 > 0)
                        combinedFirst.UnionWith(prefixFirstSet[prod.Right.Count - i - 2]);

                    prefixFirstSet.Add(combinedFirst);
                }

                // Actually, the first item in firstSet is First(Xn),
                // which should be the last. So we just reverse it.
                prefixFirstSet.Reverse();

                foreach (var e in prod.Right.Select((value, index) => new { index, value }))
                    if (e.value.Type == ProductionSymbol.SymbolType.NonTerminal)
                    {
                        if (e.index < prod.Right.Count - 1)
                        {
                            // If A -> αBβ, First(β) - {ε} is in Follow(B), α can be ε
                            var firstBeta = new HashSet<ProductionSymbol>(prefixFirstSet[e.index + 1]);
                            firstBeta.ExceptWith(new ProductionSymbol[] { grammar.Epsilon });
                            followSet.Put(e.value, firstBeta);

                            // If A -> αBβ, and First(β) contains ε, then Follow(A) is in Follow(B)
                            if (prefixFirstSet[e.index + 1].Contains(grammar.Epsilon))
                                goto unionLeft;
                        }
                        else if (e.index == prod.Right.Count - 1)
                        {
                            // If A -> αB, Follow(A) is in Follow(B)
                            goto unionLeft;
                        }
                        continue;

                    unionLeft:
                        if (!pendingCompute.ContainsKey(e.value))
                             pendingCompute.Add(e.value, new HashSet<ProductionSymbol>());
                        pendingCompute[e.value].Add(prod.Left);
                    }
            }
            */
            
            
            foreach (var prod in grammar.Productions)
            {
                // We reversely compute Follow set, for no need to compute prefixFirstSet first.
                var rightReverse = prod.Right;
                rightReverse.Reverse();

                // Because we reversely compute, so First(β) can be compute on each loop below.
                var firstBeta = new HashSet<ProductionSymbol>();

                foreach (var e in rightReverse.Select((value, index) => new { index, value }))
                {
                    if (e.value.Type == ProductionSymbol.SymbolType.NonTerminal)
                    {
                        if (e.index == 0)
                        {
                            // If A -> αB, Follow(A) is in Follow(B)
                            goto unionLeft;
                        }
                        else
                        {
                            // Store if First(β) contains ε for second check
                            // Because we'll excpet the ε in First(β) below.
                            var containsEplison = firstBeta.Contains(grammar.Epsilon);
                            // If A -> αBβ, First(β) - {ε} is in Follow(B), α can be ε
                            firstBeta.ExceptWith(new ProductionSymbol[] { grammar.Epsilon });
                            followSet.Put(e.value, firstBeta);

                            // If A -> αBβ, and First(β) contains ε, then Follow(A) is in Follow(B)
                            if (containsEplison)
                            {
                                // Before goto, we restore the ε.
                                firstBeta.Add(grammar.Epsilon);
                                goto unionLeft;
                            }
                        }

                        // If we goes here, we update the first beta.
                        goto updateFirstBeta;

                    unionLeft:
                        if (!pendingCompute.ContainsKey(e.value))
                            pendingCompute.Add(e.value, new HashSet<ProductionSymbol>());
                        pendingCompute[e.value].Add(prod.Left);
                    }

                updateFirstBeta:
                    // Then we update First(β).
                    // Compute First(Xi) first, actually we have compute all terminals,
                    // but nonterminals are not.
                    var curPosFirst = new HashSet<ProductionSymbol>();
                    if (e.value.Type == ProductionSymbol.SymbolType.Terminal)
                        curPosFirst.Add(e.value);
                    else
                        curPosFirst = firstSet.Get(e.value);

                    // Now we check if First(Xi) contains ε
                    // If so, we then union firstBeta with First(Xi)
                    // else, firstBeta = First(Xi)
                    if (curPosFirst.Contains(grammar.Epsilon))
                        firstBeta.UnionWith(curPosFirst);
                    else
                        firstBeta = curPosFirst;
                }
            }

            // Finish computing
            foreach (var e in pendingCompute.Keys)
            {
                var pendingComputeStack = new Stack<ProductionSymbol>();
                pendingComputeStack.Push(e);

                while (pendingComputeStack.Count > 0)
                {
                    var topSymbol = pendingComputeStack.Peek();

                    var stackSize = pendingComputeStack.Count;
                    foreach (var s in pendingCompute[topSymbol])
                        if (!s.Equals(topSymbol))
                        {
                            // Follow(s) hasn't been computed, push into stack
                            if (!followSet.Contains(s))
                                pendingComputeStack.Push(s);
                            else
                                followSet.Put(topSymbol, followSet.Get(s));
                        }

                    // The size of stack hasn't change, which means
                    // Follow(topSymbol) has been computed.
                    // So we pop it.
                    if (stackSize == pendingComputeStack.Count)
                        pendingComputeStack.Pop();
                }
            }
        }
    }
}
