namespace BuhtigIssueTracker.Tests
{
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestGetMyIssuesMethod
    {
        private IssueTracker tracker;

        [TestInitialize]
        public void TestInitialization()
        {
            this.tracker = new IssueTracker();
        }

        [TestMethod]
        public void TestGetIssuesSuccessfullyShouldReturnAppropriateMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");
            this.tracker.CreateIssue("New issue", "This is a new issue", IssuePriority.High, new[] { "new", "issue" });
            string message = this.tracker.GetMyIssues();

            var issue = new StringBuilder();
            issue.AppendLine("New issue")
                .AppendLine("Priority: ***")
                .AppendLine("This is a new issue")
                .Append("Tags: issue,new");

            Assert.AreEqual(issue.ToString(), message);
        }

        [TestMethod]
        public void TestGetIssuesWithNoIssuesSuccessfullyShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");

            string message = this.tracker.GetMyIssues();

            Assert.AreEqual("No issues", message);
        }

        [TestMethod]
        public void TestWithNonLoggedInUserShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");

            this.tracker.CreateIssue("New issue", "This is a new issue", IssuePriority.High, new[] { "new", "issue" });
            string message = this.tracker.GetMyIssues();

            Assert.AreEqual("There is no currently logged in user", message);
        }
    }
}
