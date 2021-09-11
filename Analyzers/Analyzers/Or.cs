using System.Linq;

namespace Analyzers.Analyzers
{
    public class Or : IAnalyzer
    {
        private readonly IAnalyzer[] analyzers;
        public string Description => $"({string.Join(") OR (", analyzers.Select(x => x.Description))})";

        public Or(params IAnalyzer[] analyzers)
        {
            this.analyzers = analyzers;
        }

        public AnalyzerResult Analyze(TimeEntry entry)
        {
            var firstMatch = analyzers.Select(a => a.Analyze(entry)).FirstOrDefault(x => x.Matched);

            if (firstMatch == null)
            {
                return new AnalyzerResult(entry, Description, null);
            }

            return new AnalyzerResult(entry, Description, firstMatch.MatchedPart);
        }
    }
}