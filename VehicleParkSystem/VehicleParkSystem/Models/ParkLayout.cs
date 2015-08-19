namespace VehicleParkSystem.Models
{
    using System;

    public class ParkLayout
    {
        private int numberOfSectors;

        private int placesPerSector;

        public ParkLayout(int numberOfSectors, int placesPerSector)
        {
            this.NumberOfSectors = numberOfSectors;
            this.PlacesPerSector = placesPerSector;
        }

        public int NumberOfSectors
        {
            get
            {
                return this.numberOfSectors;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The number of sectors must be positive.");
                }

                this.numberOfSectors = value;
            }
        }

        public int PlacesPerSector
        {
            get
            {
                return this.placesPerSector;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The number of places per sector must be positive.");
                }

                this.placesPerSector = value;
            }
        }
    }
}