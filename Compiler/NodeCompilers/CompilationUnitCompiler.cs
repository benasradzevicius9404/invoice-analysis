using Analyzers.Analyzers;
using Lexer.SyntaxAnalysis;
using System.Linq;

namespace Compiler.NodeCompilers
{
    public class CompilationUnitCompiler : Compiler<IAnalyzer>
    {
        public override IAnalyzer Compile(MainCompiler compiler, CompilationContext context, SyntaxNode node)
        {
            return node.Nodes.Select(n => compiler.Compile(n, context)).OfType<IAnalyzer>().First();
        }
    }
}
