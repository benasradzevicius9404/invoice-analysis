namespace Analyzers.Analyzers
{
    public interface IAnalyzer
    {
        public string Description { get; }
        AnalyzerResult Analyze(TimeEntry entry);
    }
}