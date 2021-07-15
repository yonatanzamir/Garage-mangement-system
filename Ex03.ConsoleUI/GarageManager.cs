using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        public static void RunGarageSystem()
        {
            bool toContinue = true;
            Garage garage = new Garage();

            int userChoice;
            while(toContinue)
            {
                userChoice = GarageUI.InputUserMenuChoice();
                RunGarageMenu(garage, userChoice, ref toContinue);
            }
        }

        public static void RunGarageMenu(Garage i_Garage, int i_UserChoice, ref bool io_Continue)
        {
            switch (i_UserChoice)
            {
                case 1:
                    AddNewVehicle(i_Garage);
                    break;
                case 2:
                    ShowLicenseNumberList(i_Garage);
                    break;
                case 3:
                    ChangeVehicleStatus(i_Garage);
                    break;
                case 4:
                    InflateToMaxAirPressure(i_Garage);
                    break;
                case 5:
                    RefuelVehicle(i_Garage);
                    break;
                case 6:
                    ChargeVehicle(i_Garage);
                    break;
                case 7:
                    ShowVehicleDetails(i_Garage);
                    break;
                case 8:
                    io_Continue = false;
                    break;
                default:
                    break;
            }
        }

        // $G$ DSN-006 (-5) This method should have been private.
        public static void AddNewVehicle(Garage i_Garage)
        {
            VehicleInGarage foundVehicleInGarage;
            string licenseNumber = GarageUI.InputLicenseNumber();
            foundVehicleInGarage = i_Garage.IsVehicleInGarage(licenseNumber);
            if(foundVehicleInGarage != null)
            {
                Console.WriteLine("This vehicle is already in garage, status changed to: In Repair");
                foundVehicleInGarage.VehicleStatus = VehicleInGarage.eVehicleStatus.InRepair;
            }
            else
            {
                VehicleInGarage newVehicle = GarageUI.InputVehicleInGarage(licenseNumber);
                i_Garage.VehiclesInGarage.Add(newVehicle);
                Console.WriteLine("| The vehicle was added to the garage successfully |");
            }
        }

        public static void ShowLicenseNumberList(Garage i_Garage)
        {
            string userStatusChoice = "unfiltered";
            Console.WriteLine("do you want to filter your option according to vehicle status ?");
            string userBooleanChoice = GarageUI.BooleanInput();
            if(bool.Parse(userBooleanChoice))
            {
                userStatusChoice = GarageUI.EnumInput(typeof(VehicleInGarage.eVehicleStatus));
                Console.WriteLine("List of license numbers of vehicles that are " + GarageUI.SeparateWordsInType(userStatusChoice) + ":");
            }
            else
            {
                Console.WriteLine("List of license numbers of all vehicles:");
            }

            List<string> vehiclesLicenseByStatus = i_Garage.VehiclesInGarageByStatus(userStatusChoice);
            foreach(string license in vehiclesLicenseByStatus)
            {
                Console.WriteLine(license);
            }
        }

        public static void ChangeVehicleStatus(Garage i_Garage)
        {
            bool isGoodInput = false;
            string licenseNumber;
            VehicleInGarage vehicleStatusToChange;
            if(i_Garage.VehiclesInGarage.Count != 0)
            {
                while(!isGoodInput)
                {
                    licenseNumber = GarageUI.InputLicenseNumber();
                    vehicleStatusToChange = i_Garage.IsVehicleInGarage(licenseNumber);
                    try
                    {
                        if(vehicleStatusToChange != null)
                        {
                            string newStatus = GarageUI.EnumInput(typeof(VehicleInGarage.eVehicleStatus));
                            vehicleStatusToChange.VehicleStatus = (VehicleInGarage.eVehicleStatus)Enum.Parse(typeof(VehicleInGarage.eVehicleStatus), newStatus);
                            isGoodInput = true;
                            Console.WriteLine("| Status changed successfully |");
                        }
                        else
                        {
                            throw new ArgumentException("The vehicle is not in the garage.");
                        }
                    }
                    catch(ArgumentException error)
                    {
                        Console.WriteLine(error.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in garage!");
            }
        }

        public static void InflateToMaxAirPressure(Garage i_Garage)
        {
            bool isGoodInput = false;
            string licenseNumber;
            VehicleInGarage vehicleToInflate;
            if(i_Garage.VehiclesInGarage.Count != 0)
            {
                while(!isGoodInput)
                {
                    licenseNumber = GarageUI.InputLicenseNumber();
                    vehicleToInflate = i_Garage.IsVehicleInGarage(licenseNumber);
                    try
                    {
                        if(vehicleToInflate != null)
                        {
                            vehicleToInflate.Vehicle.InflateAllWheelsToMax();
                            isGoodInput = true;
                            Console.WriteLine("| The inflate to max air pressure was completed successfully |");
                        }
                        else
                        {
                            throw new ArgumentException("The vehicle is not in the garage.");
                        }
                    }
                    catch(ArgumentException error)
                    {
                        Console.WriteLine(error.Message);
                    }
                    catch(ValueOutOfRangeException error)
                    {
                        Console.WriteLine(error.Message);
                        Console.WriteLine("The range of the value should be between {0} - {1}", error.MinValue, error.MaxValue);
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in garage!");
            }
        }

        public static void RefuelVehicle(Garage i_Garage)
        {
            FillVehicle(i_Garage, typeof(FuelVehicle), "fuel");
        }

        public static void ChargeVehicle(Garage i_Garage)
        {
            FillVehicle(i_Garage, typeof(ElectricVehicle), "electricity");
        }

        public static void FillVehicle(Garage i_Garage, Type i_VehicleType, string i_FillType)
        {
            string licenseNumber;
            VehicleInGarage vehicleToFill;
            bool isGoodInput = false;

            if(i_Garage.VehiclesInGarage.Count != 0)
            {
                while(!isGoodInput)
                {
                    licenseNumber = GarageUI.InputLicenseNumber();
                    vehicleToFill = i_Garage.IsVehicleInGarage(licenseNumber);
                    try
                    {
                        if(vehicleToFill != null)
                        {
                            if(vehicleToFill.Vehicle.GetType().BaseType == i_VehicleType)
                            {
                                if(vehicleToFill.Vehicle is FuelVehicle)
                                {
                                    FillFuel(vehicleToFill.Vehicle);
                                }
                                else if(vehicleToFill.Vehicle is ElectricVehicle)
                                {
                                    FillElectricity(vehicleToFill.Vehicle);
                                }

                                isGoodInput = true;
                                Console.WriteLine(String.Format("| The fill was completed successfully |"));
                                                        
                            }
                            else
                            {
                                throw new ArgumentException(string.Format("This is not a vehicle that's based on {0}!", i_FillType));
                            }
                        }
                        else
                        {
                            throw new ArgumentException("The vehicle is not in the garage.");
                        }
                    }
                    catch(ArgumentException error)
                    {
                        Console.WriteLine(error.Message);
                        Console.WriteLine();
                    }
                    catch(ValueOutOfRangeException error)
                    {
                        Console.WriteLine(error.Message);
                        Console.WriteLine("The range of {2} amount should be between {0} - {1}", error.MinValue, error.MaxValue, i_FillType);
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in garage!");
            }
        }

        public static void FillFuel(Vehicle i_FuelVehicle)
        {
            string selectedFuelType = GarageUI.EnumInput(typeof(FuelVehicle.eFuelType));
            FuelVehicle.eFuelType fuelType = (FuelVehicle.eFuelType)Enum.Parse(
                typeof(FuelVehicle.eFuelType),
                selectedFuelType);
            float amountToFill = GarageUI.GetAmountToFill("fuel");
            (i_FuelVehicle as FuelVehicle).Refuel(amountToFill, fuelType);
        }

        public static void FillElectricity(Vehicle i_ElectricVehicle)
        {
            float amountToFillInMinutes = GarageUI.GetAmountToFill("electricity");
            float amountToFillInHours = amountToFillInMinutes / 60f;
            (i_ElectricVehicle as ElectricVehicle).ChargeBattery(amountToFillInHours);
        }


        public static void ShowVehicleDetails(Garage i_Garage)
        {
            string licenseNumber;
            VehicleInGarage vehicleToShow;
            bool isGoodInput = false;


            if(i_Garage.VehiclesInGarage.Count != 0)
            {
                while(!isGoodInput)
                {
                    licenseNumber = GarageUI.InputLicenseNumber();
                    vehicleToShow = i_Garage.IsVehicleInGarage(licenseNumber);
                    try
                    {
                        if(vehicleToShow != null)
                        {
                            Console.WriteLine(vehicleToShow.ToString());
                            isGoodInput = true;
                        }
                        else
                        {
                            throw new ArgumentException("The vehicle is not in the garage.");
                        }
                    }
                    catch(ArgumentException error)
                    {
                        Console.WriteLine(error.Message);
                    }
                }
            }

            else
            {
                Console.WriteLine("There are no vehicles in garage!");
            }
        }
    }
}

