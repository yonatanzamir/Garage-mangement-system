using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        public static bool TryParseAll(Type i_TypeToConvert, object i_ValueToConvert)
        {
            bool succeed = i_TypeToConvert.Name == "String";

            switch (i_TypeToConvert.Name.ToUpper())
            {
                case "SINGLE":
                    float floatInput;
                    succeed = float.TryParse(i_ValueToConvert.ToString(), out floatInput);
                    break;

                case "INT32":
                    int InttInput;
                    succeed = int.TryParse(i_ValueToConvert.ToString(), out InttInput);
                    break;
            }

            return succeed;
        }

        public static void SetPropertiesForMember(Type i_MemberType, string i_UserInput, string i_MemberName, Vehicle i_Vehicle)
        {
            PropertyInfo propertyInfo = i_Vehicle.GetType().GetProperty(i_MemberName);
            if (i_MemberType.IsEnum)
            {
                propertyInfo.SetValue(i_Vehicle, Enum.Parse(propertyInfo.PropertyType, i_UserInput));
            }
            else
            {
                propertyInfo.SetValue(i_Vehicle, Convert.ChangeType(i_UserInput, propertyInfo.PropertyType), null);
            }
        }

        public static VehicleInGarage InputVehicleInGarage(string i_LicenseNumber)
        {
            VehicleInGarage vehicleToGarage = VehicleGenerator.GenerateVehicleInGarageObject();
            vehicleToGarage.Vehicle = InputVehicle(i_LicenseNumber);
            InputOwnerDetails(vehicleToGarage);
            return vehicleToGarage;
        }

        public static VehicleGenerator.eVehicleType InputVehicleType()
        {
            string userInput = EnumInput(typeof(VehicleGenerator.eVehicleType));
            VehicleGenerator.eVehicleType userVehicleChoice = (VehicleGenerator.eVehicleType)Enum.Parse(typeof(VehicleGenerator.eVehicleType), userInput); ;

            return userVehicleChoice;
        }

        public static string InputLicenseNumber()
        {
            Console.WriteLine("Please enter license number: ");
            string userInput = Console.ReadLine();
            return userInput;
        }

        public static Vehicle InputVehicle(string i_LicenseNumber)
        {
            bool isValidInput = false;
            Vehicle vehicle = VehicleGenerator.GenerateVehicle(InputVehicleType());
            string userInput = String.Empty;
            vehicle.LicenseNumber = i_LicenseNumber;

            foreach (KeyValuePair<string, Type> obj in vehicle.Dictionary)
            {
                isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {

                        if (obj.Value == typeof(bool))
                        {
                            Console.Write("Please enter the ");
                            Console.WriteLine(SeparateWordsInType(obj.Key)+":");
                            userInput = BooleanInput();
                        }
                        else if ((obj.Value).IsEnum)
                        {
                            userInput = EnumInput(obj.Value);
                        }
                        else
                        {
                            Console.Write("Please enter the ");
                            Console.WriteLine(SeparateWordsInType(obj.Key) + ":");
                            userInput = Console.ReadLine();
                            if (!TryParseAll(obj.Value, userInput))
                            {
                                throw new FormatException("Invalid Format Input");
                            }
                        }

                        SetPropertiesForMember(obj.Value, userInput, obj.Key, vehicle);
                        isValidInput = true;

                    }

                    catch (TargetInvocationException e) // exception that we catch when we use reflection.
                                                        // We use nesting exception structure in order to take care of the original exception (inner exception).
                    {                                   // NOTE: When this exception is being thrown and we run the program with debugger (f5), there's a need to run 
                                                        // step by step (with f10). We talked to Guy about that and he said it's not a problem.
                        try
                        {
                            throw e.InnerException;
                        }
                        catch (ArgumentException error)
                        {
                            Console.WriteLine(error.Message);
                        }
                        catch (ValueOutOfRangeException error)
                        {
                            Console.WriteLine(error.Message);
                            Console.WriteLine(
                                "The range of the value should be between {0} - {1}",
                                error.MinValue,
                                error.MaxValue);
                        }
                    }
                    catch (FormatException error)
                    {
                        Console.WriteLine(error.Message);
                    }
                }
            }
            InputWheel(vehicle);

            return vehicle;
        }

        public static void PrintMenu()
        {
            string garageMenu = string.Format(
                @"
Please enter your choice (number between 1-8):
_______________________________________________

1. Add new vehicle to the garage.
2. Show Vehicles license numbers.
3. Change vehicle status.
4. Inflate wheels to maximum air pressure amount
5. Refuel gas based engine
6. Charge electricity based engine
7. Show full details of vehicle.
8. Exit program.
_______________________________________________");
            Console.WriteLine(garageMenu);
        }

        public static int InputUserMenuChoice()
        {
            PrintMenu();
            int userChoice;
            bool isGoodInput = int.TryParse(Console.ReadLine(), out userChoice);
            while (!isGoodInput || userChoice <= 0 || userChoice >= 9)
            {
                Console.WriteLine("Invalid input, please enter number between 1-8 :");
                isGoodInput = int.TryParse(Console.ReadLine(), out userChoice);
            }

            return userChoice;
        }
        public static void InputOwnerDetails(VehicleInGarage i_VehicleToGarage)
        {
            Console.WriteLine("Please enter Owner Name: ");
            string ownerName = Console.ReadLine();
            i_VehicleToGarage.OwnerName = ownerName;

            Console.WriteLine("Please enter Owner Phone Number: ");
            string ownerPhone = Console.ReadLine();
            long phoneNumber;
            bool isNumber = long.TryParse(ownerPhone, out phoneNumber);
            bool isValidPhoneNumber = false;

            while (!isValidPhoneNumber)
            {
                try
                {
                    while (!isNumber)
                    {
                        Console.WriteLine("Invalid Phone Number! Please enter again");
                        ownerPhone = Console.ReadLine();
                        isNumber = long.TryParse(ownerPhone, out phoneNumber);
                    }

                    i_VehicleToGarage.OwnerPhone = ownerPhone;
                    isValidPhoneNumber = true;
                }

                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    isNumber = false;
                }
            }
        }

        public static void InputWheel(Vehicle i_Vehicle)
        {
            Console.WriteLine("Please enter Wheel Manufacturer Name: ");
            string manufacturerName = Console.ReadLine();
            float currentAirPressure = 0;
            bool validParsingInput = false;
            bool validRangeInput = false;
            Console.WriteLine("Please enter Current Air Pressure: ");

            while (!validRangeInput)
            {
                try
                {
                    validParsingInput = float.TryParse(Console.ReadLine(), out currentAirPressure);
                    while (!validParsingInput)
                    {
                        Console.WriteLine("Invalid input! Please enter valid air pressure");
                        validParsingInput = float.TryParse(Console.ReadLine(), out currentAirPressure);
                    }

                    i_Vehicle.UpdateVehicleWheels(manufacturerName, currentAirPressure);
                    validRangeInput = true;
                }


                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Please enter air pressure between {0} - {1}:", exception.MinValue, exception.MaxValue);
                    validParsingInput = false;
                }
            }
        }

        public static string BooleanInput()
        {
            bool isBooleanInputValid = false;
            string userInput;
            string boolInputRes = "true";
            string msgBoolean = string.Format(@"1. Yes
2. No");

            while (!isBooleanInputValid)
            {
                Console.WriteLine(msgBoolean);
                userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    boolInputRes = "true";
                    isBooleanInputValid = true;
                }
                else if (userInput == "2")
                {
                    boolInputRes = "false";
                    isBooleanInputValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter again:");
                }
            }

            return boolInputRes;
        }

        public static StringBuilder SeparateWordsInType(string i_Word)
        {
            StringBuilder separateWords = new StringBuilder();
            separateWords.Append(i_Word[0]);
            for (int i = 1; i < i_Word.Length; i++)
            {
                if (char.IsUpper(i_Word[i]))
                {
                    separateWords.Append(' ');
                    separateWords.Append(i_Word[i]);
                }
                else
                {
                    separateWords.Append(i_Word[i]);
                }
            }

            return separateWords;
        }

        public static void EnumOutput(Type i_EnumType)
        {
            int enumValueCounter = 1;
            Console.Write("Please enter the ");
            Console.WriteLine(SeparateWordsInType((i_EnumType.Name).Substring(1))+":");
            foreach (object enumValue in i_EnumType.GetEnumValues())
            {
                if (enumValueCounter != 0)
                {
                    Console.WriteLine(string.Format("{0}. {1}", enumValueCounter, enumValue.ToString()));
                }

                enumValueCounter++;
            }
        }

        public static string EnumInput(Type i_EnumType)
        {
            string enumUserInput;
            bool validInput = false;
            int inputResult = 0;
            EnumOutput(i_EnumType);
            int enumSize = i_EnumType.GetEnumValues().Length;
            while (!validInput)
            {
                try
                {
                    enumUserInput = Console.ReadLine();
                    validInput = int.TryParse(enumUserInput, out inputResult);

                    if (validInput)
                    {
                        if (inputResult < 1 || inputResult > enumSize)
                        {
                            validInput = false;
                            throw new ValueOutOfRangeException(1, enumSize, "You are out of the enum range");
                        }
                    }
                    else
                    {
                        throw new FormatException("Invalid format input please enter again");
                    }
                }
                catch (FormatException error)
                {
                    Console.WriteLine(error.Message);
                }
                catch (ValueOutOfRangeException error)
                {
                    Console.WriteLine(error.Message);
                    Console.WriteLine("please enter number between {0} - {1}:", error.MinValue, error.MaxValue);
                }
            }

            return (i_EnumType.GetEnumValues().GetValue(inputResult - 1)).ToString();
        }

        public static float GetAmountToFill(string i_MsgToUser)
        {
            float amountToFill;
            bool isValidInput = false;
            Console.WriteLine("Please enter amount of {0} to fill:", i_MsgToUser);
            isValidInput = float.TryParse(Console.ReadLine(), out amountToFill);
            while (!isValidInput)
            {
                Console.WriteLine("Invalid input! Please enter valid amount:");
                isValidInput = float.TryParse(Console.ReadLine(), out amountToFill);
            }

            return amountToFill;
        }
    }
}

