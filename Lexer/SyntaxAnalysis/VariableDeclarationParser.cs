using Lexer.LexicalAnalysis;
using System;
using System.Collections.Generic;

namespace Lexer.SyntaxAnalysis
{
    public class VariableDeclarationParser : INodeParser
    {
        public ParseResult Parse(IEnumerator<Token> tokens, SyntaxParser parser)
        {
            if(tokens.Current.Type != TokenType.VariableName)
            {
                return ParseResult.Fail(new SyntaxError(tokens.Current, "Variable name expected"));
            }

            var variableName = new SyntaxToken(tokens.Current);

            tokens.MoveNext();

            if(tokens.Current.Type != TokenType.Assignment)
            {
                return ParseResult.Fail(new SyntaxError(tokens.Current, "Assignment symbol was expected"));
            }
            tokens.MoveNext();

            if (tokens.Current.Type != TokenType.AnalyzerReference)
            {
                return ParseResult.Fail(new SyntaxError(tokens.Current, "Analyzer expected"));
            }

            var analyzer = parser.ParseNode(SyntaxNodeType.Analyzer, tokens);

            if(analyzer.HasError)
            {
                return analyzer;
            }

            return ParseResult.Success(new SyntaxNode(SyntaxNodeType.VariableDeclaration, variableName, analyzer.Node));
        }
    }
}
