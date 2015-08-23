namespace BangaloreUniversityLearningSystem.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BangaloreUniversityLearningSystem.Models;

    public class CoursesRepository : Repository<Course>
    {
        public override IEnumerable<Course> GetAll()
        {
            return this.Items.OrderBy(c => c.CourseName).ThenByDescending(c => c.Students.Count);
        }
    }
}
