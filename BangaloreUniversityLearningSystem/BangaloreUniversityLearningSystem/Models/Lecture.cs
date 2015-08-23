namespace BangaloreUniversityLearningSystem.Models
{
    using System;

    public class Lecture
    {
        private string lectureName;

        public Lecture(string lectureName)
        {
            this.LectureName = lectureName;
        }

        public string LectureName
        {
            get
            {
                return this.lectureName;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                {
                    throw new ArgumentException("The lecture name must be at least 3 symbols long.");
                }

                this.lectureName = value;
            }
        }
    }
}
