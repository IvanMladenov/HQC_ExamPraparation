namespace VehicleParkSystem.Tests
{
    using System;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VehicleParkSystem.Contracts;
    using VehicleParkSystem.Models;
    using VehicleParkSystem.Vehicles;

    [TestClass]
    public class TestExitVehicleMethod
    {
        [TestMethod]
        public void TestAVehicleExitSuccessShouldReturnAnExitTicket()
        {
            IVehiclePark vehiclePark = new VehiclePark(3, 5);
            var vehicle = new Car("CA1001HH", "Jay Margareta", 1);

            vehiclePark.InsertCar(vehicle, 1, 5, new DateTime(2015, 05, 04, 10, 30, 00));
            string message = vehiclePark.ExitVehicle("CA1001HH", new DateTime(2015, 05, 04, 13, 30, 00), 40m);

            var ticket = new StringBuilder();
            ticket.AppendLine("********************")
                .AppendLine("Car [CA1001HH], owned by Jay Margareta")
                .AppendLine("at place (1,5)")
                .AppendLine("Rate: $2.00")
                .AppendLine("Overtime rate: $7.00")
                .AppendLine("--------------------")
                .AppendLine("Total: $9.00")
                .AppendLine("Paid: $40.00")
                .AppendLine("Change: $31.00")
                .Append("********************");

            Assert.AreEqual(ticket.ToString(), message);
        }

        [TestMethod]
        public void TestAVehicleExitSuccessWithLessThanTheStayingHoursShouldReturnAnExitTicketWithFullHoursPaid()
        {
            IVehiclePark vehiclePark = new VehiclePark(3, 5);
            var vehicle = new Car("CA1001HH", "Jay Margareta", 1);

            vehiclePark.InsertCar(vehicle, 1, 5, new DateTime(2015, 05, 04, 10, 30, 00));
            string message = vehiclePark.ExitVehicle("CA1001HH", new DateTime(2015, 05, 04, 10, 50, 00), 40m);

            var ticket = new StringBuilder();
            ticket.AppendLine("********************")
                .AppendLine("Car [CA1001HH], owned by Jay Margareta")
                .AppendLine("at place (1,5)")
                .AppendLine("Rate: $2.00")
                .AppendLine("Overtime rate: $0.00")
                .AppendLine("--------------------")
                .AppendLine("Total: $2.00")
                .AppendLine("Paid: $40.00")
                .AppendLine("Change: $38.00")
                .Append("********************");

            Assert.AreEqual(ticket.ToString(), message);
        }

        [TestMethod]
        public void TestAVehicleExitWithIncorrectLisenceplateShouldReturnAnErrorMessage()
        {
            IVehiclePark vehiclePark = new VehiclePark(3, 5);
            var vehicle = new Car("CA1001HH", "Jay Margareta", 1);

            vehiclePark.InsertCar(vehicle, 1, 5, new DateTime(2015, 05, 04, 10, 30, 00));
            string message = vehiclePark.ExitVehicle("CA1101HH", new DateTime(2015, 05, 04, 13, 30, 00), 40m);

            Assert.AreEqual("There is no vehicle with license plate CA1101HH in the park", message);
        }
    }
}
