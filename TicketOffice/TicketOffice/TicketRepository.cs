namespace TicketOffice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::TicketOffice.Contracts;
    using global::TicketOffice.Tickets;

    using Wintellect.PowerCollections;

    public class TicketRepository : ITicketRepository
    {
        private readonly MultiDictionary<string, Ticket> ticketsByDepartureAndArrival =
            new MultiDictionary<string, Ticket>(true);

        private readonly Dictionary<string, Ticket> ticketsByKey = new Dictionary<string, Ticket>();

        private readonly OrderedMultiDictionary<DateTime, Ticket> ticketsByTimeInterval =
            new OrderedMultiDictionary<DateTime, Ticket>(true);

        private int airTicketsCount;

        private int busTicketsCount;

        private int trainTicketsCount;

        public string CommandParser(string commandLine)
        {
            if (commandLine == string.Empty)
            {
                return null;
            }

            int firstSpaceIndex = commandLine.IndexOf(' ');

            if (firstSpaceIndex == -1)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var parameters = GetParametersFromCommandLine(commandLine, firstSpaceIndex);

            string command = commandLine.Substring(0, firstSpaceIndex);
            string commandResult = "Invalid command!";
            switch (command)
            {
                case "CreateFlight":
                    commandResult = this.AddAirTicket(
                        parameters[0],
                        parameters[1],
                        parameters[2],
                        parameters[3],
                        Ticket.ParseDateTime(parameters[4]),
                        decimal.Parse(parameters[5]));
                    break;
                case "DeleteFlight":
                    commandResult = this.DeleteAirTicket(parameters[0]);
                    break;
                case "CreateTrain":
                    commandResult = this.AddTrainTicket(
                        parameters[0],
                        parameters[1],
                        Ticket.ParseDateTime(parameters[2]),
                        decimal.Parse(parameters[3]),
                        decimal.Parse(parameters[4]));
                    break;
                case "DeleteTrain":
                    commandResult = this.DeleteTrainTicket(
                        parameters[0],
                        parameters[1],
                        Ticket.ParseDateTime(parameters[2]));
                    break;
                case "CreateBus":
                    commandResult = this.AddBusTicket(
                        parameters[0],
                        parameters[1],
                        parameters[2],
                        Ticket.ParseDateTime(parameters[3]),
                        decimal.Parse(parameters[4]));
                    break;
                case "DeleteBus":
                    commandResult = this.DeleteBusTicket(
                        parameters[0],
                        parameters[1],
                        parameters[2],
                        Ticket.ParseDateTime(parameters[3]));
                    break;
                case "FindTickets":
                    commandResult = this.FindTicketsByDepartureAndArrival(parameters[0], parameters[1]);
                    break;
                case "FindByDates":
                    commandResult = this.FindTicketsInInterval(
                        Ticket.ParseDateTime(parameters[0]),
                        Ticket.ParseDateTime(parameters[1]));
                    break;
            }

            return commandResult;
        }

        public string FindTicketsByDepartureAndArrival(string from, string to)
        {
            string fromToKey = Ticket.CreateFromToKey(from, to);

            if (this.ticketsByDepartureAndArrival.ContainsKey(fromToKey))
            {
                var ticketsFound = this.ticketsByDepartureAndArrival[fromToKey];

                string ticketsAsString = this.ReadTickets(ticketsFound);

                return ticketsAsString;
            }

            return "No matches";
        }

        public string FindTicketsInInterval(DateTime startDateTime, DateTime endDateTime)
        {
            var ticketsFound = this.ticketsByTimeInterval.Range(startDateTime, true, endDateTime, true).Values;
            if (ticketsFound.Count > 0)
            {
                string ticketsAsString = this.ReadTickets(ticketsFound);

                return ticketsAsString;
            }

            return "No matches";
        }

        public int GetTicketsCount(TicketType type)
        {
            if (type == TicketType.Flight)
            {
                return this.airTicketsCount;
            }

            if (type == TicketType.Bus)
            {
                return this.busTicketsCount;
            }

            return this.trainTicketsCount;
        }

        public string AddAirTicket(
            string flightNumber,
            string from,
            string to,
            string airline,
            DateTime dateAndTime,
            decimal price)
        {
            AirTicket ticket = new AirTicket(flightNumber, from, to, airline, dateAndTime, price);

            string result = this.AddTicket(ticket);
            if (result.Contains("created"))
            {
                this.airTicketsCount++;
            }

            return result;
        }

        public string DeleteAirTicket(string flightNumber)
        {
            AirTicket ticket = new AirTicket(flightNumber);

            string result = this.DeleteTicket(ticket);
            if (result.Contains("deleted"))
            {
                this.airTicketsCount--;
            }

            return result;
        }

        public string AddTrainTicket(string from, string to, DateTime dateAndTime, decimal price, decimal studentPrice)
        {
            TrainTicket ticket = new TrainTicket(from, to, dateAndTime, price, studentPrice);

            string result = this.AddTicket(ticket);
            if (result.Contains("created"))
            {
                this.trainTicketsCount++;
            }

            return result;
        }

        public string DeleteTrainTicket(string from, string to, DateTime dateAndTime)
        {
            TrainTicket ticket = new TrainTicket(from, to, dateAndTime);
            string result = this.DeleteTicket(ticket);

            if (result.Contains("deleted"))
            {
                this.trainTicketsCount--;
            }

            return result;
        }

        public string AddBusTicket(string from, string to, string company, DateTime dateAndTime, decimal price)
        {
            BusTicket ticket = new BusTicket(from, to, company, dateAndTime, price);

            string result = this.AddTicket(ticket);

            if (result.Contains("created"))
            {
                this.busTicketsCount++;
            }

            return result;
        }

        public string DeleteBusTicket(string from, string to, string company, DateTime dateAndTime)
        {
            BusTicket ticket = new BusTicket(from, to, company, dateAndTime);
            string result = this.DeleteTicket(ticket);

            if (result.Contains("deleted"))
            {
                this.busTicketsCount--;
            }

            return result;
        }

        public string ReadTickets(ICollection<Ticket> tickets)
        {
            List<Ticket> sortedTickets = new List<Ticket>(tickets);

            sortedTickets.Sort();

            string result = string.Join(" ", sortedTickets.Select(t => t.ToString()));

            return result;
        }

        public string AddTicket(Ticket ticket)
        {
            string key = ticket.DataKey;
            if (this.ticketsByKey.ContainsKey(key))
            {
                return "Duplicated " + ticket.Type.ToString().ToLower();
            }

            this.ticketsByKey.Add(key, ticket);
            string fromToKey = ticket.FromToKey;

            this.ticketsByDepartureAndArrival.Add(fromToKey, ticket);
            this.ticketsByTimeInterval.Add(ticket.DateAndTime, ticket);
            return ticket.Type + " created";
        }

        public string DeleteTicket(Ticket ticket)
        {
            string key = ticket.DataKey;
            if (this.ticketsByKey.ContainsKey(key))
            {
                ticket = this.ticketsByKey[key];
                this.ticketsByKey.Remove(key);
                string fromToKey = ticket.FromToKey;

                this.ticketsByDepartureAndArrival.Remove(fromToKey, ticket);
                this.ticketsByTimeInterval.Remove(ticket.DateAndTime, ticket);
                return ticket.Type + " deleted";
            }

            return ticket.Type + " does not exist";
        }

        private static string[] GetParametersFromCommandLine(string commandLine, int firstSpaceIndex)
        {
            string allParameters = commandLine.Substring(firstSpaceIndex + 1);
            string[] parameters = allParameters.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = parameters[i].Trim();
            }

            return parameters;
        }
    }
}