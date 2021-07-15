using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        private float m_RemainingBatteryTime;
        private readonly float r_MaxBatteryTime;

        internal ElectricVehicle(float i_MaxBatteryTime)
        {
            r_MaxBatteryTime = i_MaxBatteryTime;
            BuildVehicleInfo();
        }
        protected void BuildVehicleInfo()
        {
            Dictionary.Add("RemainingBatteryTime", m_RemainingBatteryTime.GetType());
        }

        public override string ToString()
        {
            string vehicleDetails = string.Format("{0}Remaining Battery Time: {1}{3}Max Battery Time: {2}{3}", base.ToString(), m_RemainingBatteryTime, r_MaxBatteryTime, Environment.NewLine);
            return vehicleDetails;
        }

        public void ChargeBattery(float i_BatteryTimeToAdd)
        {
            string msg;
            if (m_RemainingBatteryTime + i_BatteryTimeToAdd > r_MaxBatteryTime)
            {
                msg = string.Format("An error occured while trying to charge the battery of the vehicle with more {0} hours", i_BatteryTimeToAdd);
                throw new ValueOutOfRangeException(0, r_MaxBatteryTime, msg);
            }
            else
            {
                m_RemainingBatteryTime += i_BatteryTimeToAdd;
                this.Energy = (m_RemainingBatteryTime / r_MaxBatteryTime) * 100;
            }
        }


        public float RemainingBatteryTime
        {
            get
            {
                return m_RemainingBatteryTime;
            }

            set
            {
                if (value > r_MaxBatteryTime)
                {
                    throw new ValueOutOfRangeException(0, r_MaxBatteryTime, string.Format("An error occured while trying set battery time of {0} hours", value));
                }
                else
                {
                    m_RemainingBatteryTime = value;
                    this.Energy = (m_RemainingBatteryTime / r_MaxBatteryTime) * 100;
                }
            }
        }

        public float MaxBatteryTime
        {
            get
            {
                return r_MaxBatteryTime;
            }
        }
    }
}
