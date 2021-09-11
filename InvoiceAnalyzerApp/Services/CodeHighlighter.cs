using Lexer.LexicalAnalysis;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceAnalyzerApp.Services
{
    public class CodeHighlighter
    {
        public MarkupString Highlight(string sourceCode, IEnumerable<Token> tokens)
        {
            return (MarkupString)tokens.Reverse().Aggregate(sourceCode, (highlighted, token) =>
            {
                switch (token.Type)
                {
                    case TokenType.StringLiteral:
                        return highlighted.Substring(0, token.Lexeme.StartPosition)
                            + $"<span style='color: orange;'>{token.Lexeme.Value}</span>"
                            + highlighted.Substring(token.Lexeme.StartPosition + token.Lexeme.Length);
                    case TokenType.AnalyzerReference:
                        return highlighted.Substring(0, token.Lexeme.StartPosition)
                            + $"<b>{token.Lexeme.Value}</b>"
                            + highlighted.Substring(token.Lexeme.StartPosition + token.Lexeme.Length);
                    case TokenType.VariableName:
                        return highlighted.Substring(0, token.Lexeme.StartPosition)
                            + $"<span style='color: blue;'>{token.Lexeme.Value}</span>"
                            + highlighted.Substring(token.Lexeme.StartPosition + token.Lexeme.Length);
                }

                return highlighted;
            });
        }
    }
}
