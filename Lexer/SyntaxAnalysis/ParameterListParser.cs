using Lexer.LexicalAnalysis;
using System;
using System.Collections.Generic;

namespace Lexer.SyntaxAnalysis
{
    public class ParameterListParser : INodeParser
    {
        public ParseResult Parse(IEnumerator<Token> tokens, SyntaxParser parser)
        {
            if (tokens.Current.Type != TokenType.BraketOpen)
            {
                return ParseResult.Fail(new SyntaxError(tokens.Current, "Open braket expected"));
            }
            tokens.MoveNext();

            var parameters = new List<ISyntax>();

            while(true) {
                switch (tokens.Current.Type)
                {
                    case TokenType.AnalyzerReference:
                        var analyzer = parser.ParseNode(SyntaxNodeType.Analyzer, tokens);
                        if(analyzer.HasError)
                        {
                            return analyzer;
                        }

                        parameters.Add(analyzer.Node);
                        break;
                    case TokenType.VariableName:
                    case TokenType.StringLiteral:
                        parameters.Add(new SyntaxToken(tokens.Current));
                        tokens.MoveNext();
                        break;
                }

                if(tokens.Current.Type == TokenType.BraketClose)
                {
                    break;
                }

                if (tokens.Current.Type != TokenType.Comma)
                {
                    return ParseResult.Fail(new SyntaxError(tokens.Current, "Comma expected"));
                }

                tokens.MoveNext();
            }

            tokens.MoveNext(); // Consume BraketClose token


            return ParseResult.Success(new SyntaxNode(SyntaxNodeType.ParameterList, parameters.ToArray()));
        }
    }
}
