using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BangaloreUniversityLearningSystem.Tests
{
    using BangaloreUniversityLearningSystem.Contracts;
    using BangaloreUniversityLearningSystem.Controllers;
    using BangaloreUniversityLearningSystem.Data;
    using BangaloreUniversityLearningSystem.Models;

    [TestClass]
    public class TestLogoutMethod
    {
        [TestMethod]
        public void TestSuccessfullLogoutShouldReturnAppropriateMessage()
        {
            IBangaloreUniversityData data = new BangaloreUniversityData();
            User user = new User("firstLecturer", "firstPass", Role.Lecturer);
            UsersController controller = new UsersController(data, user);

            string message = controller.Logout().Display();

            Assert.AreEqual("User firstLecturer logged out successfully.", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLogoutIfNoUserIsLoggedInShouldReturnAppropriateMessage()
        {
            IBangaloreUniversityData data = new BangaloreUniversityData();
            User user = new User("firstLecturer", "firstPass", Role.Lecturer);
            UsersController controller = new UsersController(data, null);

            controller.Logout().Display();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLogoutWithWrongUserRoleShouldReturnAppropriateMessage()
        {
            IBangaloreUniversityData data = new BangaloreUniversityData();
            User user = new User("firstLecturer", "firstPass", Role.Guest);
            UsersController controller = new UsersController(data, user);

            controller.Logout().Display();
        }
    }
}
