using System;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected readonly float r_MaximumAmountOfEnergy;
        protected float m_CurrentAmountOfEnergy;

        public Engine(float i_MaximumAmountOfEnergy)
        {
            r_MaximumAmountOfEnergy = i_MaximumAmountOfEnergy;
        }

        public float MaximumAmountOfEnergy
        {
            get
            {
                return r_MaximumAmountOfEnergy;
            }
        }

        public float CurrentAmountOfEnergy
        {
            get
            {
                return m_CurrentAmountOfEnergy;
            }

            set
            {
                if (value <= r_MaximumAmountOfEnergy && value >= 0f)
                {
                    m_CurrentAmountOfEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0f, r_MaximumAmountOfEnergy);
                }
            }
        }
    }
}
