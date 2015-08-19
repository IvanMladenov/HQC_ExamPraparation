namespace VehicleParkSystem.Tests
{
    using System;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VehicleParkSystem.Models;
    using VehicleParkSystem.Vehicles;

    [TestClass]
    public class TestFindVehiclesByOwnerMethod
    {
        [TestMethod]
        public void TestFindOneVehicleShouldReturnTheVehicleWithTheOwnerAndTheParkingPlace()
        {
            var vehiclePark = new VehiclePark(2, 3);
            var vehicle = new Car("CA1001HH", "Jay Margareta", 1);
            vehiclePark.InsertCar(vehicle, 1, 2, new DateTime(2015, 05, 04, 10, 30, 00));
            string message = vehiclePark.FindVehiclesByOwner("Jay Margareta");

            var result = new StringBuilder();
            result.AppendLine("Car [CA1001HH], owned by Jay Margareta").Append("Parked at (1,2)");

            Assert.AreEqual(result.ToString(), message);
        }

        [TestMethod]
        public void TestFindVehicleWithNonExistingOwnerShouldReturnErrorMessage()
        {
            var vehiclePark = new VehiclePark(2, 3);
            var vehicle = new Car("CA1001HH", "Jay Margareta", 1);
            vehiclePark.InsertCar(vehicle, 1, 2, new DateTime(2015, 05, 04, 10, 30, 00));
            string message = vehiclePark.FindVehiclesByOwner("Margarata");

            Assert.AreEqual("No vehicles by Margarata", message);
        }
    }
}
