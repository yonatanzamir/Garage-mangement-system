using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{


    public class Truck : FuelVehicle
    {
        private const float k_MaxAirPressure = 26;
        private const int k_NumberOfWheels = 16;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_MaxAmountOfFuel = 120;
        private bool m_IsContainDangerousMaterials;
        private float m_MaxCarryCapacity;

        public Truck(): base(k_MaxAmountOfFuel, k_FuelType)
        {
            BuildVehicleInfo();
            base.InitializeWheels(k_NumberOfWheels, k_MaxAirPressure);
        }

        public override string ToString()
        {
            string isContainDangerous;

            if (m_IsContainDangerousMaterials)
            {
                isContainDangerous = "Yes";
            }
            else
            {
                isContainDangerous = "No";
            }

            string truckDetails = string.Format("{0}Is Contain Dangerous Materials: {1}{3}Max Carry Capacity: {2}{3}", base.ToString(), isContainDangerous, m_MaxCarryCapacity, Environment.NewLine);
            return truckDetails;
        }

        protected void BuildVehicleInfo()
        {
            Dictionary.Add("IsContainDangerousMaterials", m_IsContainDangerousMaterials.GetType());
            Dictionary.Add("MaxCarryCapacity", m_MaxCarryCapacity.GetType());
        }


        public float MaxCarryCapacity
        {
            get
            {
                return m_MaxCarryCapacity;
            }

            set
            {
                if(value < 0)
                {
                    throw new ArgumentException(string.Format("An error occured while trying set maximal carry capacity of {0} ", value));
                }
                else
                {
                    m_MaxCarryCapacity = value;
                }
            }
        }

        public bool IsContainDangerousMaterials
        {
            get
            {
                return m_IsContainDangerousMaterials;
            }

            set
            {
                m_IsContainDangerousMaterials = value;
            }
        }
    }
}
