namespace TicketOffice.Tickets
{
    using System;

    public class BusTicket : Ticket
    {
        public BusTicket(string from, string to, string company, DateTime dateAndTime, decimal price) 
            : base(TicketType.Bus, from, to, dateAndTime, price)
        {
            this.Company = company;
        }

        public BusTicket(string from, string to, string company, DateTime dateAndTime) 
            : this(from, to, company, dateAndTime, 0m)
        {
        }

        public string Company { get; set; }

        public override string DataKey
        {
            get
            {
                return this.Type + ";;" + this.From + ";" + this.To + ";" + this.Company + this.DateAndTime + ";";
            }
        }
    }
}
