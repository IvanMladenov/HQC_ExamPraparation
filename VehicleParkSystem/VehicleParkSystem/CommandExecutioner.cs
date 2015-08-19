namespace VehicleParkSystem
{
    using System;
    using System.Globalization;

    using VehicleParkSystem.Contracts;
    using VehicleParkSystem.Models;
    using VehicleParkSystem.Vehicles;

    public class CommandExecutioner
    {
        public IVehiclePark VehiclePark { get; set; }

        public string Execute(ICommand command)
        {
            if (command.CommandName != "SetupPark" && this.VehiclePark == null)
            {
                throw new InvalidOperationException("The vehicle park has not been set up");
            }

            string message = string.Empty;
            switch (command.CommandName)
            {
                case "SetupPark":
                    this.VehiclePark = new VehiclePark(
                        int.Parse(command.Parameters["sectors"]), 
                        int.Parse(command.Parameters["placesPerSector"]));
                    message = "Vehicle park created";
                    break;
                case "Park":
                    switch (command.Parameters["type"])
                    {
                        case "car":
                            message = this.VehiclePark.InsertCar(
                                    new Car(
                                        command.Parameters["licensePlate"],
                                        command.Parameters["owner"],
                                        int.Parse(command.Parameters["hours"])),
                                    int.Parse(command.Parameters["sector"]),
                                    int.Parse(command.Parameters["place"]),
                                    DateTime.Parse(command.Parameters["time"], null, DateTimeStyles.RoundtripKind));
                            break;
                        case "motorbike":
                            message = this.VehiclePark.InsertMotorbike(
                                    new Motorbike(
                                        command.Parameters["licensePlate"],
                                        command.Parameters["owner"],
                                        int.Parse(command.Parameters["hours"])),
                                    int.Parse(command.Parameters["sector"]),
                                    int.Parse(command.Parameters["place"]),
                                    DateTime.Parse(command.Parameters["time"], null, DateTimeStyles.RoundtripKind));
                            break;
                        case "truck":
                            message = this.VehiclePark.InsertTruck(
                                    new Truck(
                                        command.Parameters["licensePlate"],
                                        command.Parameters["owner"],
                                        int.Parse(command.Parameters["hours"])),
                                    int.Parse(command.Parameters["sector"]),
                                    int.Parse(command.Parameters["place"]),
                                    DateTime.Parse(command.Parameters["time"], null, DateTimeStyles.RoundtripKind));
                            break;
                    }

                    break;
                case "Exit":
                    message = this.VehiclePark.ExitVehicle(
                        command.Parameters["licensePlate"],
                        DateTime.Parse(command.Parameters["time"], null, DateTimeStyles.RoundtripKind),
                        decimal.Parse(command.Parameters["paid"]));
                    break;
                case "Status":
                    message = this.VehiclePark.GetStatus();
                    break;
                case "FindVehicle":
                    message = this.VehiclePark.FindVehicle(command.Parameters["licensePlate"]);
                    break;
                case "VehiclesByOwner":
                    message = this.VehiclePark.FindVehiclesByOwner(command.Parameters["owner"]);
                    break;
                default:
                    throw new InvalidOperationException("Invalid command.");
            }

            return message;
        }
    }
}
