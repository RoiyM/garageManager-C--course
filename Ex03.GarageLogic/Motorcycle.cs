using System;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_AmountOfWheels = 2;
        private const int k_MaxAirPressure = 30;
        private eLicenseType m_LicenseType;
        private int m_EngineVolumeInCC;

        public Motorcycle(
            VehicleCreator.eVehicles i_VehicleType,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelsManufacture)
            : base(k_AmountOfWheels, k_MaxAirPressure, i_ModelName, i_LicenseNumber, i_WheelsManufacture)
        {
            switch(i_VehicleType)
            {
                case VehicleCreator.eVehicles.FuelMotorcycle:
                    m_EngineType = new FuelEngine(FuelEngine.eFuelType.Octan98, 6);
                    break;
                case VehicleCreator.eVehicles.ElectricMotorcycle:
                    m_EngineType = new ElectricEngine(1.8F);
                    break;
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                if (!Enum.IsDefined(typeof(eLicenseType), value))
                {
                    throw new ArgumentException(string.Format("{0} is not one of the options.", value));
                }

                m_LicenseType = value;
            }
        }

        public int EngineVolumeInCC
        {
            get
            {
                return m_EngineVolumeInCC;
            }

            set
            {
                if(value <= 0)
                {
                    throw new ValueOutOfRangeException(0);
                }

                m_EngineVolumeInCC = value;
            }
        }

        public enum eLicenseType
        {
            A = 1,
            B1,
            AA,
            BB
        }

        public override string ToString()
        {
            string motorcycleInfo = string.Format(
                @"Motorcycle{0}

License Type: {1}
Engine Volume: {2} CC",
                base.ToString(),
                m_LicenseType,
                m_EngineVolumeInCC);

            return motorcycleInfo;
        }
    }
}
