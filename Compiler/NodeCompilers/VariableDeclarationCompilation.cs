using Analyzers;
using Analyzers.Analyzers;
using Lexer.LexicalAnalysis;
using Lexer.SyntaxAnalysis;
using System.Linq;

namespace Compiler.NodeCompilers
{
    public class VariableDeclarationCompilation : Compiler<object>
    {
        public override object Compile(MainCompiler compiler, CompilationContext context, SyntaxNode node)
        {
            var variableName = node.Tokens.First(x => x.Token.Type == TokenType.VariableName);
            var value = compiler.Compile(node.Nodes.First(x => x.Type == SyntaxNodeType.Analyzer), context);

            context.Variables[variableName.Token.Lexeme.Value] = new CustomAnalyzerDescription(ToTitle(variableName.Token.Lexeme.Value), value as IAnalyzer);

            return null;
        }

        private string ToTitle(string camelCase)
        {
            return camelCase.Substring(1, 1).ToUpper() + new string(camelCase.Substring(2).SelectMany(x => char.IsUpper(x) ? new[] { ' ', char.ToLower(x) } : new[] { x }).ToArray());
        }
    }
}
