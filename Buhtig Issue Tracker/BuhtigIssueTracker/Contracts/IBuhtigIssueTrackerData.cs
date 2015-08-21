namespace BuhtigIssueTracker.Contracts
{
    using System.Collections.Generic;

    using Wintellect.PowerCollections;

    public interface IBuhtigIssueTrackerData
    {
        User LoggedUser { get; set; }

        IDictionary<string, User> UsersByUsername { get; }

        OrderedDictionary<int, Issue> IssuesById { get; }

        MultiDictionary<string, Issue> IssuesByUsername { get; }

        MultiDictionary<string, Issue> IssuesByTags { get; }

        MultiDictionary<User, Comment> CommentsByUser { get; }
    }
}
