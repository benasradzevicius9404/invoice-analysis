using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Analyzers.Analyzers
{
    public class ContainsDescription : IAnalyzer
    {
        private readonly string[] text;

        public string Description => $"Contains in description '{string.Join(",", text)}'";

        public ContainsDescription(params string[] text)
        {
            this.text = text.Select(x =>x.ToLower()).ToArray();
        }

        public AnalyzerResult Analyze(TimeEntry entry)
        {
            var matchedWord = text.FirstOrDefault(x => Regex.IsMatch(entry.Description.ToLower(), @"\b" + Regex.Escape(x) + @"\b"));

            if (matchedWord != null)
            {
                return new AnalyzerResult(entry, Description, $"Contains '{matchedWord}'");
            }

            return new AnalyzerResult(entry, Description, null);
        }
    }
}