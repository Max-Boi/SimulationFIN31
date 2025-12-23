namespace SimulationFIN31.Models.MentalDiseases;

public struct MidDepression
{
    const double StressDebuff = 0.65;
    const  double MoodDebuff = 0.6;

    public MidDepression()
    {
    }

    //how long the stress needs to be above the resilience to get this Condition
    public const int TRIGGER_COUNTDOWN = 4;

    //How long the SimulationState needs to be below the Threshold
    public const int HEALING_TIME = 2;
}