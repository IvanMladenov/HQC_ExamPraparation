namespace BuhtigIssueTracker
{
    using System.Collections.Generic;

    using BuhtigIssueTracker.Contracts;

    using Wintellect.PowerCollections;

    public class BuhtigIssueTrackerData : IBuhtigIssueTrackerData
    {
        public BuhtigIssueTrackerData()
        {
            this.NextIssueId = 1;
            this.UsersByUsername = new Dictionary<string, User>();

            this.IssuesById = new OrderedDictionary<int, Issue>();
            this.IssuesByUsername = new MultiDictionary<string, Issue>(true);
            this.IssuesByTags = new MultiDictionary<string, Issue>(true);

            this.CommentsByUser = new MultiDictionary<User, Comment>(true);
        }

        public int NextIssueId { get; set; }

        public User LoggedUser { get; set; }

        public IDictionary<string, User> UsersByUsername { get; set; }

        public OrderedDictionary<int, Issue> IssuesById { get; set; }

        public MultiDictionary<string, Issue> IssuesByUsername { get; set; }

        public MultiDictionary<string, Issue> IssuesByTags { get; set; }

        public MultiDictionary<User, Comment> CommentsByUser { get; set; }
    }
}