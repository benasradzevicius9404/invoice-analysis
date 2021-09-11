using System.Text.RegularExpressions;

namespace Lexer.LexicalAnalysis
{
    public record Grammer(LexicalRule[] Rules)
    {
        public static Grammer Default()
        {
            return new Grammer(new LexicalRule[] {
                new LexicalRule(new Regex("^\\s"), TokenType.WhiteSpace),
                new LexicalRule(new Regex("^="), TokenType.Assignment),
                new LexicalRule(new Regex("^;"), TokenType.Semicolon),
                new LexicalRule(new Regex("^\\$[_A-Za-z][_A-Za-z0-9]*"), TokenType.VariableName),
                new LexicalRule(new Regex("^\\("), TokenType.BraketOpen),
                new LexicalRule(new Regex("^\\)"), TokenType.BraketClose),
                new LexicalRule(new Regex("^[_A-Za-z][_A-Za-z0-9]*"), TokenType.AnalyzerReference),
                new LexicalRule(new Regex("^\".*?\"", RegexOptions.IgnoreCase), TokenType.StringLiteral),
                new LexicalRule(new Regex("^\\,"), TokenType.Comma),
                new LexicalRule(new Regex(".*"), TokenType.Unknown),
            });
        }
    }
}
