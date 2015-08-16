namespace TicketOffice
{
    using System;

    public class TicketOffice
    {
        private static void Main()
        {
            var ticketRepository = new TicketRepository();
            string line = Console.ReadLine();
            while (line != null)
            {
                line = line.Trim();
                string commandResult = ticketRepository.CommandParser(line);
                if (commandResult != null)
                {
                    Console.WriteLine(commandResult);
                }

                line = Console.ReadLine();
            }
        }
    }
}