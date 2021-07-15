using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly List<VehicleInGarage> r_VehiclesInGarage;

        public Garage()
        {
            r_VehiclesInGarage = new List<VehicleInGarage>();
        }

        public VehicleInGarage IsVehicleInGarage(string i_LicenseNumber)
        {
            VehicleInGarage isVehicleFound =null;

            foreach(VehicleInGarage vecInGarage in r_VehiclesInGarage)
            {
                if(vecInGarage.Vehicle.LicenseNumber == i_LicenseNumber)
                {
                    isVehicleFound = vecInGarage;
                    break;
                }
            }

            return isVehicleFound;
        }

        public List<string> VehiclesInGarageByStatus(string i_Status)
        {
            List<string> licenseNumberByStatus = new List<string>();
            VehicleInGarage.eVehicleStatus requestedStatus;
            bool isFiltered = VehicleInGarage.eVehicleStatus.TryParse(i_Status, out requestedStatus);
            if (isFiltered)
            {
                foreach (VehicleInGarage vecInGarage in r_VehiclesInGarage)
                {
                    if (vecInGarage.VehicleStatus == requestedStatus)
                    {
                        licenseNumberByStatus.Add(vecInGarage.Vehicle.LicenseNumber);
                    }
                }
            }
            else
            {
                foreach(VehicleInGarage vecInGarage in r_VehiclesInGarage)
                {
                    licenseNumberByStatus.Add(vecInGarage.Vehicle.LicenseNumber);
                }
            }

            return licenseNumberByStatus;
        }

        public List<VehicleInGarage> VehiclesInGarage
        {
            get
            {
                return r_VehiclesInGarage;
            }
        }
    }
}
