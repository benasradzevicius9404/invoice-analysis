using System.Text.RegularExpressions;

namespace Lexer.LexicalAnalysis
{
    public record LexicalRule(Regex Pattern, TokenType TokenType) { }
}
