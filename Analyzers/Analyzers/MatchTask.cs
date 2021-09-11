using System;

namespace Analyzers.Analyzers
{
    public class MatchTask : IAnalyzer
    {
        private readonly string task;

        public string Description => $"Task is '{task}'";

        public MatchTask(string task)
        {
            this.task = task.ToLower();
        }

        public AnalyzerResult Analyze(TimeEntry entry)
        {
            if (entry.Task.ToLower().Equals(task))
            {
                return new AnalyzerResult(entry, Description, Description);
            }

            return new AnalyzerResult(entry, Description, null);
        }
    }
}