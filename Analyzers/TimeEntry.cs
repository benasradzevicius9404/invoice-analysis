using System;

namespace Analyzers
{
    public record TimeEntry(decimal Quantity, DateTime Date, string Project, string Task, string Employee, string Description) { }
}