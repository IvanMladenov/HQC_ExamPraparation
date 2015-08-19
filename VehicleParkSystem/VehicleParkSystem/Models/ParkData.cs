namespace VehicleParkSystem.Models
{
    using System;
    using System.Collections.Generic;

    using VehicleParkSystem.Contracts;

    using Wintellect.PowerCollections;

    public class ParkData
    {
        public ParkData(int numberOfSectors)
        {
            this.NumberOfSectors = new int[numberOfSectors];
            this.VehiclesInPark = new Dictionary<IVehicle, string>();
            this.VehiclesBySectorAndPlace = new Dictionary<string, IVehicle>();
            this.VehiclesByLicensePLate = new Dictionary<string, IVehicle>();
            this.VehiclesByStartDate = new Dictionary<IVehicle, DateTime>();
            this.VehiclesByOwner = new MultiDictionary<string, IVehicle>(false);
        }

        public Dictionary<IVehicle, string> VehiclesInPark { get; set; }

        public Dictionary<string, IVehicle> VehiclesBySectorAndPlace { get; set; }

        public Dictionary<string, IVehicle> VehiclesByLicensePLate { get; set; }

        public Dictionary<IVehicle, DateTime> VehiclesByStartDate { get; set; }

        public MultiDictionary<string, IVehicle> VehiclesByOwner { get; set; }

        public int[] NumberOfSectors { get; set; }
    }
}
