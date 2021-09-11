using System;

namespace Analyzers.Analyzers
{
    public class MatchProject : IAnalyzer
    {
        private readonly string project;

        public string Description => $"Is project '{project}'";

        public MatchProject(string project)
        {
            this.project = project.ToLower();
        }

        public AnalyzerResult Analyze(TimeEntry entry)
        {
            if (entry.Project.ToLower().Equals(project))
            {
                return new AnalyzerResult(entry, Description, Description);
            }

            return new AnalyzerResult(entry, Description, null);
        }
    }
}