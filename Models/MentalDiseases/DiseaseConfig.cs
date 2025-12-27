namespace SimulationFIN31.Models.MentalDiseases;

public class DiseaseConfig
{
    public string Name { get; init; }
    public double StressDebuff { get; init; }
    public double MoodDebuff { get; init; }
    public double SocialProximityDebuff { get; init; }
    public int TriggerCountdown { get; init; }
    public int HealingTime { get; init; }
}
