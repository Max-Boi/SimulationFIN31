namespace SimulationFIN31.Models.structs;

public readonly struct InfluenceFactor
{
        public string FactorName { get; }
        public double Exponent { get; }
        
        public InfluenceFactor(string name, double exponent)
        {
            FactorName = name;
            Exponent = exponent;
        }
}