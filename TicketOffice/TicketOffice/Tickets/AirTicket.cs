namespace TicketOffice.Tickets
{
    using System;

    public class AirTicket : Ticket
    {
        public AirTicket(string flightNumber, string from, string to, string airline, DateTime dateAndTime, decimal price) 
            : base(TicketType.Flight, from, to, dateAndTime, price)
        {
            this.FlightNumber = flightNumber;
            this.Airline = airline;
        }

        public AirTicket(string flightNumber) 
            : this(flightNumber, null, null, null, default(DateTime), 0m)
        {
        }

        public string FlightNumber { get; set; }

        public string Airline { get; set; }

        public override string DataKey
        {
            get
            {
                return this.Type + ";;" + this.FlightNumber;
            }
        }
    }
}
