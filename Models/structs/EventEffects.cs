namespace SimulationFIN31.Models.structs;

public readonly struct EventEffects
{
    public double StressImpact { get; }
    public double MoodImpact { get; }
    public double SocialBelongingImpact { get; }
    public double ResilienceImpact { get; }
    public double HealthImpact { get; }

    public EventEffects(
        double stressImpact = 0,
        double moodImpact = 0,
        double socialBelongingImpact = 0,
        double resilienceImpact = 0,
        double healthImpact = 0)
    {
        StressImpact = stressImpact;
        MoodImpact = moodImpact;
        SocialBelongingImpact = socialBelongingImpact;
        ResilienceImpact = resilienceImpact;
        HealthImpact = healthImpact;
    }
}