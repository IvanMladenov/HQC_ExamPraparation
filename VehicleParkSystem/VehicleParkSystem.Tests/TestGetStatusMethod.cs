namespace VehicleParkSystem.Tests
{
    using System;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VehicleParkSystem.Contracts;
    using VehicleParkSystem.Models;
    using VehicleParkSystem.Vehicles;

    [TestClass]
    public class TestGetStatusMethod
    {
        [TestMethod]
        public void TestEmptyPark()
        {
            IVehiclePark vehiclePark = new VehiclePark(2, 4);
            string message = vehiclePark.GetStatus();

            var result = new StringBuilder();
            result.AppendLine("Sector 1: 0 / 4 (0% full)").Append("Sector 2: 0 / 4 (0% full)");

            Assert.AreEqual(result.ToString(), message);
        }

        [TestMethod]
        public void TestFullPark()
        {
            IVehiclePark vehiclePark = new VehiclePark(1, 3);
            var firstVehicle = new Car("CA1001HH", "Jay Margareta", 1);
            vehiclePark.InsertCar(firstVehicle, 1, 1, new DateTime(2015, 05, 04, 10, 30, 00));
            var secondVehicle = new Car("CA1111HH", "Guy Sheard", 2);
            vehiclePark.InsertCar(secondVehicle, 1, 2, new DateTime(2015, 05, 04, 10, 40, 00));
            var thirdVehicle = new Truck("C5842CH", "Jessie Raul", 5);
            vehiclePark.InsertTruck(thirdVehicle, 1, 3, new DateTime(2015, 05, 04, 10, 50, 00));
            string message = vehiclePark.GetStatus();

            var result = new StringBuilder();
            result.Append("Sector 1: 3 / 3 (100% full)");

            Assert.AreEqual(result.ToString(), message);
        }

        [TestMethod]
        public void TestPartialyFullPark()
        {
            IVehiclePark vehiclePark = new VehiclePark(1, 3);
            var firstVehicle = new Car("CA1001HH", "Jay Margareta", 1);
            vehiclePark.InsertCar(firstVehicle, 1, 1, new DateTime(2015, 05, 04, 10, 30, 00));
            var thirdVehicle = new Truck("C5842CH", "Jessie Raul", 5);
            vehiclePark.InsertTruck(thirdVehicle, 1, 3, new DateTime(2015, 05, 04, 10, 50, 00));
            string message = vehiclePark.GetStatus();

            var result = new StringBuilder();
            result.Append("Sector 1: 2 / 3 (67% full)");

            Assert.AreEqual(result.ToString(), message);
        }
    }
}
