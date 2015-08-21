namespace BuhtigIssueTracker.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestCreateIssueMethod
    {
        private IssueTracker tracker;

        [TestInitialize]
        public void TestInitialization()
        {
            this.tracker = new IssueTracker();
        }

        [TestMethod]
        public void TestCreateIssueSuccessfullyShouldReturnAppropriateMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");
            string message = 
                this.tracker.CreateIssue("New issue", "This is a new issue", IssuePriority.Medium, new[] { "new", "issue" });

            Assert.AreEqual("Issue 1 created successfully", message);
        }

        [TestMethod]
        public void TestCreateIssueWithNonLoggedUserShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");

            string message =
                this.tracker.CreateIssue("New issue", "This is a new issue", IssuePriority.Medium, new[] { "new", "issue" });

            Assert.AreEqual("There is no currently logged in user", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The title must be at least 3 symbols long")]
        public void TestCreateIssueWithNonValidTitleShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");

            this.tracker.CreateIssue(string.Empty, "This is a new issue", IssuePriority.Medium, new[] { "new", "issue" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The description must be at least 5 symbols long")]
        public void TestCreateIssueWithNonValidDescriptionShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");

            this.tracker.CreateIssue("New issue", string.Empty, IssuePriority.Medium, new[] { "new", "issue" });
        }
    }
}
