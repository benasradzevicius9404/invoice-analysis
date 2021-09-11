using Analyzers.Analyzers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzers
{
    public class CustomAnalyzerDescription : IAnalyzer
    {
        private readonly IAnalyzer analyzer;

        public string Description { get; private set; }

        public CustomAnalyzerDescription(string description, IAnalyzer analyzer)
        {
            Description = description;
            this.analyzer = analyzer;
        }

        public AnalyzerResult Analyze(TimeEntry entry) => analyzer.Analyze(entry) switch { 
            { Matched: true } e => e with { MatchedPart = Description },
            { Matched: false} e => e
        };
    }
}
