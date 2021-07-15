using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleGenerator
    {
        public enum eVehicleType
        {
            ElectricCar,
            ElectricMotorBike,
            FuelCar,
            FuelMotorBike,
            Truck
        }

        public static Vehicle GenerateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle=null;
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                    vehicle = new ElectricCar();
                    break;
                case eVehicleType.ElectricMotorBike:
                    vehicle= new ElectricMotorBike();
                    break;
                case eVehicleType.FuelCar:
                    vehicle =new FuelCar();
                    break;
                case eVehicleType.FuelMotorBike:
                    vehicle= new FuelMotorBike();
                    break;
                case eVehicleType.Truck:
                    vehicle =new Truck();
                    break;
            }

            return vehicle;
        }

        public static Car GenerateCarObject()
        {
            return new Car();
        }

        public static MotorBike GenerateMotorBikeObject()
        {
            return new MotorBike();
        }

        public static VehicleInGarage GenerateVehicleInGarageObject()
        {
            return new VehicleInGarage();
        }

    }
}
