namespace BangaloreUniversityLearningSystem.Contracts
{
    using BangaloreUniversityLearningSystem.Data;
    using BangaloreUniversityLearningSystem.Models;

    public interface IBangaloreUniversityData
    {
        UsersRepository Users { get; }

        CoursesRepository Courses { get; }
    }
}