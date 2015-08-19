namespace VehicleParkSystem.Contracts
{
    using System;

    using VehicleParkSystem.Vehicles;

    /// <summary>
    /// Contains methods for inserting and exiting vehicles to and from the park system, finding vehicles by 
    /// given criteria and getting the status of the park system.
    /// </summary>
    public interface IVehiclePark
    {
        /// <summary>
        /// Method for inserting a car to the park system.
        /// </summary>
        /// <param name="car">The actual car</param>
        /// <param name="sector">The sector of the park where the car should be inserted</param>
        /// <param name="placeNumber">The place of the sector where the car should be inserted</param>
        /// <param name="startTime">The time at which the car is inserted</param>
        /// <returns>Returns a success message in case of a successfull insertion, and error messages 
        /// in case of incorrect data</returns>
        string InsertCar(Car car, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Method for inserting a motorbike to the park system.
        /// </summary>
        /// <param name="motorbike">The actual motorbike</param>
        /// <param name="sector">The sector of the park where the motorbike should be inserted</param>
        /// <param name="placeNumber">The place of the sector where the motorbike should be inserted</param>
        /// <param name="startTime">The time at which the motorbike is inserted</param>
        /// <returns>Returns a success message in case of a successfull insertion, and error messages 
        /// in case of incorrect data</returns>
        string InsertMotorbike(Motorbike motorbike, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Method for inserting a truck to the park system.
        /// </summary>
        /// <param name="truck">The actual truck</param>
        /// <param name="sector">The sector of the park where the truck should be inserted</param>
        /// <param name="placeNumber">The place of the sector where the truck should be inserted</param>
        /// <param name="startTime">The time at which the truck is inserted</param>
        /// <returns>Returns a success message in case of a successfull insertion, and error messages 
        /// in case of incorrect data</returns>
        string InsertTruck(Truck truck, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Method for exiting a vehicle from the park system.
        /// </summary>
        /// <param name="licensePlate">The licensePlate of the vehicle</param>
        /// <param name="endTime">The exiting time of the vehicle</param>
        /// <param name="amountPaid">The amount paid by the owner of the vehicle</param>
        /// <returns>Returns a ticket in case of a successfull exit, and an error message in case of the 
        /// licensePlate could not be found</returns>
        string ExitVehicle(string licensePlate, DateTime endTime, decimal amountPaid);

        /// <summary>
        /// Gets the status of the par system.
        /// </summary>
        /// <returns>Returns a message with information how many of the places are occupied and how full
        /// is the park</returns>
        string GetStatus();

        /// <summary>
        /// Finds a vehicle by its license plate.
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle</param>
        /// <returns>Returns a success message with information about the vehicle, its owner and its parking place, 
        /// and an error message if there is no vehicle with such license plate</returns>
        string FindVehicle(string licensePlate);

        /// <summary>
        /// Finds a vehicle by its owner
        /// </summary>
        /// <param name="owner">The owner of the vehicle</param>
        /// <returns>Returns a success message with information about the vehicle, its owner and its parking place, 
        /// and an error message if there is no vehicle with such owner</returns>
        string FindVehiclesByOwner(string owner);
    }
}
