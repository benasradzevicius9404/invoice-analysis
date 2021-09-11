using Analyzers.Analyzers;

namespace InvoiceAnalyzerApp.Models
{
    public record AnalyzerModel(ParsedAnalyzerModel Parsed, IAnalyzer Compiled)
    {
    }
}
