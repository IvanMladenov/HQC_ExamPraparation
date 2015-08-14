namespace PhonebookSystem.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for the PhonebookRepositoryClass. Contains methods for adding entries to the 
    /// phonebook, changing phone numbers and listing entries.
    /// </summary>
    public interface IPhonebookRepository
    {
        /// <summary>
        /// Adds entries to the phonebook along with their adjacent phone numbers.
        /// </summary>
        /// <param name="name">The new entry name.</param>
        /// <param name="phoneNumbers">The new entry adjacent phone numbers</param>
        /// <returns>'true' if the name doesn`t exist in the phonebook and 
        /// 'false' otherwise</returns>
        bool AddPhone(string name, IEnumerable<string> phoneNumbers);

        /// <summary>
        /// Replaces an existing phone number with a new one.
        /// </summary>
        /// <param name="oldPhoneNumber">The existing phone number</param>
        /// <param name="newPhoneNumber">The new phone number</param>
        /// <returns>The count of phone numbers replaced</returns>
        int ChangePhone(string oldPhoneNumber, string newPhoneNumber);

        /// <summary>
        /// Lists the phonebook entries with paging.
        /// </summary>
        /// <param name="startIndex">Specifies the initial page of the phonebook to be listed</param>
        /// <param name="count">Specifies the number of phonebook entries to be retrieved</param>
        /// <returns>A list of phonebook entries</returns>
        PhonebookEntry[] ListEntries(int startIndex, int count);
    }
}
