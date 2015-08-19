namespace VehicleParkSystem.Vehicles
{
    public class Motorbike : Vehicle
    {
        private const decimal MotorBikeRegularRate = 1.35m;

        private const decimal MotorBikeOvertimeRate = 3m;

        public Motorbike(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, MotorBikeRegularRate, MotorBikeOvertimeRate, reservedHours)
        {
        }
    }
}
