namespace VehicleParkSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using VehicleParkSystem.Contracts;
    using VehicleParkSystem.Vehicles;

    public class VehiclePark : IVehiclePark
    {
        private readonly ParkData parkData;

        private readonly ParkLayout parkLayout;

        public VehiclePark(int numberOfSectors, int placesPerSector)
        {
            this.parkData = new ParkData(numberOfSectors);
            this.parkLayout = new ParkLayout(numberOfSectors, placesPerSector);
        }

        public string InsertCar(Car car, int sector, int placeNumber, DateTime startTime)
        {
            return this.InsertVehicle(car, sector, placeNumber, startTime);
        }

        public string InsertMotorbike(Motorbike motorbike, int sector, int placeNumber, DateTime startTime)
        {
            return this.InsertVehicle(motorbike, sector, placeNumber, startTime);
        }

        public string InsertTruck(Truck truck, int sector, int placeNumber, DateTime startTime)
        {
            return this.InsertVehicle(truck, sector, placeNumber, startTime);
        }

        public string ExitVehicle(string licensePlate, DateTime endTime, decimal amountPaid)
        {
            if (!this.parkData.VehiclesByLicensePLate.ContainsKey(licensePlate))
            {
                return string.Format("There is no vehicle with license plate {0} in the park", licensePlate);
            }

            var vehicle = this.parkData.VehiclesByLicensePLate[licensePlate];
            var startTime = this.parkData.VehiclesByStartDate[vehicle];
            var exitTime = (int)Math.Round((endTime - startTime).TotalHours);
            var sectorAndPlace = this.parkData.VehiclesInPark[vehicle];
            var rate = vehicle.ReservedHours * vehicle.RegularRate;
            var overTimeRate = exitTime > vehicle.ReservedHours
                                   ? (exitTime - vehicle.ReservedHours) * vehicle.OvertimeRate
                                   : 0;
            var totalAmountToBePaid = rate + overTimeRate;
            var change = amountPaid - totalAmountToBePaid;

            var ticket = new StringBuilder();
            ticket.AppendLine(new string('*', 20))
                .AppendLine(string.Format("{0}", vehicle))
                .AppendLine(string.Format("at place {0}", sectorAndPlace))
                .AppendLine(string.Format("Rate: ${0:F2}", rate))
                .AppendLine(string.Format("Overtime rate: ${0:F2}", overTimeRate))
                .AppendLine(new string('-', 20))
                .AppendLine(string.Format("Total: ${0:F2}", totalAmountToBePaid))
                .AppendLine(string.Format("Paid: ${0:F2}", amountPaid))
                .AppendLine(string.Format("Change: ${0:F2}", change))
                .Append(new string('*', 20));

            int sector =
                int.Parse(
                    this.parkData.VehiclesInPark[vehicle].Split(
                        new[] { "(", ",", ")" },
                        StringSplitOptions.RemoveEmptyEntries)[0]);
            this.parkData.VehiclesBySectorAndPlace.Remove(this.parkData.VehiclesInPark[vehicle]);
            this.parkData.VehiclesInPark.Remove(vehicle);
            this.parkData.VehiclesByLicensePLate.Remove(vehicle.LicensePlate);
            this.parkData.VehiclesByStartDate.Remove(vehicle);
            this.parkData.VehiclesByOwner.Remove(vehicle.Owner, vehicle);

            // TODO: Check if incrementation is right
            this.parkData.NumberOfSectors[sector - 1]++;

            return ticket.ToString();
        }

        public string GetStatus()
        {
            var places =
                this.parkData.NumberOfSectors.Select(
                    (occupiedPlaces, sector) =>
                    string.Format(
                        "Sector {0}: {1} / {2} ({3}% full)",
                        sector + 1,
                        Math.Abs(occupiedPlaces),
                        this.parkLayout.PlacesPerSector,
                        Math.Abs(Math.Round((double)occupiedPlaces / this.parkLayout.PlacesPerSector * 100))));

            return string.Join(Environment.NewLine, places);
        }

        public string FindVehicle(string licensePlate)
        {
            if (!this.parkData.VehiclesByLicensePLate.ContainsKey(licensePlate))
            {
                return string.Format("There is no vehicle with license plate {0} in the park", licensePlate);
            }

            var vehicle = this.parkData.VehiclesByLicensePLate[licensePlate];

            return this.PrintVehicles(new[] { vehicle });
        }

        public string FindVehiclesByOwner(string owner)
        {
            // PERFORMANCE: Wrong way of searching in the wrong dictionary
            if (!this.parkData.VehiclesByOwner.ContainsKey(owner))
            {
                return string.Format("No vehicles by {0}", owner);
            }

            var vehiclesByOwner =
                this.parkData.VehiclesByOwner[owner].OrderBy(v => this.parkData.VehiclesByStartDate[v])
                    .ThenBy(v => v.LicensePlate);

            return this.PrintVehicles(vehiclesByOwner);
        }

        private string InsertVehicle(IVehicle vehicle, int sector, int placeNumber, DateTime startTime)
        {
            if (sector > this.parkLayout.NumberOfSectors)
            {
                return string.Format("There is no sector {0} in the park", sector);
            }

            if (placeNumber > this.parkLayout.PlacesPerSector)
            {
                return string.Format("There is no place {0} in sector {1}", placeNumber, sector);
            }

            if (this.parkData.VehiclesBySectorAndPlace.ContainsKey(string.Format("({0},{1})", sector, placeNumber)))
            {
                return string.Format("The place ({0},{1}) is occupied", sector, placeNumber);
            }

            if (this.parkData.VehiclesByLicensePLate.ContainsKey(vehicle.LicensePlate))
            {
                return string.Format("There is already a vehicle with license plate {0} in the park", vehicle.LicensePlate);
            }

            this.parkData.VehiclesInPark[vehicle] = string.Format("({0},{1})", sector, placeNumber);

            this.parkData.VehiclesBySectorAndPlace[string.Format("({0},{1})", sector, placeNumber)] = vehicle;
            this.parkData.VehiclesByLicensePLate[vehicle.LicensePlate] = vehicle;
            this.parkData.VehiclesByStartDate[vehicle] = startTime;
            this.parkData.VehiclesByOwner[vehicle.Owner].Add(vehicle);
            this.parkData.NumberOfSectors[sector - 1]--;
            return string.Format("{0} parked successfully at place ({1},{2})", vehicle.GetType().Name, sector, placeNumber);
        }

        private string PrintVehicles(IEnumerable<IVehicle> vehicles)
        {
            var result = new StringBuilder();
            foreach (var vehicle in vehicles)
            {
                result
                    .AppendLine(vehicle.ToString())
                    .AppendLine(string.Format("Parked at {0}", this.parkData.VehiclesInPark[vehicle]));
            }

            return result.ToString().Trim();
        }
    }
}
