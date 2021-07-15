using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar: ElectricVehicle
    {
        private const float k_MaxBatteryTime = 3.2f;
        private readonly Car r_Car;


        internal ElectricCar(): base(k_MaxBatteryTime)
        {
            r_Car = VehicleGenerator.GenerateCarObject();
            base.InitializeWheels(Car.NumberOfWheels, Car.MaxAirPressure);
            BuildVehicleInfo();
        }

        public override string ToString()
        {
            string electricCarDetails = string.Format("{0}{1}", base.ToString(), r_Car.ToString());
            return electricCarDetails;
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
    }
}
