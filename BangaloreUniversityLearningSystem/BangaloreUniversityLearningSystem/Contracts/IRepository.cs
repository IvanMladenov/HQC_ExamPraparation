namespace BangaloreUniversityLearningSystem.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains methods for getting all the courses in the repository, getting a concrete course
    /// by its ID and adding courses
    /// </summary>
    /// <typeparam name="T">Type of the item for performing the methods` operations</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Gets all the courses in the repository
        /// </summary>
        /// <returns>Returns the courses in appropriate order and "No courses." message if there are no courses</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets a concrete course by its ID
        /// </summary>
        /// <param name="id">The ID of the course</param>
        /// <returns>Returns the course in appropriate format</returns>
        T Get(int id);

        /// <summary>
        /// Adds a course to the repository courses
        /// </summary>
        /// <param name="item">The course to be added</param>
        void Add(T item);
    }
}
