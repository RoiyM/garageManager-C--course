using System;
using System.Text;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInGarage> r_VehiclesInGarages;
        
        public Garage()
        {
            r_VehiclesInGarages = new Dictionary<string, VehicleInGarage>();
        }

        public string GetAllVehiclesInfo()
        {
            StringBuilder vehiclesInfo = new StringBuilder();
            int index = 1;
            foreach(var vehicleInGarage in r_VehiclesInGarages)
            {
                vehiclesInfo.AppendLine(index.ToString());
                vehiclesInfo.AppendLine(vehicleInGarage.Value.ToString());
                index++;
            }

            return vehiclesInfo.ToString();
        }

        public void Add(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            VehicleInGarage toAdd = new VehicleInGarage(i_OwnerName, i_OwnerPhone, i_Vehicle);
            toAdd.Status = VehicleInGarage.eStatus.InRepair;
            r_VehiclesInGarages.Add(i_Vehicle.LicenseNumber, toAdd);
        }

        public bool IsExist(string i_LicenseNumber)
        {
            return r_VehiclesInGarages.ContainsKey(i_LicenseNumber);
        }

        public void ChangeStatus(string i_LicenseNumber, VehicleInGarage.eStatus i_StatusToChange )
        {
            r_VehiclesInGarages.TryGetValue(i_LicenseNumber, out VehicleInGarage vehicleCard);
            vehicleCard.Status = i_StatusToChange;
        }

        public string GetAllLicenseNumbers()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach(var vehicleCard in r_VehiclesInGarages)
            {
                stringBuilder.AppendLine(vehicleCard.Key);
            }

            return stringBuilder.ToString();
        }

        public void RefuelVehicle(string i_LicenseNumber, FuelEngine.eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {
            r_VehiclesInGarages[i_LicenseNumber].RefuelVehicle(i_FuelType, i_AmountOfFuelToAdd);
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_AmountOfHoursToCharge)
        {
            r_VehiclesInGarages[i_LicenseNumber].ChargeVehicle(i_AmountOfHoursToCharge);
        }

        public string GetAllLicenseNumbersByStatus(VehicleInGarage.eStatus i_Status)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var vehicleCard in r_VehiclesInGarages)
            {
                if(vehicleCard.Value.Status == i_Status)
                {
                    stringBuilder.AppendLine(vehicleCard.Key);
                }
            }

            return stringBuilder.ToString();
        }

        public void SetWheelAirPressure(string i_LicenseNumber)
        {
            r_VehiclesInGarages[i_LicenseNumber].FillWheelsAirPressure();
        }

        public class VehicleInGarage
        {
            private readonly Vehicle r_Vehicle;
            private readonly string r_OwnerName;
            private readonly string r_OwnerPhone;
            private eStatus m_Status;

            public VehicleInGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
            {
                r_OwnerName = i_OwnerName;
                r_OwnerPhone = i_OwnerPhone;
                m_Status = eStatus.InRepair;
                r_Vehicle = i_Vehicle;
            }

            public void FillWheelsAirPressure()
            {
                r_Vehicle.WheelsAirPressure = r_Vehicle.MaximumWheelAirPressure;
            }

            public void RefuelVehicle(FuelEngine.eFuelType i_FuelType, float i_AmountOfFuelToAdd)
            {
                if (r_Vehicle.Engine is FuelEngine)
                {
                    ((FuelEngine)r_Vehicle.Engine).Refuel(i_AmountOfFuelToAdd, i_FuelType);
                    r_Vehicle.PercentageOfEnergyLeft = r_Vehicle.Engine.CurrentAmountOfEnergy
                                                       / r_Vehicle.Engine.MaximumAmountOfEnergy * 100f;
                }
                else
                {
                    throw new ArgumentException("Error! The vehicle does not have fuel engine... ");
                }
            }

            public void ChargeVehicle(float i_AmountOfHoursToCharge)
            {
                if (r_Vehicle.Engine is ElectricEngine)
                {
                    ((ElectricEngine)r_Vehicle.Engine).ChargeBattery(i_AmountOfHoursToCharge);
                    r_Vehicle.PercentageOfEnergyLeft = r_Vehicle.Engine.CurrentAmountOfEnergy
                                                       / r_Vehicle.Engine.MaximumAmountOfEnergy * 100f;
                }
                else
                {
                    throw new ArgumentException("Error! The vehicle does not have electric engine... ");
                }
            }

            public enum eStatus
            {
                InRepair = 1,
                Repaired,
                Paid
            }

            public Vehicle Vehicle
            {
                get
                {
                    return r_Vehicle;
                }
            }

            public eStatus Status
            {
                get
                {
                    return m_Status;
                }

                set
                {
                    m_Status = value;
                }
            }

            public override string ToString()
            {
                StringBuilder vehicleInfo = new StringBuilder();

                vehicleInfo.AppendFormat(
                    @"Owner Name: {0}
Owner Phone Number: {1}
Vehicle Status: {2}
",
                    r_OwnerName,
                    r_OwnerPhone,
                    m_Status);
                vehicleInfo.AppendLine(r_Vehicle.ToString());

                return vehicleInfo.ToString();
            }
        }
    }
}
