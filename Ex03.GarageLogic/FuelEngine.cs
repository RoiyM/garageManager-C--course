using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(eFuelType i_FuelType, float i_MaximumAmountOfFuelInLiters)
            : base(i_MaximumAmountOfFuelInLiters)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public void Refuel(float i_AmountToAdd, eFuelType i_FuelType)
        {
            if(m_FuelType != i_FuelType)
            {
                throw new ArgumentException("Fuel type doesn't match!");
            }

            if(m_CurrentAmountOfEnergy + i_AmountToAdd > r_MaximumAmountOfEnergy)
            {
                throw new ValueOutOfRangeException(0, r_MaximumAmountOfEnergy - m_CurrentAmountOfEnergy);
            }
            else
            {
                m_CurrentAmountOfEnergy += i_AmountToAdd;
            }
        }

        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        public override string ToString()
        {
            string fuelEngineDetails = string.Format(
                @"Engine Type: Fuel
Fuel Type: {0}
Fuel Left: {1} Litters
Maximum Fuel Capacity: {2} Litters
",
                m_FuelType,
                m_CurrentAmountOfEnergy,
                r_MaximumAmountOfEnergy);

            return fuelEngineDetails;
        }
    }
}
