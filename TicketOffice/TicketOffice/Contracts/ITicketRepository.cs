namespace TicketOffice.Contracts
{
    using System;

    /// <summary>
    /// Interface for the TicketRepository class. Contains methods for adding, deleting and searching tickets in the database.
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// Adds a ticket of type Flight to the database.
        /// </summary>
        /// <param name="flightNumber">The flight number of the ticket</param>
        /// <param name="from">The departure destination of the ticket</param>
        /// <param name="to">The arrival destination of the ticket</param>
        /// <param name="airline">The airline of the ticket</param>
        /// <param name="dateTime">The departure time and date of the ticket</param>
        /// <param name="price">The price of the ticket</param>
        /// <returns>Returns a message 'created' if the ticket doesnt already exists in the database and 
        /// 'duplicated' otherwise</returns>
        string AddAirTicket(
            string flightNumber,
            string from,
            string to,
            string airline,
            DateTime dateTime,
            decimal price);

        string DeleteAirTicket(string flightNumber);

        string AddTrainTicket(string from, string to, DateTime dateTime, decimal price, decimal studentPrice);

        string DeleteTrainTicket(string from, string to, DateTime dateTime);

        string AddBusTicket(string from, string to, string travelCompany, DateTime dateTime, decimal price);

        /// <summary>
        /// Removes a ticket of type Bus from the database.
        /// </summary>
        /// <param name="from">The departure destination of the ticket</param>
        /// <param name="to">The arrival destination of the ticket</param>
        /// <param name="travelCompany">The travel company of the ticket</param>
        /// <param name="dateTime">The departure time and date of the ticket</param>
        /// <returns>Returns a message 'deleted' if the ticket exists in the database and 'does not exist' otherwise</returns>
        string DeleteBusTicket(string from, string to, string travelCompany, DateTime dateTime);

        /// <summary>
        /// Searches for tickets in the database by given departure and arrival destinations
        /// </summary>
        /// <param name="from">The departure destination of the ticket</param>
        /// <param name="to">The arrival destination of the ticket</param>
        /// <returns>Returns a message containing all found tickets and a 'No matches' message if no tickets are found</returns>
        string FindTicketsByDepartureAndArrival(string from, string to);

        /// <summary>
        /// Searches for tickets in the database by given time interval
        /// </summary>
        /// <param name="startDateTime">The staring time and date</param>
        /// <param name="endDateTime">The ending time and date</param>
        /// <returns>Returns a message containing all found tickets and a 'No matches' message if no tickets are found</returns>
        string FindTicketsInInterval(DateTime startDateTime, DateTime endDateTime);

        int GetTicketsCount(TicketType type);
    }
}
