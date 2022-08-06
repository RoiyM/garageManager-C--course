using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(
                string.Format(
                    "Error! The value is out of range, should be between ({0} - {1})",
                    i_MinValue,
                    i_MaxValue))
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public ValueOutOfRangeException(float i_MinValue)
            : base(string.Format("Error! The value is out of range, should be above {0}", i_MinValue))
        {
        }

        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return r_MinValue;
            }
        }
    }
}
