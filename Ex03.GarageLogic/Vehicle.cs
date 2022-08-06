using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly List<Wheel> r_WheelsList;
        private readonly string r_Model;
        private readonly string r_LicenseNumber;
        private float m_PercentageOfEnergyLeft;
        protected Engine m_EngineType;

        public Vehicle(int i_AmountOfWheels, float i_MaxAirPressureInWheels, string i_Model, string i_LicenseNumber, string i_WheelsManufacture)
        {
            r_WheelsList = new List<Wheel>(i_AmountOfWheels);

            for(int i = 0; i < i_AmountOfWheels; i++)
            {
                r_WheelsList.Add(new Wheel(i_MaxAirPressureInWheels, i_WheelsManufacture));
            }

            r_Model = i_Model;
            r_LicenseNumber = i_LicenseNumber;
            m_EngineType = null;
        }

        public Engine Engine
        {
            get
            {
                return m_EngineType;
            }
        }

        public string Model
        {
            get
            {
                return r_Model;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public float PercentageOfEnergyLeft
        {
            get
            {
                return m_PercentageOfEnergyLeft;
            }

            set
            {
                m_PercentageOfEnergyLeft = value;
            }
        }

        public string WheelsManufacture
        {
            get
            {
                return r_WheelsList[0].Manufacture;
            }

            set
            {
                foreach(var wheel in r_WheelsList)
                {
                    wheel.Manufacture = value;
                }
            }
        }

        public float WheelsAirPressure
        {
            get
            {
                return r_WheelsList[0].CurrentAirPressure;
            }

            set
            {
                foreach (var wheel in r_WheelsList)
                {
                    wheel.CurrentAirPressure = value;
                }
            }
        }

        public float MaximumWheelAirPressure
        {
            get
            {
                return r_WheelsList[0].MaximumAirPressure;
            }
        }

        public override string ToString()
        {
            string vehicleInfo = string.Format(
                @" Details:
Model Name: {0}
License Number: {1}
Percent Of Energy Left: {2}%
{3}
{4} Wheels: 
{5}",
                r_Model,
                r_LicenseNumber,
                m_PercentageOfEnergyLeft,
                m_EngineType,
                r_WheelsList.Count,
                r_WheelsList[0]);

            return vehicleInfo;
        }
    }
}
