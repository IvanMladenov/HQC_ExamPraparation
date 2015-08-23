namespace BangaloreUniversityLearningSystem.Data
{
    using System;
    using System.Collections.Generic;

    using BangaloreUniversityLearningSystem.Contracts;

    public class Repository<T> : IRepository<T>
    {
        public Repository()
        {
            this.Items = new List<T>();
        }

        public List<T> Items { get; set; }

        public virtual IEnumerable<T> GetAll()
        {
            return this.Items;
        }

        public virtual T Get(int id)
        {
            T item;
            try
            {
                item = this.Items[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                item = default(T);
            }

            return item;
        }

        public virtual void Add(T item)
        {
            this.Items.Add(item);
        }
    }
}