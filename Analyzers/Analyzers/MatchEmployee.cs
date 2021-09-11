using System;

namespace Analyzers.Analyzers
{
    public class MatchEmployee : IAnalyzer
    {
        private readonly string employee;

        public string Description => $"Is employee '{employee}'";

        public MatchEmployee(string employee)
        {
            this.employee = employee.ToLower();
        }

        public AnalyzerResult Analyze(TimeEntry entry)
        {
            if (entry.Employee.ToLower().Equals(employee))
            {
                return new AnalyzerResult(entry, Description, Description);
            }

            return new AnalyzerResult(entry, Description, null);
        }
    }
}