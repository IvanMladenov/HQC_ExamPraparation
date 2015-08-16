namespace TicketOffice.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AirTicketTests
    {
        private TicketRepository repository;

        [TestInitialize]
        public void TestInitialization()
        {
            this.repository = new TicketRepository();
        }

        [TestMethod]
        public void TestAddAirTicketShouldAddTheTicket()
        {
            string message = this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);

            Assert.AreEqual("Flight created", message);
            Assert.AreEqual(1, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus)); 
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestAddAirTicketShouldHaveTheAddedTicket()
        {
            this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);

            string message = this.repository.FindTicketsByDepartureAndArrival("London", "Paris");

            Assert.AreEqual("[27.01.2015 05:00|flight|150.00]", message);
        }

        [TestMethod]
        public void TestAddAddedAirTicketShouldReturnDuplicatedFlight()
        {
            this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);
            string message = this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);

            Assert.AreEqual("Duplicated flight", message);
            Assert.AreEqual(1, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestDeleteAirTicketShouldDeleteTheTicket()
        {
            this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);
            string message = this.repository.DeleteAirTicket("TX251FB");

            Assert.AreEqual("Flight deleted", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestDeleteNonExistingAirTicketShouldReturnAppropriateMessage()
        {
            this.repository.AddAirTicket(
                "TX251FB",
                "London",
                "Paris",
                "Swiss Air",
                new DateTime(2015, 01, 27, 05, 00, 00),
                150m);
            this.repository.DeleteAirTicket("TX251FB");
            string message = this.repository.DeleteAirTicket("TX251FB");

            Assert.AreEqual("Flight does not exist", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }
    }
}
