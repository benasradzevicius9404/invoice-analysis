namespace Analyzers.Analyzers
{
    public record AnalyzerResult(TimeEntry Entry, string FullRule, string MatchedPart)
    {
        public bool Matched => MatchedPart != null;
    }
}