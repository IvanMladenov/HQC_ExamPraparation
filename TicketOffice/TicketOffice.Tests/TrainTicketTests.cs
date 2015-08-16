namespace TicketOffice.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TrainTicketTests
    {
        private TicketRepository repository;

        [TestInitialize]
        public void TestInitialization()
        {
            this.repository = new TicketRepository();
        }

        [TestMethod]
        public void TestAddTrainTicketShouldAddTheTicket()
        {
            string message = this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);

            Assert.AreEqual("Train created", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(1, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestAddTrainTicketShouldHaveTheAddedTicket()
        {
            this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);

            string message = this.repository.FindTicketsByDepartureAndArrival("Sofia", "Varna");

            Assert.AreEqual("[17.01.2015 11:25|train|6.50]", message);
        }

        [TestMethod]
        public void TestAddAddedTrainTicketShouldReturnDuplicatedTrain()
        {
            this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);
            string message = this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);

            Assert.AreEqual("Duplicated train", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(1, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestDeleteTrainTicketShouldDeleteTheTicket()
        {
            this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);
            string message = this.repository.DeleteTrainTicket("Sofia", "Varna", new DateTime(2015, 01, 17, 11, 25, 00));

            Assert.AreEqual("Train deleted", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }

        [TestMethod]
        public void TestDeleteNonExistingTrainTicketShouldReturnAppropriateMessage()
        {
            this.repository.AddTrainTicket(
                "Sofia",
                "Varna",
                new DateTime(2015, 01, 17, 11, 25, 00),
                6.5m,
                2.4m);
            this.repository.DeleteTrainTicket("Sofia", "Varna", new DateTime(2015, 01, 17, 11, 25, 00));
            string message = this.repository.DeleteTrainTicket("Sofia", "Varna", new DateTime(2015, 01, 17, 11, 25, 00));

            Assert.AreEqual("Train does not exist", message);
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Flight));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Bus));
            Assert.AreEqual(0, this.repository.GetTicketsCount(TicketType.Train));
        }
    }
}


