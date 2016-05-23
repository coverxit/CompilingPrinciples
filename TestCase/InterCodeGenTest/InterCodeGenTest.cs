using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using CompilingPrinciples.IntermediateCodeGenCore;
using CompilingPrinciples.ParserCore;
using CompilingPrinciples.LexerCore;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.TestCase
{
    public class InterCodeGenTest
    {
        private static void Traverse(TreeNode parent, TreeNode<SDTTreeNodeEntry> pNode)
        {
            var node = new TreeNode(pNode.Value.Type == SDTTreeNodeEntry.TypeEnum.Action ? "Action" : pNode.Value.Symbol.Symbol.ToString());
            parent.Nodes.Add(node);

            foreach (var e in pNode.Children)
                Traverse(node, e);
        }

        public static void LaunchTest()
        {
            var se_stream = new FileStream("SLRParserContext.ctx", FileMode.Open);
            
            var st = new SymbolTable();
            var parser = Parser.CreateFromContext(se_stream, st, null);

            var fs = new FileStream("ParserTest.lc", FileMode.Open);
            var result = parser.Parse(fs);

            var gen = new IntermediateCodeGen(st, result);

            var form = new TreeTest();
            var root = new TreeNode("P'");
            form.treeView.Nodes.Add(root);
            Traverse(root, gen.SDTTree);
            
            foreach (var e in ThreeAddrCodeFormatter.Format(gen.Generate()))
                Console.WriteLine(e);

            Application.EnableVisualStyles();
            Application.Run(form);

            fs.Close();
            se_stream.Close();

            Console.ReadLine();
        }
    }
}
