using Lexer.LexicalAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Lexer.SyntaxAnalysis
{
    public class CompilationUnitParser : INodeParser
    {
        public ParseResult Parse(IEnumerator<Token> tokens, SyntaxParser parser)
        {
            var statements = new List<ParseResult>();

            while (tokens.Current.Type != TokenType.End)
            {
                var result = parser.ParseNode(SyntaxNodeType.Statement, tokens);

                if(result.HasError)
                {
                    return result;
                }

                statements.Add(result);
            }

            tokens.MoveNext();

            return statements.IfSuccess(x => new SyntaxNode(SyntaxNodeType.CompilationUnit, x));
        }
    }
}
