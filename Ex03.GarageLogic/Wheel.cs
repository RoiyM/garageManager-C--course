namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaximumAirPressure;
        private string m_Manufacture;
        private float m_CurrentAirPressure;

        public Wheel(float i_MaximumAirPressure, string i_Manufacture)
        {
            r_MaximumAirPressure = i_MaximumAirPressure;
            m_Manufacture = i_Manufacture;
        }

        public string Manufacture
        {
            get
            {
                return m_Manufacture;
            }

            set
            {
                m_Manufacture = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                if(value < 0 || value > r_MaximumAirPressure)
                {
                    throw new ValueOutOfRangeException(0, r_MaximumAirPressure);
                }

                m_CurrentAirPressure = value;
            }
        }

        public float MaximumAirPressure
        {
            get
            {
                return r_MaximumAirPressure;
            }
        }

        public override string ToString()
        {
            string wheelInfo = string.Format(
                @"Manufacture: {0}
Air Pressure: {1} PSI
Maximum Air Pressure: {2} PSI",
                m_Manufacture,
                m_CurrentAirPressure,
                r_MaximumAirPressure);

            return wheelInfo;
        }
    }
}
