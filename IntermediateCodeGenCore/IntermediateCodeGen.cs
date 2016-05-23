using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.ParserCore;
using CompilingPrinciples.SymbolEnvironment;

namespace CompilingPrinciples.IntermediateCodeGenCore
{
    public class IntermediateCodeGen
    {
        private class SDTTreeNodeEntry
        {
            public enum TypeEnum
            {
                Action = 0,
                Symbol = 1
            }

            private TypeEnum type;
            public TypeEnum Type
            {
                get { return type; }
            }

            private ParseTreeNodeEntry symbol;
            public ParseTreeNodeEntry Symbol
            {
                get { return symbol; }
            }

            private object data;
            public object Data
            {
                get { return data; }
                set { data = value; }
            }

            private Action<SDTTreeNodeEntry, SDTTreeNodeEntry[]> action;
            public Action<SDTTreeNodeEntry, SDTTreeNodeEntry[]> Action
            {
                get { return action; }
            }

            public SDTTreeNodeEntry(ParseTreeNodeEntry symbol, object data = null)
            {
                this.type = TypeEnum.Symbol;
                this.symbol = symbol;
                this.data = data;
            }

            public SDTTreeNodeEntry(Action<SDTTreeNodeEntry, SDTTreeNodeEntry[]> action, object data = null)
            {
                this.type = TypeEnum.Action;
                this.action = action;
                this.data = data;
            }
        }

        private SymbolTable symbolTable;
        private TreeNode<SDTTreeNodeEntry> sdtTree;
        private List<string> threeAddrCode = new List<string>();
        private LabelManager labelMan;
        private TempVarManager tempVarMan;

        public IntermediateCodeGen(SymbolTable symTable, TreeNode<ParseTreeNodeEntry> parseTreeRoot)
        {
            this.symbolTable = symTable;

            // A fake root, will be removed once the tree is built
            sdtTree = new TreeNode<SDTTreeNodeEntry>(null);
            BuildSDTTree(parseTreeRoot, sdtTree);

            // Tree is built, replace sdtTree to the true root
            sdtTree = sdtTree[0];
        }

        public List<string> Generate()
        {
            threeAddrCode.Clear();
            labelMan = new LabelManager();
            tempVarMan = new TempVarManager();

            sdtTree.Traverse((node) =>
            {
                if (node.Value.Type == SDTTreeNodeEntry.TypeEnum.Action)
                {
                    var siblings = node.Parent.Children.Select(e => e.Value).ToArray();
                    node.Value.Action(node.Parent.Value, siblings);
                }
            });
            return new List<string>(threeAddrCode);
        }

        // Global varaibles in SDT
        private int offset = 0;

        // According to Chapter 5.4.3
        private void BuildSDTTree(TreeNode<ParseTreeNodeEntry> parseTree, TreeNode<SDTTreeNodeEntry> parent)
        {
            if (parseTree.Value.Symbol.Type == ProductionSymbol.SymbolType.Terminal)
                parent.AddChild(new TreeNode<SDTTreeNodeEntry>(new SDTTreeNodeEntry(parseTree.Value)));
            else // ProductionSymbol.SymbolType.NonTerminal
            {
                var node = new TreeNode<SDTTreeNodeEntry>(new SDTTreeNodeEntry(parseTree.Value));
                var prod = parseTree.Value.Data as string;

                parent.AddChild(node);

                // Examine each node N on ParseTree, for production A -> α.
                // Add addtional children to N for the actions in α.
                switch (prod)
                {
                    case "P -> D S":
                        // P ->     { offset = 0; }
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            offset = 0;
                        }));

                        //      D   { S.next = newlabel(); }
                        node.AddChild(new SDTTreeNodeEntry(parseTree[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[1].Data = labelMan.Create();
                        }));

                        //      S   { gen('L' S.next ':'); }
                        node.AddChild(new SDTTreeNodeEntry(parseTree[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            GenCode(right[1].Data as Label);    
                        }));
                        break;

                    case "D -> L id ; D":
                        // D -> L id ;   { top.set(id.lexeme, L.type, offset); 
                        //                offset = offset + L.width; }
                        node.AddChild(new SDTTreeNodeEntry(parseTree[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTree[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTree[2].Value));

                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            var attr = right[0].Data as Tuple<VarType.TypeEnum, int>;

                            var entry = symbolTable.Get((right[1].Symbol.Data as Identifier).GetValue());
                            entry.Type = attr.Item1;
                            entry.Offset = attr.Item2;

                            offset = offset + attr.Item2;
                        }));

                        // D1
                        node.AddChild(new SDTTreeNodeEntry(parseTree[3].Value));
                        break;

                    case "L -> int":
                    case "L -> float":
                        // L -> int			{ L.type = integer; L.width = 4; }
                        // L-> float        { L.type = float; L.width = 8; }
                        node.AddChild(new SDTTreeNodeEntry(parseTree[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            var token = right[0].Symbol.Data as VarType;
                            left.Data = new Tuple<VarType.TypeEnum, int>(token.Type, token.Width);
                        }));
                        break;
                }

                foreach (var e in parseTree.Children)
                    BuildSDTTree(e, node);
            }
        }

        private void GenCode<T>(T code)
        {
            threeAddrCode.Add(code.ToString());
        }
    }
}
