using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorBike: FuelVehicle
    {
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_MaxAmountOfFuel = 6;
        private readonly MotorBike r_MotorBike;


        internal FuelMotorBike(): base(k_MaxAmountOfFuel, k_FuelType)
        {
            r_MotorBike = VehicleGenerator.GenerateMotorBikeObject();
            base.InitializeWheels(MotorBike.NumberOfWheels, MotorBike.MaxAirPressure);
            BuildVehicleInfo();
        }

        public override string ToString()
        {
            string fuelMotorBikeDetails= string.Format("{0}{1}", base.ToString(), r_MotorBike.ToString());
            return fuelMotorBikeDetails;
        }

        protected void BuildVehicleInfo()
        {
            r_MotorBike.BuildVehicleInfo(Dictionary);
        }
        public MotorBike.eLicenseType LicenseType
        {
            get
            {
                return r_MotorBike.LicenseType;
            }

            set
            {
                r_MotorBike.LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return r_MotorBike.EngineVolume;
            }

            set
            {
                r_MotorBike.EngineVolume = value;
            }
        }

    }
}
