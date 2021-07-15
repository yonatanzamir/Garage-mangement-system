using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class MotorBike
    {
        public enum eLicenseType
        {
            A,
            AA,
            B1,
            BB,
        }

        private const float k_MaxAirPressure = 30;
        private const int k_NumberOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        internal MotorBike()
        {
        }

        internal void BuildVehicleInfo(Dictionary<string, Type> i_InfoDictionary)
        {
            i_InfoDictionary.Add("LicenseType", m_LicenseType.GetType());
            i_InfoDictionary.Add("EngineVolume", m_EngineVolume.GetType());
        }

        public override string ToString()
        {
            string motorBikeDetails = string.Format("License Type: {0}{2}Engine Volume: {1}{2}", m_LicenseType, m_EngineVolume, Environment.NewLine);
            return motorBikeDetails;
        }

        public static float MaxAirPressure
        {
            get
            {
                return k_MaxAirPressure;
            }
        }

        public static int NumberOfWheels
        {
            get
            {
                return k_NumberOfWheels;
            }
        }


        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                if(value < 0)
                { 
                    throw new ArgumentException(string.Format("An error occured while trying to set engine volume to the motorbike, cannot be {0}", value));
                }
                else
                {
                    m_EngineVolume = value;
                }
                
            }
        }
    }
}


