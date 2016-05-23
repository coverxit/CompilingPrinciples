using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using CompilingPrinciples.LexerCore;
using CompilingPrinciples.ParserCore;
using CompilingPrinciples.SymbolEnvironment;

namespace CompilingPrinciples.IntermediateCodeGenCore
{
    public class IntermediateCodeGen
    {
        private SymbolTable symbolTable;
        private TreeNode<SDTTreeNodeEntry> sdtTree;
        private List<string> threeAddrCode = new List<string>();
        private LabelManager labelMan;
        private TempVarManager tempVarMan;
        private int errLine;

        public int ErrorLine
        {
            get { return errLine; }
        }

        public List<string> ThreeAddrCode
        {
            get { return new List<string>(threeAddrCode); }
        }

        public IntermediateCodeGen(SymbolTable symTable, TreeNode<ParseTreeNodeEntry> parseTreeRoot)
        {
            this.symbolTable = symTable;

            sdtTree = new TreeNode<SDTTreeNodeEntry>(new SDTTreeNodeEntry(parseTreeRoot.Value));
            BuildSDTTree(parseTreeRoot, sdtTree);
        }

        private void TraverseSDTTree(TreeNode<SDTTreeNodeEntry> node)
        {
            if (node.Value.Type == SDTTreeNodeEntry.TypeEnum.Action)
                node.Value.Action(node.Parent.Value, 
                                  node.Siblings.Where(e => e.Value.Type == SDTTreeNodeEntry.TypeEnum.Symbol)
                                               .Select(e => e.Value).ToArray());

            foreach (var e in node.Children)
                TraverseSDTTree(e);
        }

        public void Generate()
        {
            threeAddrCode.Clear();
            labelMan = new LabelManager();
            tempVarMan = new TempVarManager();

            TraverseSDTTree(sdtTree);
        }

        // Chapter 6.5.2
        private string Widen(string name, VarType.TypeEnum type, VarType.TypeEnum to)
        {
            if (type == to) return name;
            else if (type == VarType.TypeEnum.Int && to == VarType.TypeEnum.Float)
            {
                var temp = tempVarMan.Create().ToString();
                GenCode(temp, " = (float) ", name);
                return temp;
            }
            else return string.Empty; // ERROR 
        }

        // Global varaibles in SDT
        private int offset = 0;

        // According to Chapter 5.4.3
        private void BuildSDTTree(TreeNode<ParseTreeNodeEntry> parseTreeNode, TreeNode<SDTTreeNodeEntry> node)
        {
            if (parseTreeNode.Value.Symbol.Type == ProductionSymbol.SymbolType.NonTerminal)
            {
                var prod = parseTreeNode.Value.Data as string;
                
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
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[1].Attributes = labelMan.Create();
                        }));

                        //      S   { gen('L' S.next ':'); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            GenCode(right[1].Attributes as Label, ":");    
                        }));
                        break;

                    case "D -> L id ; D":
                        // D -> L id ;   { top.set(id.lexeme, L.type, offset); 
                        //                offset = offset + L.width; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            var attr = right[0].Attributes as Tuple<VarType.TypeEnum, int>;

                            // top.set(id.lexeme, L.type, offset); 
                            var entry = symbolTable.Get((right[1].Symbol.Data as Identifier).GetValue());
                            entry.Type = attr.Item1;
                            entry.Offset = offset;

                            // offset = offset + L.width;
                            offset += attr.Item2;
                        }));

                        // D1
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[3].Value));
                        break;

                    case "D -> ε":
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        break;

                    case "L -> int":
                    case "L -> float":
                        // L -> int			{ L.type = integer; L.width = 4; }
                        // L-> float        { L.type = float; L.width = 8; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            var token = right[0].Symbol.Data as VarType;
                            left.Attributes = new Tuple<VarType.TypeEnum, int>(token.Type, token.Width);
                        }));
                        break;

                    case "S -> id = E ;":
                        // S -> id = E;		{ gen(id.lexeme '=' E.name); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[3].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            var token = right[0].Symbol.Data as Identifier;
                            var id = symbolTable.Get(token.GetValue());
                            var E = right[2].Attributes as Tuple<string, VarType.TypeEnum>;

                            if (VarType.Max(id.Type, E.Item2) != id.Type)
                            {
                                errLine = token.Line;
                                throw new IntermediateCodeGenException(token.Line, "type mismatch");
                            }
                            
                            var Ewiden = Widen(E.Item1, E.Item2, id.Type);
                            if (string.IsNullOrEmpty(Ewiden))
                            {
                                errLine = token.Line;
                                throw new IntermediateCodeGenException(token.Line, "type mismatch");
                            }

                            GenCode(id.Lexeme, " = ", Ewiden);
                        }));
                        break;

                    case "S -> if ( C ) S":
                        // S -> if (			{ C.true = fall; C.false = S.next; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[2].Attributes = new Tuple<Label, Label>(Label.Fall, left.Attributes as Label);
                        }));

                        //          C )         { S1.next = S.next; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[3].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[4].Attributes = left.Attributes as Label;
                        }));

                        //          S1
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[4].Value));
                        break;

                    case "S -> if ( C ) S else S":
                        // S -> if (			{ C.true = fall; C.false = newlabel(); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[2].Attributes = new Tuple<Label, Label>(Label.Fall, labelMan.Create());
                        }));

                        //      C )             { S1.next = S.next; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[3].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[4].Attributes = left.Attributes as Label;
                        }));

                        //      S1              { gen('goto L' S.next); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[4].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            GenCode("goto ", left.Attributes as Label);
                        }));

                        //      else            { S2.next = S.next; gen('L' C.false ':'); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[5].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[6].Attributes = left.Attributes as Label;
                            GenCode((right[2].Attributes as Tuple<Label, Label>).Item2, ":");
                        }));

                        //      S2
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[6].Value));
                        break;

                    case "S -> while ( C ) S":
                        // S -> while (		{ while.begin = newlabel(); gen('L' while.begin ':');
                        //                    C.true = fall; C.false = S.next; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[0].Attributes = labelMan.Create();
                            GenCode(right[0].Attributes as Label, ":");

                            right[2].Attributes = new Tuple<Label, Label>(Label.Fall, left.Attributes as Label);
                        }));

                        //      C )			{ S1.next = while.begin; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[3].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[4].Attributes = right[0].Attributes as Label;
                        }));

                        //      S1          { gen('goto L' while.begin); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[4].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            GenCode("goto ", right[0].Attributes as Label);
                        }));
                        break;

                    case "S -> S S":
                        // S -> 		     { S1.next = newlabel(); }
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[0].Attributes = labelMan.Create();
                        }));

                        //      S1           { S2.next = S.next; gen('L' S1.next ':'); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            right[1].Attributes = left.Attributes as Label;
                            GenCode(right[0].Attributes as Label, ":");
                        }));

                        //      S2
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        break;

                    case "C -> E > E":
                    case "C -> E < E":
                    case "C -> E == E":
                    case "C -> E <= E":
                    case "C -> E >= E":
                    case "C -> E != E":
                        // C -> E1 rel E2		{ condition = E1.name ' rel ' E2.name
                        //                        if C.true != fall and C.false != fall then
                        //                          gen('if' condition 'goto' C.true); || gen('goto' C.false);
                        //                        else if C.true != fall then gen('if' condition 'goto L' C.true);
                        //                        else if C.false != fall then gen('ifFalse' condition 'goto L' C.false);
                        //                        else ''; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            // right[1] = rel, we use this instead of right[0], which is E1,
                            // because right[0].Symbol.Data is the reduce production
                            var line = (right[1].Symbol.Data as Token).Line;

                            var E1 = right[0].Attributes as Tuple<string, VarType.TypeEnum>;
                            var E2 = right[2].Attributes as Tuple<string, VarType.TypeEnum>;
                            
                            var maxType = VarType.Max(E1.Item2, E2.Item2);
                            if (maxType == VarType.TypeEnum.Undefined)
                            {
                                errLine = line;
                                throw new IntermediateCodeGenException(line, "type mismatch");
                            }

                            var E1widen = Widen(E1.Item1, E1.Item2, maxType);
                            var E2widen = Widen(E2.Item1, E2.Item2, maxType);

                            if (string.IsNullOrEmpty(E1widen) || string.IsNullOrEmpty(E2widen))
                            {
                                errLine = line;
                                throw new IntermediateCodeGenException(line, "type mismatch");
                            }

                            var rel = right[1].Symbol.ToString();
                            string cond = E1widen + " " + (rel == "==" ? "=" : rel) + " " + E2widen;

                            var C = left.Attributes as Tuple<Label, Label>;
                            if (!C.Item1.IsFall && C.Item2.IsFall)
                            {
                                GenCode("if ", cond, " goto ", C.Item1);
                                GenCode("goto ", C.Item2);
                            }
                            else if (!C.Item1.IsFall)
                                GenCode("if ", cond, " goto ", C.Item1);
                            else if (!C.Item2.IsFall)
                                GenCode("ifFalse ", cond, " goto ", C.Item2);
                        }));
                        break;

                    case "E -> E + T":
                    case "E -> E - T":
                        // E -> E1 +/- T 		{ E.name = newtemp(); gen(E.name '=' E1.name '+'/'-' T.name); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            // right[1] = rel, we use this instead of right[0], which is E1,
                            // because right[0].Symbol.Data is the reduce production
                            var line = (right[1].Symbol.Data as Token).Line;

                            var E1 = right[0].Attributes as Tuple<string, VarType.TypeEnum>;
                            var T = right[2].Attributes as Tuple<string, VarType.TypeEnum>;

                            var maxType = VarType.Max(E1.Item2, T.Item2);
                            if (maxType == VarType.TypeEnum.Undefined)
                            {
                                errLine = line;
                                throw new IntermediateCodeGenException(line, "type mismatch");
                            }

                            var E1widen = Widen(E1.Item1, E1.Item2, maxType);
                            var Twiden = Widen(T.Item1, T.Item2, maxType);

                            if (string.IsNullOrEmpty(E1widen) || string.IsNullOrEmpty(Twiden))
                            {
                                errLine = line;
                                throw new IntermediateCodeGenException(line, "type mismatch");
                            }

                            var temp = tempVarMan.Create().ToString();
                            left.Attributes = new Tuple<string, VarType.TypeEnum>(temp, maxType);
                            GenCode(temp, " = ", E1widen, " ", right[1].Symbol.ToString(), " ", Twiden);
                        }));
                        break;

                    case "E -> T":
                        // E -> T 				{ E.name = T.name; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            left.Attributes = right[0].Attributes as Tuple<string, VarType.TypeEnum>;
                        }));
                        break;

                    case "T -> F":
                        // T -> F 				{ T.name = F.name; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            left.Attributes = right[0].Attributes as Tuple<string, VarType.TypeEnum>;
                        }));
                        break;

                    case "T -> T * F":
                    case "T -> T / F":
                        // T -> T1 */`/` F 	{ T.name = newtemp(); gen(T.name '=' T1.name '*'/'/' F.name); }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            // right[1] = rel, we use this instead of right[0], which is E1,
                            // because right[0].Symbol.Data is the reduce production
                            var line = (right[1].Symbol.Data as Token).Line;

                            var T1 = right[0].Attributes as Tuple<string, VarType.TypeEnum>;
                            var F = right[2].Attributes as Tuple<string, VarType.TypeEnum>;

                            var maxType = VarType.Max(T1.Item2, F.Item2);
                            if (maxType == VarType.TypeEnum.Undefined)
                            {
                                errLine = line;
                                throw new IntermediateCodeGenException(line, "type mismatch");
                            }

                            var T1widen = Widen(T1.Item1, T1.Item2, maxType);
                            var Fwiden = Widen(F.Item1, F.Item2, maxType);

                            if (string.IsNullOrEmpty(T1widen) || string.IsNullOrEmpty(Fwiden))
                            {
                                errLine = line;
                                throw new IntermediateCodeGenException(line, "type mismatch");
                            }

                            var temp = tempVarMan.Create().ToString();
                            left.Attributes = new Tuple<string, VarType.TypeEnum>(temp, maxType);
                            GenCode(temp, " = ", T1widen, " ", right[1].Symbol.ToString(), " ", Fwiden);
                        }));
                        break;

                    case "F -> ( E )":
                        // F -> ( E ) 			{ F.name = E.name; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[1].Value));
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[2].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            left.Attributes = right[1].Attributes as Tuple<string, VarType.TypeEnum>;
                        }));
                        break;

                    case "F -> id":
                        // F -> id				{ F.name = id.lexeme; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            var entry = symbolTable.Get((right[0].Symbol.Data as Identifier).GetValue());
                            left.Attributes = new Tuple<string, VarType.TypeEnum>(entry.Lexeme, entry.Type);
                        }));
                        break;
                        
                    case "F -> decimal":
                        // F-> decimal        { F.name = decimal.value; }
                        node.AddChild(new SDTTreeNodeEntry(parseTreeNode[0].Value));
                        node.AddChild(new SDTTreeNodeEntry(delegate (SDTTreeNodeEntry left, SDTTreeNodeEntry[] right)
                        {
                            left.Attributes = new Tuple<string, VarType.TypeEnum>(
                                (right[0].Symbol.Data as LexerCore.Decimal).GetValue().ToString(), VarType.TypeEnum.Int);
                        }));
                        break;
                }

                // parseTreeChild.Length should equal to sdtTreeChild.Length
                var parseTreeChild = parseTreeNode.Children.ToArray();
                var sdtTreeChild = node.Children.Where(e => e.Value.Type == SDTTreeNodeEntry.TypeEnum.Symbol).ToArray();

                for (int i = 0; i < parseTreeChild.Length; i++)
                    BuildSDTTree(parseTreeChild[i], sdtTreeChild[i]);
            }
        }

        private void GenCode(params object[] arg)
        {
            var sw = new StringWriter();
            foreach (var e in arg)
                sw.Write(e.ToString());

            threeAddrCode.Add(sw.ToString());
        }
    }
}
