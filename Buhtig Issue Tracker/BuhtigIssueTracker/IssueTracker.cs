namespace BuhtigIssueTracker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BuhtigIssueTracker.Contracts;

    public class IssueTracker : IIssueTracker
    {
        public IssueTracker(IBuhtigIssueTrackerData data)
        {
            this.Data = data as BuhtigIssueTrackerData;
        }

        public IssueTracker()
            : this(new BuhtigIssueTrackerData())
        {
        }

        private BuhtigIssueTrackerData Data { get; set; }

        public string RegisterUser(string username, string password, string confirmPassword)
        {
            if (this.Data.LoggedUser != null)
            {
                return "There is already a logged in user";
            }

            if (password != confirmPassword)
            {
                return "The provided passwords do not match";
            }

            if (this.Data.UsersByUsername.ContainsKey(username))
            {
                return string.Format("A user with username {0} already exists", username);
            }

            var user = new User(username, password);
            this.Data.UsersByUsername.Add(username, user);
            return string.Format("User {0} registered successfully", username);
        }

        public string LoginUser(string username, string password)
        {
            if (this.Data.LoggedUser != null)
            {
                return "There is already a logged in user";
            }

            if (!this.Data.UsersByUsername.ContainsKey(username))
            {
                return string.Format("A user with username {0} does not exist", username);
            }

            var user = this.Data.UsersByUsername[username];
            if (user.PasswortHash != User.HashPassword(password))
            {
                return string.Format("The password is invalid for user {0}", username);
            }

            this.Data.LoggedUser = user;

            return string.Format("User {0} logged in successfully", username);
        }

        public string LogoutUser()
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            string username = this.Data.LoggedUser.Username;
            this.Data.LoggedUser = null;
            return string.Format("User {0} logged out successfully", username);
        }

        public string CreateIssue(string title, string description, IssuePriority priority, string[] tags)
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            var issue = new Issue(title, description, priority, tags.Distinct().ToList());
            issue.Id = this.Data.NextIssueId;
            this.Data.IssuesById.Add(issue.Id, issue);
            this.Data.NextIssueId++;
            this.Data.IssuesByUsername[this.Data.LoggedUser.Username].Add(
                issue);
            foreach (var tag in issue.Tags)
            {
                this.Data.IssuesByTags[tag].Add(issue);
            }

            return string.Format("Issue {0} created successfully", issue.Id);
        }

        public string RemoveIssue(int issueId)
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            if (!this.Data.IssuesById.ContainsKey(issueId))
            {
                return string.Format("There is no issue with ID {0}", issueId);
            }

            var issue = this.Data.IssuesById[issueId];
            if (!this.Data.IssuesByUsername[this.Data.LoggedUser.Username].Contains(issue))
            {
                return string.Format(
                    "The issue with ID {0} does not belong to user {1}", 
                    issueId, 
                    this.Data.LoggedUser.Username);
            }

            this.Data.IssuesByUsername[this.Data.LoggedUser.Username].Remove(
                issue);
            foreach (var tag in issue.Tags)
            {
                this.Data.IssuesByTags[tag].Remove(issue);
            }

            this.Data.IssuesById.Remove(issue.Id);
            return string.Format("Issue {0} removed", issueId);
        }

        public string AddComment(int issueId, string text)
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            if (!this.Data.IssuesById.ContainsKey(issueId))
            {
                return string.Format("There is no issue with ID {0}", issueId);
            }

            var issue = this.Data.IssuesById[issueId];
            var comment = new Comment(this.Data.LoggedUser, text);
            issue.AddComment(comment);
            this.Data.CommentsByUser[this.Data.LoggedUser].Add(comment);
            return string.Format("Comment added successfully to issue {0}", issue.Id);
        }

        public string GetMyIssues()
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            var issues =
                this.Data.IssuesByUsername[this.Data.LoggedUser.Username];
            if (issues.Any())
            {
                // Performance: Totaly unnecessary foreach loop here!
                return string.Join(Environment.NewLine, issues.OrderByDescending(x => x.Priority).ThenBy(x => x.Title));
            }

            return "No issues";
        }

        public string GetMyComments()
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            var commentsByTheCurrentUser = this.Data.CommentsByUser[this.Data.LoggedUser];

            if (!commentsByTheCurrentUser.Any())
            {
                return "No comments";
            }

            return string.Join(Environment.NewLine, commentsByTheCurrentUser);
        }

        public string SearchForIssues(string[] tags)
        {
            if (tags.Length <= 0)
            {
                return "There are no tags provided";
            }

            var issues = new List<Issue>();
            foreach (var t in tags)
            {
                issues.AddRange(this.Data.IssuesByTags[t]);
            }

            if (!issues.Any())
            {
                return "There are no issues matching the tags provided";
            }

            var distinctIssues = issues.Distinct();

            return string.Join(
                Environment.NewLine,
                distinctIssues.OrderByDescending(x => x.Priority).ThenBy(x => x.Title));
        }
    }
}