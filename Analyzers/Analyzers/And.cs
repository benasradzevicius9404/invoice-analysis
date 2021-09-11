using System.Linq;

namespace Analyzers.Analyzers
{
    public class And : IAnalyzer
    {
        private readonly IAnalyzer[] analyzers;
        public string Description => $"({string.Join(") AND (", analyzers.Select(x => x.Description))})";

        public And(params IAnalyzer[] analyzers)
        {
            this.analyzers = analyzers;
        }

        public AnalyzerResult Analyze(TimeEntry entry)
        {
            var results = analyzers.Select(a => a.Analyze(entry)).ToList();

            if (results.All(x => x.Matched))
            {
                return new AnalyzerResult(entry, Description, Description);
            }

            return new AnalyzerResult(entry, Description, null);
        }
    }
}