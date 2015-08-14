namespace PhonebookSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::PhonebookSystem.Contracts;

    using Wintellect.PowerCollections;

    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly Dictionary<string, PhonebookEntry> nameEntries = new Dictionary<string, PhonebookEntry>();

        private readonly MultiDictionary<string, PhonebookEntry> phoneEntries =
            new MultiDictionary<string, PhonebookEntry>(false);

        private readonly OrderedSet<PhonebookEntry> sortedEntries = new OrderedSet<PhonebookEntry>();

        public int NamesCount
        {
            get
            {
                return this.nameEntries.Count;
            }
        }

        public int PhonesCount
        {
            get
            {
                return this.phoneEntries.Count;
            }
        }

        public bool AddPhone(string name, IEnumerable<string> nums)
        {
            string nameToLower = name.ToLowerInvariant();
            PhonebookEntry entry;
            bool isNew = !this.nameEntries.TryGetValue(nameToLower, out entry);
            if (isNew)
            {
                entry = new PhonebookEntry { Name = name, EntryPhoneNumbers = new SortedSet<string>() };
                this.nameEntries.Add(nameToLower, entry);

                this.sortedEntries.Add(entry);
            }

            foreach (var num in nums)
            {
                this.phoneEntries.Add(num, entry);
            }

            entry.EntryPhoneNumbers.UnionWith(nums);
            return isNew;
        }

        public int ChangePhone(string oldNumber, string newNumber)
        {
            var found = this.phoneEntries[oldNumber].ToList();
            foreach (var entry in found)
            {
                entry.EntryPhoneNumbers.Remove(oldNumber);
                this.phoneEntries.Remove(oldNumber, entry);

                entry.EntryPhoneNumbers.Add(newNumber);
                this.phoneEntries.Add(newNumber, entry);
            }

            return found.Count;
        }

        public PhonebookEntry[] ListEntries(int first, int num)
        {
            if (first < 0 || first + num > this.nameEntries.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid start index or count.");
            }

            PhonebookEntry[] list = new PhonebookEntry[num];

            for (int i = first; i <= first + num - 1; i++)
            {
                PhonebookEntry entry = this.sortedEntries[i];
                list[i - first] = entry;
            }

            return list;
        }
    }
}