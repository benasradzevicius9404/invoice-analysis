using Analyzers;
using Analyzers.Analyzers;
using Lexer.LexicalAnalysis;
using Lexer.SyntaxAnalysis;
using System;
using System.Linq;
using System.Reflection;

namespace Compiler.NodeCompilers
{
    public class AnalyzerCompiler : Compiler<IAnalyzer>
    {
        public override IAnalyzer Compile(MainCompiler compiler, CompilationContext context, SyntaxNode node)
        {
            var analizerName = node.Tokens.First(x => x.Token.Type == TokenType.AnalyzerReference).Token.Lexeme.Value;
            var parameters = node.Nodes.First(x => x.Type == SyntaxNodeType.ParameterList).Children.Select(x => x switch
            {
                SyntaxToken { Token: { Type: TokenType.StringLiteral } } t => t.Token.Lexeme.Value.Replace("\"", ""),
                SyntaxNode { Type: SyntaxNodeType.Analyzer } n => compiler.Compile(n, context),
                SyntaxToken { Token: { Type: TokenType.VariableName } } t => context.Variables[t.Token.Lexeme.Value],
                _ => throw new System.NotImplementedException()
            });

            return (IAnalyzer)Activator.CreateInstance(
                Assembly.GetAssembly(typeof(TimeEntry)).GetType($"Analyzers.Analyzers.{analizerName}", true, true),
                parameters.ToArray()
            );
        }
    }
}
