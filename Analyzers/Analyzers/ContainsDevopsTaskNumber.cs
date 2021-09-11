using System.Linq;
using System.Text.RegularExpressions;

namespace Analyzers.Analyzers
{
    public class ContainsDevopsTaskNumber : IAnalyzer
    {
        public string Description => "Contains a task number in description";

        public AnalyzerResult Analyze(TimeEntry entry)
        {
            var success = Regex.IsMatch(entry.Description, "#?[0-9]{5}");

            if (success)
            {
                return new AnalyzerResult(entry, Description, $"Contains task number");
            }

            return new AnalyzerResult(entry, Description, null);
        }
    }
}