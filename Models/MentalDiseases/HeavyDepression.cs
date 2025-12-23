namespace SimulationFIN31.Models.MentalDiseases;

public class HeavyDepression
{
    const double StressDebuff = 0.5;
    const  double MoodDebuff = 0.4;

    public HeavyDepression()
    {
    }

    //how long the stress needs to be above the resilience to get this Condition
    public const int TRIGGER_COUNTDOWN = 6;

    //How long the SimulationState needs to be below the Threshold
    public const int HEALING_TIME = 3;
}