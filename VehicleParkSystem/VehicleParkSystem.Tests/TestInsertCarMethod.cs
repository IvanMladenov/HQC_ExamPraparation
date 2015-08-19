namespace VehicleParkSystem.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VehicleParkSystem.Contracts;
    using VehicleParkSystem.Models;
    using VehicleParkSystem.Vehicles;

    [TestClass]
    public class TestInsertCarMethod
    {
        [TestMethod]
        public void TestInsertOneLegitimateCarShouldReturnASuccessMessage()
        {
            IVehiclePark vehiclePark = new VehiclePark(3, 5);
            var vehicle = new Car("CA1001HH", "Jay Margareta", 1);

            string message = vehiclePark.InsertCar(vehicle, 1, 5, new DateTime(2015, 05, 04, 10, 30, 00));

            Assert.AreEqual("Car parked successfully at place (1,5)", message);
        }

        [TestMethod]
        public void TestInsertCarAtNonExistingSectorShouldReturnErrorMessage()
        {
            IVehiclePark vehiclePark = new VehiclePark(3, 5);
            var vehicle = new Car("CA1001HH", "Jay Margareta", 1);

            string message = vehiclePark.InsertCar(vehicle, 30, 5, new DateTime(2015, 05, 04, 10, 30, 00));

            Assert.AreEqual("There is no sector 30 in the park", message);
        }

        [TestMethod]
        public void TestInsertCarAtNonExistingPlaceInSectorShouldReturnErrorMessage()
        {
            IVehiclePark vehiclePark = new VehiclePark(3, 5);
            var vehicle = new Car("CA1001HH", "Jay Margareta", 1);

            string message = vehiclePark.InsertCar(vehicle, 1, 30, new DateTime(2015, 05, 04, 10, 30, 00));

            Assert.AreEqual("There is no place 30 in sector 1", message);
        }

        [TestMethod]
        public void TestInsertCarAtAlreadyOccupiedPlaceInSectorShouldReturnErrorMessage()
        {
            IVehiclePark vehiclePark = new VehiclePark(3, 5);
            var firstVehicle = new Car("CA1001HH", "Jay Margareta", 1);
            var secondVehicle = new Car("CA1111HH", "Guy Sheard", 2);

            vehiclePark.InsertCar(firstVehicle, 1, 5, new DateTime(2015, 05, 04, 10, 30, 00));
            string message = vehiclePark.InsertCar(secondVehicle, 1, 5, new DateTime(2015, 05, 04, 10, 30, 00));

            Assert.AreEqual("The place (1,5) is occupied", message);
        }

        [TestMethod]
        public void TestInsertCarWithSameLisencePlateShouldReturnErrorMessage()
        {
            IVehiclePark vehiclePark = new VehiclePark(3, 5);
            var firstVehicle = new Car("CA1001HH", "Jay Margareta", 1);
            var secondVehicle = new Car("CA1001HH", "Guy Sheard", 2);

            vehiclePark.InsertCar(firstVehicle, 1, 5, new DateTime(2015, 05, 04, 10, 30, 00));
            string message = vehiclePark.InsertCar(secondVehicle, 1, 3, new DateTime(2015, 05, 04, 10, 30, 00));

            Assert.AreEqual("There is already a vehicle with license plate CA1001HH in the park", message);
        }
    }
}