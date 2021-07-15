using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorBike : ElectricVehicle
    {
        private const float k_MaxBatteryTime = 1.8f;
        private readonly MotorBike r_MotorBike;

        internal ElectricMotorBike(): base(k_MaxBatteryTime)
        {
            r_MotorBike = VehicleGenerator.GenerateMotorBikeObject();
            base.InitializeWheels(MotorBike.NumberOfWheels, MotorBike.MaxAirPressure);
            BuildVehicleInfo();
        }

        public override string ToString()
        {
            string electricMotorBikeDetails = string.Format("{0}{1}", base.ToString(), r_MotorBike.ToString());
            return electricMotorBikeDetails;
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

