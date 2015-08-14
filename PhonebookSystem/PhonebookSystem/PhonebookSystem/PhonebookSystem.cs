namespace PhonebookSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using global::PhonebookSystem.Contracts;

    public class PhonebookSystem
    {
        private const string AreaCode = "+359";

        private const string AddPhone = "AddPhone";

        private const string ChangePhone = "ChangePhone";

        private const string List = "List";

        private static readonly IPhonebookRepository PhonebookRepository = new PhonebookRepository();

        private static readonly StringBuilder Output = new StringBuilder();

        private static void Main()
        {
            string data = Console.ReadLine();
            while (data != "End" && data != null)
            {
                int indexOfOpeningBracket = data.IndexOf('(');
                string commandArgsSubstring = data.Substring(
                    indexOfOpeningBracket + 1, 
                    data.Length - indexOfOpeningBracket - 2);
                string[] commandArgs = commandArgsSubstring.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                string command = data.Substring(0, indexOfOpeningBracket);
                if (command.StartsWith(AddPhone) && (commandArgs.Length >= 2))
                {
                    ProcessCommands(AddPhone, commandArgs);
                }
                else if ((command == ChangePhone) && (commandArgs.Length == 2))
                {
                    ProcessCommands(ChangePhone, commandArgs);
                }
                else if ((command == List) && (commandArgs.Length == 2))
                {
                    ProcessCommands(List, commandArgs);
                }
                else
                {
                    throw new StackOverflowException();
                }

                data = Console.ReadLine();
            }

            Console.Write(Output);
        }

        private static void ProcessCommands(string command, string[] commandArgs)
        {
            switch (command)
            {
                case AddPhone:
                    string name = commandArgs[0];
                    var phoneNumbers = commandArgs.Skip(1).ToList();
                    for (int i = 0; i < phoneNumbers.Count; i++)
                    {
                        phoneNumbers[i] = ConvertToCanonicalForm(phoneNumbers[i]);
                    }

                    bool isNewEntry = PhonebookRepository.AddPhone(name, phoneNumbers);

                    Print(isNewEntry ? "Phone entry created" : "Phone entry merged");
                    break;
                case ChangePhone:
                    Print(
                        string.Empty
                        + PhonebookRepository.ChangePhone(
                            ConvertToCanonicalForm(commandArgs[0]), 
                            ConvertToCanonicalForm(commandArgs[1])) + " numbers changed");
                    break;
                default:
                    try
                    {
                        IEnumerable<PhonebookEntry> entries = PhonebookRepository.ListEntries(
                            int.Parse(commandArgs[0]), 
                            int.Parse(commandArgs[1]));
                        foreach (var entry in entries)
                        {
                            Print(entry.ToString());
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Print("Invalid range");
                    }

                    break;
            }
        }

        private static string ConvertToCanonicalForm(string phoneNumber)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char ch in phoneNumber)
            {
                if (char.IsDigit(ch) || (ch == '+'))
                {
                    sb.Append(ch);
                }
            }

            if (sb.Length >= 2 && sb[0] == '0' && sb[1] == '0')
            {
                sb.Remove(0, 1);
                sb[0] = '+';
            }

            while (sb.Length > 0 && sb[0] == '0')
            {
                sb.Remove(0, 1);
            }

            if (sb.Length > 0 && sb[0] != '+')
            {
                sb.Insert(0, AreaCode);
            }

            return sb.ToString();
        }

        private static void Print(string text)
        {
            Output.AppendLine(text);
        }
    }

    // public class PhonebookEntry : IComparable<PhonebookEntry>
    // {
    // private string name;

    // private string name2;

    // public SortedSet<string> EntryPhoneNumbers { get; set; }

    // public string Name
    // {
    // get
    // {
    // return this.name;
    // }

    // set
    // {
    // this.name = value;

    // this.name2 = value.ToLowerInvariant();
    // }
    // }

    // public int CompareTo(PhonebookEntry other)
    // {
    // return this.name2.CompareTo(other.name2);
    // }

    // public override string ToString()
    // {
    // StringBuilder sb = new StringBuilder();
    // sb.Clear();
    // sb.Append('[');

    // sb.Append(this.Name);
    // bool flag = true;
    // foreach (var phone in this.EntryPhoneNumbers)
    // {
    // if (flag)
    // {
    // sb.Append(": ");
    // flag = false;
    // }
    // else
    // {
    // sb.Append(", ");
    // }

    // sb.Append(phone);
    // }

    // sb.Append(']');
    // return sb.ToString();
    // }
    // }

    // internal class SecondPhonebookRepository : IPhonebookRepository
    // {
    // private readonly List<PhonebookEntry> phonebookEntries = new List<PhonebookEntry>();

    // public bool AddPhone(string name, IEnumerable<string> nums)
    // {
    // var old = from e in this.phonebookEntries where e.Name.ToLowerInvariant() == name.ToLowerInvariant() select e;

    // bool flag;
    // if (old.Count() == 0)
    // {
    // PhonebookEntry obj = new PhonebookEntry();
    // obj.Name = name;
    // obj.EntryPhoneNumbers = new SortedSet<string>();

    // foreach (var num in nums)
    // {
    // obj.EntryPhoneNumbers.Add(num);
    // }

    // this.phonebookEntries.Add(obj);

    // flag = true;
    // }
    // else if (old.Count() == 1)
    // {
    // PhonebookEntry obj2 = old.First();
    // foreach (var num

    // in nums)
    // {
    // obj2.EntryPhoneNumbers.Add(num);
    // }

    // flag = false;
    // }
    // else
    // {
    // Console.WriteLine("Duplicated name in the phonebook found: " + name);
    // return false;
    // }

    // return flag;
    // }

    // public int ChangePhone(string oldNumber, string newNumber)
    // {
    // var list = from e in this.phonebookEntries where e.EntryPhoneNumbers.Contains(oldNumber) select e;

    // int nums = 0;
    // foreach (var entry in list)
    // {
    // entry.EntryPhoneNumbers.Remove(oldNumber);
    // entry.EntryPhoneNumbers.Add(newNumber);
    // nums++;
    // }

    // return nums;
    // }

    // public PhonebookEntry[] ListEntries(int start, int num)
    // {
    // if (start < 0 || start + num > this.phonebookEntries.Count)
    // {
    // throw new ArgumentOutOfRangeException("Invalid start index or count.");
    // }

    // this.phonebookEntries.Sort();
    // PhonebookEntry[] ent = new PhonebookEntry[num];
    // for (int i = start; i <= start + num - 1; i++)
    // {
    // PhonebookEntry entry = this.phonebookEntries[i];
    // ent[i - start] = entry;
    // }

    // return ent;
    // }
    // }

    // public class PhonebookRepository : IPhonebookRepository
    // {
    // private readonly Dictionary<string, PhonebookEntry> dict = new Dictionary<string, PhonebookEntry>();

    // private readonly MultiDictionary<string, PhonebookEntry> multidict = new MultiDictionary<string, PhonebookEntry>(false);

    // private readonly OrderedSet<PhonebookEntry> sorted = new OrderedSet<PhonebookEntry>();

    // public bool AddPhone(string name, IEnumerable<string> nums)
    // {
    // string name2 = name.ToLowerInvariant();
    // PhonebookEntry entry;
    // bool flag = !this.dict.TryGetValue(name2, out entry);
    // if (flag)
    // {
    // entry = new PhonebookEntry();
    // entry.Name = name;
    // entry.EntryPhoneNumbers = new SortedSet<string>();
    // this.dict.Add(name2, entry);

    // this.sorted.Add(entry);
    // }

    // foreach (var num in nums)
    // {
    // this.multidict.Add(num, entry);
    // }

    // entry.EntryPhoneNumbers.UnionWith(nums);
    // return flag;
    // }

    // public int ChangePhone(string oldNumber, string newNumber)
    // {
    // var found = this.multidict[oldNumber].ToList();
    // foreach (var entry in found)
    // {
    // entry.EntryPhoneNumbers.Remove(oldNumber);
    // this.multidict.Remove(oldNumber, entry);

    // entry.EntryPhoneNumbers.Add(newNumber);
    // this.multidict.Add(newNumber, entry);
    // }

    // return found.Count;
    // }

    // public PhonebookEntry[] ListEntries(int first, int num)
    // {
    // if (first < 0 || first + num > this.dict.Count)
    // {
    // Console.WriteLine("Invalid start index or count.");
    // return null;
    // }

    // PhonebookEntry[] list = new PhonebookEntry[num];

    // for (int i = first; i <= first + num - 1; i++)
    // {
    // PhonebookEntry entry = this.sorted[i];
    // list[i - first] = entry;
    // }

    // return list;
    // }
    // }
}