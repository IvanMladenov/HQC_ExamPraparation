namespace BangaloreUniversityLearningSystem.Models
{
    using System;
    using System.Collections.Generic;

    using BangaloreUniversityLearningSystem.Utilities;

    public class User
    {
        private string password;

        private string username;

        public User(string username, string password, Role role)
        {
            this.Username = username;
            this.Password = password;

            this.PasswordHash = HashUtilities.HashPassword(password);
            this.Role = role;
            this.Courses = new List<Course>();
        }

        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException("The username must be at least 5 symbols long.");
                }

                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 6)
                {
                    throw new ArgumentException("The password must be at least 6 symbols long.");
                }

                this.password = value;
            }
        }

        public string PasswordHash { get; set; }

        public Role Role { get; private set; }

        public IList<Course> Courses { get; private set; }
    }
}