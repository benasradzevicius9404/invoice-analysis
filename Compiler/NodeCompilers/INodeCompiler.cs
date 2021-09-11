using Lexer.SyntaxAnalysis;

namespace Compiler.NodeCompilers
{
    public interface INodeCompiler
    {
        object Compile(MainCompiler compiler, CompilationContext context, SyntaxNode node);
    }
}
