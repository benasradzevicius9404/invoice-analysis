namespace Analyzers.Analyzers
{
    public class Not : IAnalyzer
    {
        private readonly IAnalyzer analizer;

        public string Description => $"NOT ({analizer.Description})";

        public Not(IAnalyzer analizer)
        {
            this.analizer = analizer;
        }

        public AnalyzerResult Analyze(TimeEntry entry)
        {
            var result = analizer.Analyze(entry);

            if (result.Matched)
            {
                return new AnalyzerResult(entry, Description, null);
            }

            return new AnalyzerResult(entry, Description, Description);
        }
    }
}