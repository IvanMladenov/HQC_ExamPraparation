namespace PhonebookSystem
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PhonebookEntry : IComparable<PhonebookEntry>
    {
        private string name;

        private string name2;

        public SortedSet<string> EntryPhoneNumbers { get; set; }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;

                this.name2 = value.ToLowerInvariant();
            }
        }

        public int CompareTo(PhonebookEntry other)
        {
            return string.Compare(this.name2, other.name2, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return string.Format("[{0}: {1}]", this.Name, string.Join(", ", this.EntryPhoneNumbers));
        }
    }
}
