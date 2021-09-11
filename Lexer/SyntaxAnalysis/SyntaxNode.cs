using Lexer.LexicalAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lexer.SyntaxAnalysis
{
    public interface ISyntax { }

    public record SyntaxNode(SyntaxNodeType Type, params ISyntax[] Children) : ISyntax {
        public IEnumerable<SyntaxToken> Tokens => Children.OfType<SyntaxToken>();
        public IEnumerable<SyntaxNode> Nodes => Children.OfType<SyntaxNode>();

        public SyntaxToken GetToken(TokenType type) => Tokens.FirstOrDefault(x => x.Token.Type == type);
        public SyntaxNode GetNode(SyntaxNodeType type) => Nodes.FirstOrDefault(x => x.Type == type);
    }

    public record SyntaxToken(Token Token) : ISyntax { }

    public record SyntaxError(Token Token, string Error) { }

    public record ParseResult(SyntaxNode Node, SyntaxError Error) {
        public bool HasError => Error != null;

        public static ParseResult Success(SyntaxNode node) { return new ParseResult(node, null); }
        public static ParseResult Fail(SyntaxError error) { return new ParseResult(null, error); }

        public ParseResult IfSuccess(Func<SyntaxNode, SyntaxNode> convert)
        {
            if(HasError)
            {
                return this;
            }

            return ParseResult.Success(convert(Node));
        }
    }

    public static class ParseResultExtensions
    {
        public static ParseResult IfSuccess(this IEnumerable<ParseResult> results,  Func<SyntaxNode[], SyntaxNode> convert)
        {
            var firstError = results.FirstOrDefault(x => x.HasError);

            if(firstError != null)
            {
                return firstError;
            }

            return ParseResult.Success(convert(results.Select(x => x.Node).ToArray()));
        }
    }
}
