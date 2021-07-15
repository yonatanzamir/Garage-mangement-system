using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car
    {
        public enum eColor
        {
            Red,
            Silver,
            White,
            Black,
        }

        private eColor m_Color;
        private int m_NumberOfDoors;
        private const float k_MaxAirPressure = 32;
        private const int k_NumberOfWheels = 4;

        internal Car()
        {
        }

        internal void BuildVehicleInfo(Dictionary<string, Type> i_InfoDictionary)
        {
            i_InfoDictionary.Add("CarColor", m_Color.GetType());
            i_InfoDictionary.Add("NumberOfDoors", m_NumberOfDoors.GetType());
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

        public eColor CarColor
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public int NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                if(value < 2 || value > 5)
                {
                    string msg;
                    msg = string.Format("An error occured while trying to set {0} doors", value);
                    throw new ValueOutOfRangeException(2, 5, msg);
                }
                else
                {
                    m_NumberOfDoors = value;
                }
            }
        }

        public override string ToString()
        {
            string carDetails = string.Format("Color: {0}{2}Number Of Doors: {1}{2}", m_Color, m_NumberOfDoors, Environment.NewLine);
            return carDetails;
        }
    }
}
