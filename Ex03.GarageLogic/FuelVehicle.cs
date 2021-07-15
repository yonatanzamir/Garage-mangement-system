using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        public enum eFuelType
        {
            Soler=1,
            Octan95,
            Octan96,
            Octan98,
        }
        private float m_CurrentAmountOfFuel;
        private readonly float r_MaxAmountOfFuel;
        private readonly eFuelType r_FuelType;

        internal FuelVehicle(float i_MaxAmountOfFuel, eFuelType i_FuelType)
        {
            r_MaxAmountOfFuel = i_MaxAmountOfFuel;
            r_FuelType = i_FuelType;
            BuildVehicleInfo();
        }

        protected void BuildVehicleInfo()
        {
            Dictionary.Add("CurrentAmountOfFuel", m_CurrentAmountOfFuel.GetType());
        }

        public void Refuel(float i_AmountFuelToAdd, eFuelType i_FuelType)
        {
            string msg;
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException(string.Format("Wrong fuel type, the correct fuel type is: {0}", r_FuelType));
            }
            else if (i_AmountFuelToAdd + m_CurrentAmountOfFuel > r_MaxAmountOfFuel)
            {
                msg = string.Format("An error occured while trying to refuel the vehicle with more {0} amount of fuel", i_AmountFuelToAdd);

                throw new ValueOutOfRangeException(0, r_MaxAmountOfFuel, msg);
            }
            else
            {
                m_CurrentAmountOfFuel += i_AmountFuelToAdd;
                this.Energy = (m_CurrentAmountOfFuel / r_MaxAmountOfFuel) * 100;
            }
        }

        public float CurrentAmountOfFuel
        {
            get
            {
                return m_CurrentAmountOfFuel;
            }

            set
            {
                if (value > r_MaxAmountOfFuel||value<0)
                {
                    throw new ValueOutOfRangeException(0, r_MaxAmountOfFuel, string.Format("An error occured while trying set amount of fuel of {0}", value));
                }
                else
                {
                    m_CurrentAmountOfFuel = value;
                    this.Energy = (m_CurrentAmountOfFuel / r_MaxAmountOfFuel) * 100;
                }
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public float MaxAmountOfFuel
        {
            get
            {
                return r_MaxAmountOfFuel;
            }
        }

        public override string ToString()
        {
            string vehicleDetails = string.Format("{0}Current Amount Of Fuel: {1}{4}Max Amount Of Fuel: {2}{4}Fuel Type: {3}{4}", base.ToString(), m_CurrentAmountOfFuel, r_MaxAmountOfFuel, r_FuelType, Environment.NewLine);
            return vehicleDetails;
        }

    }
}
