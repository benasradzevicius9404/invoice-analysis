using Compiler.NodeCompilers;
using Lexer.SyntaxAnalysis;
using System.Collections.Generic;

namespace Compiler
{
    public class MainCompiler
    {
        private Dictionary<SyntaxNodeType, INodeCompiler> nodeCompilers = new Dictionary<SyntaxNodeType, INodeCompiler>();

        public MainCompiler()
        {
            nodeCompilers.Add(SyntaxNodeType.CompilationUnit, new CompilationUnitCompiler());
            nodeCompilers.Add(SyntaxNodeType.Statement, new StatementCompiler());
            nodeCompilers.Add(SyntaxNodeType.Analyzer, new AnalyzerCompiler());
            nodeCompilers.Add(SyntaxNodeType.VariableDeclaration, new VariableDeclarationCompilation());
        }

        public object Compile(SyntaxNode node, CompilationContext context = null)
        {
            var compilationContext = context ?? new CompilationContext();

            return nodeCompilers[node.Type].Compile(this, compilationContext, node);
        }
    }
}
