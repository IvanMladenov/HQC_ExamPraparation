namespace TicketOffice.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AdditionalTicketRepositoryTests
    {
        private TicketRepository repository;

        [TestInitialize]
        public void TestInitialization()
        {
            this.repository = new TicketRepository();
        }

        [TestMethod]
        public void TestSearchTicketsByDepartureAndArrivalShouldReturnFoundTicketsSignature()
        {
            this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);
            this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);
            this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);
            string messageLondonParis = this.repository.FindTicketsByDepartureAndArrival("London", "Paris");
            string messageSofiaVarna = this.repository.FindTicketsByDepartureAndArrival("Sofia", "Varna");

            Assert.AreEqual("[27.01.2015 05:00|flight|150.00]", messageLondonParis);
            Assert.AreEqual("[15.01.2015 06:15|bus|25.00] [17.01.2015 11:25|train|6.50]", messageSofiaVarna);
        }

        [TestMethod]
        public void TestSearchTicketsByNonExistingDepartureAndArrivalShouldReturnAppropriateMessage()
        {
            this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);
            this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);
            this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);
            string message = this.repository.FindTicketsByDepartureAndArrival("Varna", "Sofia");

            Assert.AreEqual("No matches", message);
        }

        [TestMethod]
        public void TestSearchTicketsByTimeAndDateShouldReturnFoundTicketsSignature()
        {
            this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);
            this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);
            this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);
            string messageLondonParis = this.repository.FindTicketsInInterval(
                new DateTime(2015, 01, 26, 11, 25, 00),
                new DateTime(2015, 01, 28, 11, 25, 00));
            string messageSofiaVarna = this.repository.FindTicketsInInterval(
                new DateTime(2015, 01, 14, 11, 25, 00),
                new DateTime(2015, 01, 18, 11, 25, 00));

            Assert.AreEqual("[27.01.2015 05:00|flight|150.00]", messageLondonParis);
            Assert.AreEqual("[15.01.2015 06:15|bus|25.00] [17.01.2015 11:25|train|6.50]", messageSofiaVarna);
        }

        [TestMethod]
        public void TestSearchForNonExistingTicketsByTimeAndDateShouldReturnApprpriateMessage()
        {
            this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);
            this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);
            this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);
            string message = this.repository.FindTicketsInInterval(
                new DateTime(2015, 03, 26, 11, 25, 00),
                new DateTime(2015, 04, 28, 11, 25, 00));

            Assert.AreEqual("No matches", message);
        }
    }
}
