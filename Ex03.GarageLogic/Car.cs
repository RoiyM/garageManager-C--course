using System;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_AmountOfWheels = 4;
        private const int k_MaxAirPressure = 32;
        private eColor m_Color;
        private eAmountOfDoors m_AmountOfDoors;

        public Car(
            VehicleCreator.eVehicles i_VehicleType,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelsManufacture)
            : base(k_AmountOfWheels, k_MaxAirPressure, i_ModelName, i_LicenseNumber, i_WheelsManufacture)
        {
            switch (i_VehicleType)
            {
                case VehicleCreator.eVehicles.FuelCar:
                    m_EngineType = new FuelEngine(FuelEngine.eFuelType.Octan95, 45);
                    break;
                case VehicleCreator.eVehicles.ElectricCar:
                    m_EngineType = new ElectricEngine(3.2F);
                    break;
            }
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }

            set
            {
                if(!Enum.IsDefined(typeof(eColor), value))
                {
                    throw new ArgumentException(string.Format("{0} is not one of the options.", value));
                }

                m_Color = value;
            }
        }

        public eAmountOfDoors AmountOfDoors
        {
            get
            {
                return m_AmountOfDoors;
            }

            set
            {
                if (!Enum.IsDefined(typeof(eAmountOfDoors), value))
                {
                    throw new ArgumentException(string.Format("{0} is not one of the options.", value));
                }

                m_AmountOfDoors = value;
            }
        }

        public enum eColor
        {
            Red = 1,
            Silver,
            White,
            Black
        }

        public enum eAmountOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        public override string ToString()
        {
            string carInfo = string.Format(
                @"Car{0}

Color: {1}
Number Of Doors: {2}",
                base.ToString(),
                m_Color,
                m_AmountOfDoors);

            return carInfo;
        }
    }
}
