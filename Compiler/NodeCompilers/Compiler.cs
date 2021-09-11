using Lexer.SyntaxAnalysis;

namespace Compiler.NodeCompilers
{
    public abstract class Compiler<T> : INodeCompiler
    {
        object INodeCompiler.Compile(MainCompiler compiler, CompilationContext context, SyntaxNode node) => Compile(compiler, context, node);

        public abstract T Compile(MainCompiler compiler, CompilationContext context, SyntaxNode node);
    }
}
