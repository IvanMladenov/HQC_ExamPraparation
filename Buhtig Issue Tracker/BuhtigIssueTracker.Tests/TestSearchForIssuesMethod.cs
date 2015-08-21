namespace BuhtigIssueTracker.Tests
{
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestSearchForIssuesMethod
    {
        private IssueTracker tracker;

        [TestInitialize]
        public void TestInitialization()
        {
            this.tracker = new IssueTracker();
        }

        [TestMethod]
        public void TestSuccessfullSearchShouldReturnAppropriateMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");
            this.tracker.CreateIssue("New issue", "This is a new issue", IssuePriority.High, new[] { "new", "issue" });

            string message = this.tracker.SearchForIssues(new[] { "new", "issue" });

            var issue = new StringBuilder();
            issue.AppendLine("New issue")
                .AppendLine("Priority: ***")
                .AppendLine("This is a new issue")
                .Append("Tags: issue,new");

            Assert.AreEqual(issue.ToString(), message);
        }

        [TestMethod]
        public void TestSearchWithNoTagsShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");
            this.tracker.CreateIssue("New issue", "This is a new issue", IssuePriority.High, new[] { "new", "issue" });

            string message = this.tracker.SearchForIssues(new string[0]);

            Assert.AreEqual("There are no tags provided", message);
        }

        [TestMethod]
        public void TestSearchWithNonExistingTagsShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");
            this.tracker.CreateIssue("New issue", "This is a new issue", IssuePriority.High, new[] { "new", "issue" });

            string message = this.tracker.SearchForIssues(new[] { "errorrrr", "i`m f***ed" });

            Assert.AreEqual("There are no issues matching the tags provided", message);
        }
    }
}
