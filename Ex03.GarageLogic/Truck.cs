namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_AmountOfWheels = 16;
        private const int k_MaxAirPressure = 26;
        private bool m_IsDeliveringHazardousMaterials;
        private float m_MaximumCarryWeight;

        public Truck(
            VehicleCreator.eVehicles i_VehicleType,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelsManufacture)
            : base(k_AmountOfWheels, k_MaxAirPressure, i_ModelName, i_LicenseNumber, i_WheelsManufacture)
        {
            m_EngineType = new FuelEngine(FuelEngine.eFuelType.Soler, 120F);
        }

        public bool IsDeliveringHazardousMaterials
        {
            get
            {
                return m_IsDeliveringHazardousMaterials;
            }

            set
            {
                m_IsDeliveringHazardousMaterials = value;
            }
        }

        public float MaximumCarryWeight
        {
            get
            {
                return m_MaximumCarryWeight;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ValueOutOfRangeException(0);
                }

                m_MaximumCarryWeight = value;
            }
        }

        public override string ToString()
        {
            string truckInfo = string.Format(
                @"Truck{0}

Is Delivering Hazardous Materials: {1}
Maximum Carry Weight: {2}",
                base.ToString(),
                m_IsDeliveringHazardousMaterials,
                m_MaximumCarryWeight);

            return truckInfo;
        }
    }
}
