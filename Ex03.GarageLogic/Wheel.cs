using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        internal Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public override string ToString()
        {
            string wheelDetails = string.Format("Current Air Pressure: {0}{2}Max Air Pressure: {1}{2}", m_CurrentAirPressure, r_MaxAirPressure, Environment.NewLine);
            return wheelDetails;
        }

        public void InflateToMaxAir()
        {
            float amountToInflate = r_MaxAirPressure - m_CurrentAirPressure;

            Inflate(amountToInflate);
        }

        public void Inflate(float i_AirPressureToAdd)
        {
            string msg;
            float sum = m_CurrentAirPressure + i_AirPressureToAdd;

            if ( sum> r_MaxAirPressure)
            {
                msg = string.Format("An error occured while trying to " + "inflate" + " the wheel with more {0} amount of air", i_AirPressureToAdd);
                throw new ValueOutOfRangeException(0, r_MaxAirPressure, msg);
            }
            else
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                if (value > r_MaxAirPressure || value < 0)
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure, string.Format("An error occured while trying set {0} amount of air pressure to the wheel", value));
                }
                else
                {
                    m_CurrentAirPressure = value;
                }
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

    }
}
