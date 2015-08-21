namespace BuhtigIssueTracker.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestRegisterUserMethod
    {
        private IssueTracker tracker;

        [TestInitialize]
        public void TestInitialization()
        {
            this.tracker = new IssueTracker();
        }

        [TestMethod]
        public void TestRegisterAUserSuccessfullyShouldReturnAppropriateMessage()
        {
            string message = this.tracker.RegisterUser("admin", "pass123", "pass123");

            Assert.AreEqual("User admin registered successfully", message);
        }

        [TestMethod]
        public void TestRegisterUserIfThereIsAUserAlreadyLoggedInShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            this.tracker.LoginUser("admin", "pass123");
            string message = this.tracker.RegisterUser("user", "user123", "user123");

            Assert.AreEqual("There is already a logged in user", message);
        }

        [TestMethod]
        public void TestRegisterUserWithNonMatchingPasswordsShouldReturnAnErrorMessage()
        {
            string message = this.tracker.RegisterUser("user", "user123", "user1234");

            Assert.AreEqual("The provided passwords do not match", message);
        }

        [TestMethod]
        public void TestRegisterUserWithAlreadyExistingUsernameShouldReturnAnErrorMessage()
        {
            this.tracker.RegisterUser("admin", "pass123", "pass123");
            string message = this.tracker.RegisterUser("admin", "user123", "user123");

            Assert.AreEqual("A user with username admin already exists", message);
        }
    }
}