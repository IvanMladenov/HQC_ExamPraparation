namespace BangaloreUniversityLearningSystem.Data
{
    using System.Linq;

    using BangaloreUniversityLearningSystem.Models;

    public class UsersRepository : Repository<User>
    {
        public User GetByUsername(string username)
        {
            return this.Items.FirstOrDefault(u => u.Username == username);
        }
    }
}