namespace SimulationFIN31.Models.MentalDiseases;

public class DiseaseConfig
{
    public string Name { get; init; }
    public double StressDebuff { get; init; }
    public double MoodDebuff { get; init; }
    public double SocialProximityDebuff { get; init; }
    public int TriggerChance { get; init; }
    public int HealingTime { get; init; }

    /// <summary>
    /// Minimum age at which this illness can be triggered.
    /// Based on developmental psychology research.
    /// </summary>
    public int MinAge { get; init; }
}
