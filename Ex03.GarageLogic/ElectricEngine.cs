namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaximumBatteryTimeInHours) : base(i_MaximumBatteryTimeInHours)
        {
        }

        public void ChargeBattery(float i_AmountToAdd)
        {
            if (m_CurrentAmountOfEnergy + i_AmountToAdd > r_MaximumAmountOfEnergy)
            {
                throw new ValueOutOfRangeException(0, r_MaximumAmountOfEnergy - m_CurrentAmountOfEnergy);
            }

            m_CurrentAmountOfEnergy += i_AmountToAdd;
        }

        public override string ToString()
        {
            string electricEngineDetails = string.Format(
                @"Engine Type: Electric
Battery Left: {0} hours
Maximum Battery Capacity: {1} hours 
",
                m_CurrentAmountOfEnergy,
                r_MaximumAmountOfEnergy);
            return electricEngineDetails;
        }
    }
}
