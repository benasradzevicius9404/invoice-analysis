using Lexer.LexicalAnalysis;
using System;
using System.Collections.Generic;

namespace Lexer.SyntaxAnalysis
{
    public class StatementParser : INodeParser
    {
        public ParseResult Parse(IEnumerator<Token> tokens, SyntaxParser parser)
        {
            ParseResult childNode;
            if (tokens.Current.Type == TokenType.VariableName)
            {
                childNode = parser.ParseNode(SyntaxNodeType.VariableDeclaration, tokens);
            }
            else if (tokens.Current.Type == TokenType.AnalyzerReference)
            {
                childNode = parser.ParseNode(SyntaxNodeType.Analyzer, tokens);
            }
            else
            {
                return ParseResult.Fail(new SyntaxError(tokens.Current, "Expected a variable declaration or an analyzer"));
            }

            if(childNode.HasError)
            {
                return childNode;
            }

            if(tokens.Current.Type != TokenType.Semicolon)
            {
                return ParseResult.Fail(new SyntaxError(tokens.Current, "Expected semicolon"));
            }

            tokens.MoveNext();

            return ParseResult.Success(new SyntaxNode(SyntaxNodeType.Statement, childNode.Node));
        }
    }
}
