using Lexer.LexicalAnalysis;
using System.Collections.Generic;

namespace InvoiceAnalyzerApp.Models
{
    public record ParsedAnalyzerModel(string SourceCode, IReadOnlyCollection<Token> Tokens)
    {
    }
}
