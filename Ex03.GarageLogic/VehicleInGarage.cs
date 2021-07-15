using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eVehicleStatus m_VehicleStatus;

        internal VehicleInGarage()
        {
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public override string ToString()
        {
            string vehicleInGarageDetails = string.Format("Owner Name: {0}{3}Vehicle Status in garage: {1}{3}{2}Number Of Wheels: {4}{3}{5}", m_OwnerName, m_VehicleStatus, m_Vehicle.ToString(), Environment.NewLine,m_Vehicle.WheelsCollections.Count ,m_Vehicle.WheelsCollections[0].ToString());

            return vehicleInGarageDetails;
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }

            set
            {
                m_Vehicle = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }

            set
            {
                if(long.Parse(value) > 0)
                {
                    m_OwnerPhone = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("An error occured while trying to set owner's phone number, cannot be {0}", value));
                }
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;

            }
        }
      
        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

    }
}
