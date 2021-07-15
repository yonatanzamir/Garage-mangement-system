using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : FuelVehicle
    {
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_MaxAmountOfFuel = 45;
        private readonly Car r_Car;


        internal FuelCar(): base(k_MaxAmountOfFuel, k_FuelType)
        {
            r_Car = VehicleGenerator.GenerateCarObject();
            base.InitializeWheels(Car.NumberOfWheels, Car.MaxAirPressure);
            BuildVehicleInfo();
        }

        protected void BuildVehicleInfo()
        {
            r_Car.BuildVehicleInfo(Dictionary);
        }

        public Car.eColor CarColor
        {
            get
            {
                return r_Car.CarColor;
            }

            set
            {
                r_Car.CarColor = value;
            }
        }

        public int NumberOfDoors
        {
            get
            {
                return r_Car.NumberOfDoors;
            }

            set
            {
                r_Car.NumberOfDoors = value;
            }
        }

        public override string ToString()
        {
            string vehicleDetails = string.Format("{0}{1}", base.ToString(), r_Car.ToString());
            return vehicleDetails;
        }

    }
}
