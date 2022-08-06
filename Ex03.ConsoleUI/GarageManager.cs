using System;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        private readonly Garage r_Garage;

        public GarageManager()
        {
            r_Garage = new Garage();
        }

        public void RunGarage()
        {
            eMenuOptions menuOption;
            do
            {
                Console.Clear();
                printMenu();
                menuOption = getInputMenu();
                Console.Clear();
                optionHandler(menuOption);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            while (menuOption != eMenuOptions.Exit);
            Console.WriteLine("See you again :)"); ////exit message
        }

        private void printMenu()
        {
            Console.WriteLine(
                @"Hello and welcome to the great super amazing garage!
1. Enter vehicle to the Garage.
2. Display license number list of all vehicles in the garage.
3. Change vehicle status in the garage.
4. Fill the air pressure to the maximum in the vehicle wheels.
5. Refuel a gas engine vehicle.
6. Charge electric engine vehicle.
7. Display full information on a specific vehicle by license number.
8. Exit
Please enter your choice: ");
        }

        private eMenuOptions getInputMenu()
        {
            bool isANumber = int.TryParse(Console.ReadLine(), out int input);
            bool correctInput = isANumber && input >= 1 && input <= 8;

            while (!correctInput)
            {
                Console.WriteLine("Wrong Input!! enter a valid option (1-7)");
                isANumber = int.TryParse(Console.ReadLine(), out input);
                correctInput = isANumber && input >= 1 && input <= 7;
            }

            return (eMenuOptions)input;
        }

        private void optionHandler(eMenuOptions i_MenuOptions)
        {
            switch (i_MenuOptions)
            {
                case eMenuOptions.EnterVehicle:
                    enterVehicleToTheGarage();
                    break;
                case eMenuOptions.DisplayLicenseNumberList:
                    printLicenseNumberOfVehiclesInTheGarage();
                    break;
                case eMenuOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOptions.FillWheelsAirPressureToTheMax:
                    fillWheelsAirPressureToTheMax();
                    break;
                case eMenuOptions.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eMenuOptions.ChargeBattery:
                    chargeVehicleBattery();
                    break;
                case eMenuOptions.DisplayFullVehicleInfo:
                    displayAllVehiclesFullInfo();
                    break;
                case eMenuOptions.Exit:
                    break;
            }
        }

        private void displayAllVehiclesFullInfo()
        {
            string fullVehiclesInfo = r_Garage.GetAllVehiclesInfo();

            if(string.IsNullOrEmpty(fullVehiclesInfo))
            {
                fullVehiclesInfo = "There are no vehicles in the garage!";
            }

            Console.WriteLine(fullVehiclesInfo);
        }

        private void chargeVehicleBattery()
        {
            try
            {
                bool isExist = getLicenseNumber(out string licenseNumber);
                if (isExist)
                {
                    Console.WriteLine("Please enter the required amount of minutes to charge: ");
                    float amountOfMinutesToCharge = float.Parse(Console.ReadLine());
                    float amountOfHoursToCharge = amountOfMinutesToCharge / 60;
                    r_Garage.ChargeVehicle(licenseNumber, amountOfHoursToCharge);
                    Console.WriteLine("Vehicle's {0} battery has charged successfully.", licenseNumber);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input, The amount of minutes needs to be a number.");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ValueOutOfRangeException exception)
            {
                if (exception.MaxValue == exception.MinValue)
                {
                    Console.WriteLine("Error! the battery is already full..");
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void refuelVehicle()
        {
            try
            {
                bool isExist = getLicenseNumber(out string licenseNumber);
                if(isExist)
                {
                    getChoiceFromNumberOfChoices(
                        "Please choose a fuel type: ",
                        typeof(FuelEngine.eFuelType),
                        out int fuelTypeChoice);
                    Console.WriteLine("Please enter the amount of fuel to refuel: ");
                    float amountOfFuel = float.Parse(Console.ReadLine());
                    r_Garage.RefuelVehicle(licenseNumber, (FuelEngine.eFuelType)fuelTypeChoice, amountOfFuel);
                    Console.WriteLine("Vehicle {0} has refueled successfully.", licenseNumber);
                }
            }
            catch(FormatException)
            {
                Console.WriteLine("Wrong input, The amount of fuel needs to be a number.");
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch(ValueOutOfRangeException exception)
            {
                if(exception.MaxValue == exception.MinValue)
                {
                    Console.WriteLine("Error! the fuel tank is already full..");
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void fillWheelsAirPressureToTheMax()
        {
            bool isExist = getLicenseNumber(out string licenseNumber);
            if(isExist)
            {
                r_Garage.SetWheelAirPressure(licenseNumber);
                Console.WriteLine("Vehicle {0} wheels has been filled successfully.", licenseNumber);
            }
        }

        private bool getLicenseNumber(out string o_LicenseNumber)
        {
            Console.WriteLine("Please enter vehicle's license number: ");
            o_LicenseNumber = Console.ReadLine();
            bool isExist = r_Garage.IsExist(o_LicenseNumber);

            if (!isExist)
            {
                Console.WriteLine("The Vehicle doesn't exist in the garage!");
            }

            return isExist;
        }

        private string getInputStringFromUser()
        {
            string input = Console.ReadLine();

            while (input.Equals(string.Empty))
            {
                Console.WriteLine(@"Wrong input!
                The input you entered cannot be empty, try again.");
                input = Console.ReadLine();
            }

            return input;
        }

        private void enterVehicleToTheGarage()
        {
            Console.WriteLine("Please enter vehicle license number:");
            string licenseNumber = getInputStringFromUser();

            if (r_Garage.IsExist(licenseNumber))
            {
                r_Garage.ChangeStatus(licenseNumber, Garage.VehicleInGarage.eStatus.InRepair);
                Console.WriteLine("The Vehicle is already in the garage!");
            }
            else
            {
                getVehicleOwnerDetails(out string ownerName, out string phoneNumber);
                getVehicleDetails(out string modelName, out VehicleCreator.eVehicles vehicleType);
                getVehicleWheelsDetails(out string wheelsManufacture);
                Vehicle vehicle = VehicleCreator.Create(vehicleType, modelName, licenseNumber, wheelsManufacture);
                getCurrentWheelsAirPressure(vehicle);
                handleSpecificDetails(vehicle, vehicleType);
                getCurrentAmountOfEnergy(vehicle);
                r_Garage.Add(vehicle, ownerName, phoneNumber);
                Console.WriteLine(@"Vehicle has entered successfully!");
            }
        }

        private void handleSpecificDetails(Vehicle i_Vehicle, VehicleCreator.eVehicles i_VehicleType)
        {
            switch (i_VehicleType)
            {
                case VehicleCreator.eVehicles.FuelCar:
                case VehicleCreator.eVehicles.ElectricCar:
                    getSpecificDetails(i_Vehicle as Car);
                    break;
                case VehicleCreator.eVehicles.FuelMotorcycle:
                case VehicleCreator.eVehicles.ElectricMotorcycle:
                    getSpecificDetails(i_Vehicle as Motorcycle); 
                    break;
                case VehicleCreator.eVehicles.FuelTruck:
                    getSpecificDetails(i_Vehicle as Truck);
                    break;
            }
        }

        private void getSpecificDetails(Car i_Car)
        {
            getAmountOfDoors(i_Car);
            getCarColor(i_Car);
        }

        private void getAmountOfDoors(Car i_Car)
        {
            try
            {
                printChoices("Please choose the amount of doors:", Enum.GetNames(typeof(Car.eAmountOfDoors)));
                i_Car.AmountOfDoors = (Car.eAmountOfDoors)int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Wrong input! the input should be one of the options, Try again..");
                getAmountOfDoors(i_Car);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                getAmountOfDoors(i_Car);
            }
        }

        private void getChoiceFromNumberOfChoices(string i_Message, Type i_TypeOfEnum, out int o_Choice)
        {
            try
            {
                printChoices(i_Message, Enum.GetNames(i_TypeOfEnum));
                o_Choice = int.Parse(Console.ReadLine());
                if(!Enum.IsDefined(i_TypeOfEnum, o_Choice))
                {
                    throw new FormatException();
                }
            }
            catch(FormatException)
            {
                Console.WriteLine("Wrong input! the input should be one of the options, Try again..");
                getChoiceFromNumberOfChoices(i_Message, i_TypeOfEnum, out o_Choice);
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                getChoiceFromNumberOfChoices(i_Message, i_TypeOfEnum, out o_Choice);
            }
        }

        private void getCarColor(Car i_Car)
        {
            getChoiceFromNumberOfChoices("Please choose the car color:", typeof(Car.eColor), out int choice);
            i_Car.Color = (Car.eColor)choice;
        }

        private void getSpecificDetails(Motorcycle i_Motorcycle)
        {
            getMotorcycleLicenseType(i_Motorcycle);
            getMotorcycleEngineVolume(i_Motorcycle);
        }

        private void getMotorcycleEngineVolume(Motorcycle i_Motorcycle)
        {
            try
            {
                Console.WriteLine("Please enter the engine volume :");
                i_Motorcycle.EngineVolumeInCC = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input, Input must be number, Try again...");
                getMotorcycleEngineVolume(i_Motorcycle);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                getMotorcycleEngineVolume(i_Motorcycle);
            }
            catch (ValueOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
                getMotorcycleEngineVolume(i_Motorcycle);
            }
        }

        private void getMotorcycleLicenseType(Motorcycle i_Motorcycle)
        {
            try
            {
                printChoices("Please choose the license type:", Enum.GetNames(typeof(Motorcycle.eLicenseType)));
                i_Motorcycle.LicenseType = (Motorcycle.eLicenseType)int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Wrong input! the input should be one of the options, Try again..");
                getMotorcycleLicenseType(i_Motorcycle);
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                getMotorcycleLicenseType(i_Motorcycle);
            }
        }

        private void getSpecificDetails(Truck i_Truck)
        {
            getTruckIsDeliveringHazardousMaterials(i_Truck);
            getTruckMaximumCarryWeight(i_Truck);
        }

        private void getTruckIsDeliveringHazardousMaterials(Truck i_Truck)
        {
            try
            {
                Console.WriteLine("Do you delivering hazardous materials? (Yes = true, No = false)");
                i_Truck.IsDeliveringHazardousMaterials = bool.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input, Input must be one of the options above, Try again...");
                getTruckIsDeliveringHazardousMaterials(i_Truck);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                getTruckIsDeliveringHazardousMaterials(i_Truck);
            }
        }

        private void getTruckMaximumCarryWeight(Truck i_Truck)
        {
            try
            {
                Console.WriteLine("Please enter the maximum carry weight :");
                i_Truck.MaximumCarryWeight = float.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input, Input must be number, Try again...");
                getTruckMaximumCarryWeight(i_Truck);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                getTruckMaximumCarryWeight(i_Truck);
            }
            catch (ValueOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
                getTruckMaximumCarryWeight(i_Truck);
            }
        }

        private void getCurrentWheelsAirPressure(Vehicle i_Vehicle)
        {
            try
            {
                Console.WriteLine("Enter Vehicle's Current Air Pressure In Wheels:");
                i_Vehicle.WheelsAirPressure = float.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input! Air pressure in wheels needs to be larger than zero, Try again.");
                getCurrentWheelsAirPressure(i_Vehicle);
            }
            catch (ValueOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
                getCurrentWheelsAirPressure(i_Vehicle);
            }
        }

        private void getVehicleWheelsDetails(out string o_WheelsManufacture)
        {
            Console.WriteLine("Please enter the wheels manufacture name");
            o_WheelsManufacture = getInputStringFromUser();
        }

        private void getVehicleDetails(out string o_ModelName, out VehicleCreator.eVehicles o_VehicleType)
        {
            o_VehicleType = getVehicleTypeFromUser();
            Console.WriteLine("Enter vehicle model:");
            o_ModelName = getInputStringFromUser();
        }

        private VehicleCreator.eVehicles getVehicleTypeFromUser()
        {
            string[] choicesList = Enum.GetNames(typeof(VehicleCreator.eVehicles));
            printChoices("Choose Vehicle Type:", choicesList);
            
            return getVehicleType();
        }

        private void printChoices(string i_MessageChoices, string[] i_ChoicesList)
        {
            StringBuilder stringBuilderMessage = new StringBuilder(i_MessageChoices);
            int indexChoice = 1;

            stringBuilderMessage.AppendLine();
            foreach (string vehicleType in i_ChoicesList)
            {
                stringBuilderMessage.AppendFormat("{0}. {1}", indexChoice, vehicleType);
                stringBuilderMessage.AppendLine();
                indexChoice++;
            }

            Console.WriteLine(stringBuilderMessage);
        }

        private VehicleCreator.eVehicles getVehicleType()
        {
            VehicleCreator.eVehicles typeToCreate;
            string vehicleType = Console.ReadLine();

            while (!VehicleCreator.eVehicles.TryParse(vehicleType, out typeToCreate))
            {
                Console.WriteLine("Wrong input! Try again.");
            }

            return typeToCreate;
        }

        private void getVehicleOwnerDetails(out string o_OwnerName, out string o_PhoneNumber)
        {
            o_OwnerName = getOwnerName();
            o_PhoneNumber = getOwnerPhoneNumber();
        }

        private void getCurrentAmountOfEnergy(Vehicle i_Vehicle)
        {
            try
            {
                if(i_Vehicle.Engine is FuelEngine)
                {
                    Console.WriteLine("Please enter the current amount of fuel left in litters:");
                }
                else
                {
                    Console.WriteLine("Please enter the current battery hours left:");
                }

                i_Vehicle.Engine.CurrentAmountOfEnergy = float.Parse(getInputStringFromUser());
                i_Vehicle.PercentageOfEnergyLeft = i_Vehicle.Engine.CurrentAmountOfEnergy / i_Vehicle.Engine.MaximumAmountOfEnergy * 100f;
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input! Current remaining energy need to be positive number, Try again.");
                getCurrentAmountOfEnergy(i_Vehicle);
            }
            catch (ValueOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
                getCurrentAmountOfEnergy(i_Vehicle);
            }
        }

        private string getOwnerPhoneNumber()
        {
            bool isLegalPhoneNumber = false;
            string phoneNumber;

            do
            {
                Console.WriteLine("Please enter the vehicle owner phone number:");
                phoneNumber = getInputStringFromUser();
                isLegalPhoneNumber = phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);

                if (!isLegalPhoneNumber)
                {
                    Console.WriteLine(
                        @"Wrong input!
Phone number need to contain only 10 numbers, Try again.");
                }
            }
            while (!isLegalPhoneNumber);

            return phoneNumber;
        }

        private string getOwnerName()
        {
            Console.WriteLine("Please enter the vehicle owner name:");

            return getInputStringFromUser();
        }

        private void printLicenseNumberOfVehiclesInTheGarage()
        {
            try
            {
                Console.WriteLine(@"Please choose the required option:
1. print all license number.
2. print license number by status.");
                int choice = int.Parse(Console.ReadLine());
                string licenseNumbers;

                if (choice == 1)
                {
                    licenseNumbers = r_Garage.GetAllLicenseNumbers();
                    
                    if (string.IsNullOrEmpty(licenseNumbers))
                    {
                        licenseNumbers = "There are no vehicles in the garage.";
                    }
                    
                    Console.WriteLine(licenseNumbers);
                }
                else if (choice == 2)
                {
                    getVehicleStatusInGarage(out Garage.VehicleInGarage.eStatus status);
                    licenseNumbers = r_Garage.GetAllLicenseNumbersByStatus(status);
                    
                    if (string.IsNullOrEmpty(licenseNumbers))
                    {
                        licenseNumbers = "There are no vehicles in the garage with the required status.";
                    }

                    Console.WriteLine(licenseNumbers);
                }
                else
                {
                    Console.WriteLine("Wrong input! The input should be one of the options.");
                    printLicenseNumberOfVehiclesInTheGarage();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input! The input should be a number.");
                printLicenseNumberOfVehiclesInTheGarage();
            }
        }

        private void getVehicleStatusInGarage(out Garage.VehicleInGarage.eStatus o_Status)
        {
            try
            {
                printChoices("Please choose vehicle status:", Enum.GetNames(typeof(Garage.VehicleInGarage.eStatus)));
                int choice = int.Parse(Console.ReadLine());

                while (!Enum.IsDefined(typeof(Garage.VehicleInGarage.eStatus), choice))
                {
                    Console.WriteLine("Wrong input!");
                    printChoices("Please choose vehicle status:", Enum.GetNames(typeof(Garage.VehicleInGarage.eStatus)));
                    choice = int.Parse(Console.ReadLine());
                }

                o_Status = (Garage.VehicleInGarage.eStatus)choice;
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input! The input should be a number.");
                getVehicleStatusInGarage(out o_Status);
            }
        }

        private void changeVehicleStatus()
        {
            bool isExist = getLicenseNumber(out string licenseNumber);
            getVehicleStatusInGarage(out Garage.VehicleInGarage.eStatus status);

            if(isExist)
            {
                r_Garage.ChangeStatus(licenseNumber, status);
                Console.WriteLine("Vehicle {0} status has changed successfully.", licenseNumber);
            }
        }

        private enum eMenuOptions
        {
            EnterVehicle = 1,
            DisplayLicenseNumberList,
            ChangeVehicleStatus,
            FillWheelsAirPressureToTheMax,
            RefuelVehicle,
            ChargeBattery,
            DisplayFullVehicleInfo,
            Exit
        }
    }
}
