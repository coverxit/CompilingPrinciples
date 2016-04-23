using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.ParserModule;

namespace CompilingPrinciples.Utility
{
    public static class CompilerHelper
    {
        private static CodeCompileUnit GenerateParserCompileUnit(string sourceFilePath)
        {
            var stmts = new CodeStatement[]
            {
                // var symbolTable = new SymbolTable();
                new CodeVariableDeclarationStatement(
                    new CodeTypeReference(typeof(SymbolTable)), "symbolTable",
                    new CodeObjectCreateExpression(typeof(SymbolTable))
                ),
                
                // var ctxStream = new FileStream(ctxPath, FileMode.Open)
                new CodeVariableDeclarationStatement(
                    new CodeTypeReference(typeof(FileStream)), "ctxStream",
                    new CodeObjectCreateExpression(
                        typeof(FileStream),
                        new CodePrimitiveExpression(Path.GetFileNameWithoutExtension(sourceFilePath) + ".ctx"),
                        new CodePropertyReferenceExpression(
                            new CodeTypeReferenceExpression(typeof(FileMode)), "Open"
                        )
                    )
                ),

                // Parser parser = Parser.CreateFromContext(ctxStream, symbolTable);
                new CodeVariableDeclarationStatement(
                    new CodeTypeReference(typeof(Parser)), "parser",
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression(typeof(Parser)), "CreateFromContext",
                        new CodeVariableReferenceExpression("ctxStream"),
                        new CodeVariableReferenceExpression("symbolTable")
                    )
                ),

                // var inputStream = Console.OpenStandardInput();
                new CodeVariableDeclarationStatement(
                    new CodeTypeReference(typeof(Stream)), "inputStream",
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression(typeof(Console)), "OpenStandardInput"
                    )
                ),

                // var ops = parser.Parse(inputStream);
                new CodeVariableDeclarationStatement(
                    new CodeTypeReference(typeof(List<Tuple<string, string>>)), "ops",
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("parser"), "Parse",
                        new CodeVariableReferenceExpression("inputStream")
                    )
                ),
                
                // empty line
                new CodeSnippetStatement(""),

                // Console.WriteLine("{0,-40} {1}", "SYMBOLS", "ACTION");
                new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression(typeof(Console)), "WriteLine",
                        new CodePrimitiveExpression("{0,-40} {1}"),
                        new CodePrimitiveExpression("SYMBOLS"),
                        new CodePrimitiveExpression("ACTION")
                    )
                ),

                // for (int i = 0; i < ops.Count; i++)
                new CodeIterationStatement(
                    new CodeVariableDeclarationStatement(
                        new CodeTypeReference(typeof(int)), "i",
                        new CodePrimitiveExpression(0)
                    ),
                    new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("i"),
                        CodeBinaryOperatorType.LessThan,
                        new CodePropertyReferenceExpression(
                            new CodeVariableReferenceExpression("ops"), "Count"
                        )
                    ),
                    new CodeAssignStatement(
                        new CodeVariableReferenceExpression("i"),
                        new CodeBinaryOperatorExpression(
                            new CodeVariableReferenceExpression("i"),
                            CodeBinaryOperatorType.Add,
                            new CodePrimitiveExpression(1)
                        )
                    ),
                    // Console.WriteLine("{0,-40} {1}", op.Item2, op.Item1);
                    new CodeStatement[] {
                        new CodeExpressionStatement (
                            new CodeMethodInvokeExpression(
                                new CodeTypeReferenceExpression(typeof(Console)), "WriteLine",
                                new CodePrimitiveExpression("{0,-40} {1}"),
                                new CodePropertyReferenceExpression(
                                    new CodeIndexerExpression(
                                        new CodeVariableReferenceExpression("ops"),
                                        new CodeVariableReferenceExpression("i")
                                    ), "Item2"
                                ),
                                new CodePropertyReferenceExpression(
                                    new CodeIndexerExpression(
                                        new CodeVariableReferenceExpression("ops"),
                                        new CodeVariableReferenceExpression("i")
                                    ), "Item1"
                                )
                            )
                        )
                    }
                ),

                // empty line
                new CodeSnippetStatement(""),

                // ctxStream.Close();
                new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(
                            new CodeVariableReferenceExpression("ctxStream"), "Close"
                        )
                    )
                ),

                // inputStream.Close();
                new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(
                            new CodeVariableReferenceExpression("inputStream"), "Close"
                        )
                    )
                )
            };

            // public static void Main(string[] args)
            var entryPoint = new CodeEntryPointMethod();
            foreach (var s in stmts)
                entryPoint.Statements.Add(s);

            // class Program
            var parserClass = new CodeTypeDeclaration("Program");
            parserClass.Attributes = MemberAttributes.Public;
            parserClass.IsClass = true;
            parserClass.Members.Add(entryPoint);
            parserClass.Comments.Add(new CodeCommentStatement("Generated by Shindo's Parser Generator"));

            var ns = new CodeNamespace("AutoGeneratedParser");
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Types.Add(parserClass);

            // unit
            var unit = new CodeCompileUnit();
            unit.Namespaces.Add(ns);
            return unit;
        }

        public static string CompileParser(string sourceFilePath)
        {
            var appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\";

            var codeProvider = CodeDomProvider.CreateProvider(
                Path.GetExtension(sourceFilePath) == ".cs" ? "CSharp" : "VisualBasic"
            );
            var sw = new StringWriter();

            var cu = GenerateParserCompileUnit(sourceFilePath);
            codeProvider.GenerateCodeFromCompileUnit(cu, sw, null);

            var param = new CompilerParameters();
            param.GenerateExecutable = true;
            param.OutputAssembly = Path.ChangeExtension(sourceFilePath, "exe");
            param.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            param.ReferencedAssemblies.Add(appPath + "CompilingPrinciples.LexicalAnalyzer.dll");
            param.ReferencedAssemblies.Add(appPath + "CompilingPrinciples.SyntaxAnalyzer.dll");
            codeProvider.CompileAssemblyFromDom(param, cu);

            // Copy dlls
            if (Path.GetDirectoryName(sourceFilePath) != Path.GetDirectoryName(Application.ExecutablePath))
            {
                var destPath = Path.GetDirectoryName(sourceFilePath) + "\\";

                try
                {
                    File.Copy(appPath + "CompilingPrinciples.LexicalAnalyzer.dll", destPath + "CompilingPrinciples.LexicalAnalyzer.dll");
                    File.Copy(appPath + "CompilingPrinciples.SyntaxAnalyzer.dll", destPath + "CompilingPrinciples.SyntaxAnalyzer.dll");
                }
                catch (Exception)
                { }
            }

            return sw.ToString();
        }
    }
}
