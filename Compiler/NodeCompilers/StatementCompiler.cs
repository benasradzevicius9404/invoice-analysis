using Lexer.SyntaxAnalysis;
using System.Linq;

namespace Compiler.NodeCompilers
{
    public class StatementCompiler : Compiler<object>
    {
        public override object Compile(MainCompiler compiler, CompilationContext context, SyntaxNode node)
        {
            return compiler.Compile(node.Nodes.First(), context);
        }
    }
}
