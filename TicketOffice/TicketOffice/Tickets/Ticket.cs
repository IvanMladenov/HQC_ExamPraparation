namespace TicketOffice.Tickets
{
    using System;
    using System.Globalization;

    public abstract class Ticket : IComparable<Ticket>
    {
        protected Ticket(TicketType type, string from, string to, DateTime dateAndTime, decimal price)
        {
            this.Type = type;
            this.From = from;
            this.To = to;
            this.DateAndTime = dateAndTime;
            this.Price = price;
        }

        public TicketType Type { get; private set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime DateAndTime { get; set; }

        public decimal Price { get; set; }

        public abstract string DataKey { get; }

        public string FromToKey
        {
            get
            {
                return CreateFromToKey(this.From, this.To);
            }
        }

        public static string CreateFromToKey(string from, string to)
        {
            return from + "; " + to;
        }

        public static DateTime ParseDateTime(string dt)
        {
            DateTime result = DateTime.ParseExact(dt, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            return result;
        }

        public int CompareTo(Ticket otherTicket)
        {
            int comparisonResult = this.DateAndTime.CompareTo(otherTicket.DateAndTime);
            if (comparisonResult == 0)
            {
                comparisonResult = this.Type.CompareTo(otherTicket.Type);
            }

            if (comparisonResult == 0)
            {
                comparisonResult = this.Price.CompareTo(otherTicket.Price);
            }

            return comparisonResult;
        }

        public override string ToString()
        {
            string output = "[" + this.DateAndTime.ToString("dd.MM.yyyy HH:mm") + "|" + this.Type.ToString().ToLower()
                            + "|" + string.Format("{0:f2}", this.Price) + "]";
            return output;
        }
    }
}