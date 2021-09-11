using Lexer.LexicalAnalysis;
using System;
using System.Collections.Generic;

namespace Lexer.SyntaxAnalysis
{
    public class AnalyzerParser : INodeParser
    {
        public ParseResult Parse(IEnumerator<Token> tokens, SyntaxParser parser)
        {
            if (tokens.Current.Type != TokenType.AnalyzerReference)
            {
                return ParseResult.Fail(new SyntaxError(tokens.Current, "Expected to be an analyzer"));
            }

            var analyzerReference = new SyntaxToken(tokens.Current);
            tokens.MoveNext();

            var parameters = parser.ParseNode(SyntaxNodeType.ParameterList, tokens);

            return parameters.IfSuccess(x => new SyntaxNode(SyntaxNodeType.Analyzer, analyzerReference, x));
        }
    }
}
