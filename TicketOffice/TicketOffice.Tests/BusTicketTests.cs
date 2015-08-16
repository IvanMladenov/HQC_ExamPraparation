namespace TicketOffice.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BusTicketTests
    {
        private TicketRepository repository;

        [TestInitialize]
        public void TestInitialization()
        {
            this.repository = new TicketRepository();
        }

        [TestMethod]
        public void TestAddBusTicketShouldAddTheTicket()
        {
            string message = this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);

            Assert.AreEqual("Bus created", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(1, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestAddBusTicketShouldHaveTheAddedTicket()
        {
            this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);

            string message = this.repository.FindTicketsByDepartureAndArrival("Sofia", "Varna");

            Assert.AreEqual("[15.01.2015 06:15|bus|25.00]", message);
        }

        [TestMethod]
        public void TestAddAddedBusTicketShouldReturnDuplicatedBus()
        {
            this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);
            string message = this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);

            Assert.AreEqual("Duplicated bus", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(1, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestDeleteBusTicketShouldDeleteTheTicket()
        {
            this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);
            string message = this.repository.DeleteBusTicket("Sofia", "Varna", "Etap Address", new DateTime(2015, 01, 15, 06, 15, 00));

            Assert.AreEqual("Bus deleted", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestDeleteNonExistingBusTicketShouldReturnAppropriateMessage()
        {
            this.repository.AddBusTicket(
                "Sofia",
                "Varna",
                "Etap Address",
                new DateTime(2015, 01, 15, 06, 15, 00),
                25m);
            this.repository.DeleteBusTicket("Sofia", "Varna", "Etap Address", new DateTime(2015, 01, 15, 06, 15, 00));
            string message = this.repository.DeleteBusTicket("Sofia", "Varna", "Etap Address", new DateTime(2015, 01, 15, 06, 15, 00));

            Assert.AreEqual("Bus does not exist", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }
    }
}

