namespace TicketOffice.Tickets
{
    using System;

    public class TrainTicket : Ticket
    {
        public TrainTicket(string from, string to, DateTime dateAndTime, decimal price, decimal studentPrice) 
            : base(TicketType.Train, from, to, dateAndTime, price)
        {
            this.StudentPrice = studentPrice;
        }

        public TrainTicket(string from, string to, DateTime dateAndTime) 
            : this(from, to, dateAndTime, 0m, 0m)
        {
        }

        public decimal StudentPrice { get; set; }

        public override string DataKey
        {
            get
            {
                return this.Type + ";;" + this.From + ";" + this.To + ";" + this.DateAndTime + ";";
            }
        }
    }
}
