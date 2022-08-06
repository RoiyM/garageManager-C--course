using System;

namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        [Flags]
        public enum eVehicles
        {
            FuelMotorcycle = 1,
            ElectricMotorcycle,
            FuelCar,
            ElectricCar,
            FuelTruck
        }

        public static Vehicle Create(eVehicles i_VehicleType, string i_ModelName, string i_LicenseNumber, string i_WheelsManufacture)
        {
            Vehicle vehicle = null;

            switch(i_VehicleType)
            {
                case eVehicles.FuelCar:
                case eVehicles.ElectricCar:
                    vehicle = new Car(i_VehicleType, i_ModelName, i_LicenseNumber, i_WheelsManufacture);
                    break;
                case eVehicles.FuelMotorcycle:
                case eVehicles.ElectricMotorcycle:
                    vehicle = new Motorcycle(i_VehicleType, i_ModelName, i_LicenseNumber, i_WheelsManufacture);
                    break;
                case eVehicles.FuelTruck:
                    vehicle = new Truck(i_VehicleType, i_ModelName, i_LicenseNumber, i_WheelsManufacture);
                    break;
            }

            return vehicle;
        }
    }
}
