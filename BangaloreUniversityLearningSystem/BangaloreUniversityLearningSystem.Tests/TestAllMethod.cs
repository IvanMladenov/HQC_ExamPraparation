namespace BangaloreUniversityLearningSystem.Tests
{
    using System.Linq;
    using System.Text;

    using BangaloreUniversityLearningSystem.Contracts;
    using BangaloreUniversityLearningSystem.Controllers;
    using BangaloreUniversityLearningSystem.Data;
    using BangaloreUniversityLearningSystem.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestAllMethod
    {
        [TestMethod]
        public void TestWithNoCourses()
        {
            IBangaloreUniversityData data = new BangaloreUniversityData();
            User user = new User("firstLecturer", "firstPass", Role.Guest);
            CoursesController controller = new CoursesController(data, user);

            string message = controller.All().Display();

            Assert.AreEqual("No courses.", message);
        }

        [TestMethod]
        public void TestWithOneCourseAndNoStudents()
        {
            IBangaloreUniversityData data = new BangaloreUniversityData();
            User user = new User("firstLecturer", "firstPass", Role.Lecturer);
            CoursesController controller = new CoursesController(data, user);

            controller.Create("Object-Oriented+Programming");
            string message = controller.All().Display();

            var courses = new StringBuilder();
            courses.AppendLine("All courses:");
            foreach (var course in data.Courses.Items)
            {
                courses.AppendFormat("{0} ({1} students)", course.CourseName, course.Students.Count);
            }

            Assert.AreEqual(courses.ToString(), message);
        }

        [TestMethod]
        public void TestWithSeveralCoursesAndDifferentStudents()
        {
            IBangaloreUniversityData data = new BangaloreUniversityData();
            User user = new User("firstLecturer", "firstPass", Role.Lecturer);
            CoursesController controller = new CoursesController(data, user);

            controller.Create("Object-Oriented+Programming");
            controller.Create("High-Quality+Code");
            controller.Create("Java+Basics");
            controller.Enroll(1);
            controller.Enroll(3);
            string message = controller.All().Display();

            var courses = new StringBuilder();
            courses.AppendLine("All courses:");
            foreach (var course in data.Courses.Items.OrderBy(c => c.CourseName).ThenByDescending(c => c.Students.Count))
            {
                courses.AppendFormat("{0} ({1} students)", course.CourseName, course.Students.Count).AppendLine();
            }

            Assert.AreEqual(courses.ToString().Trim(), message);
        }
    }
}
