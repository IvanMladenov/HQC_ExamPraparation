namespace BangaloreUniversityLearningSystem.Data
{
    using BangaloreUniversityLearningSystem.Contracts;

    public class BangaloreUniversityData : IBangaloreUniversityData
    {
        public BangaloreUniversityData()
        {
            this.Users = new UsersRepository();
            this.Courses = new CoursesRepository();
        }

        public UsersRepository Users { get; internal set; }

        public CoursesRepository Courses { get; protected set; }
    }
}