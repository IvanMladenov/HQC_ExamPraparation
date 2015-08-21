namespace BuhtigIssueTracker.Contracts
{
    using System.Collections.Generic;

    public interface IInputHandler
    {
        string ActionName { get; }

        IDictionary<string, string> Parameters { get; }
    }
}
