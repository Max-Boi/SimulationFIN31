namespace SimulationFIN31.Models.MentalDiseases;

public struct LightDepression
{
    const double StressDebuff = 0.75;
    const  double MoodDebuff = 0.7;

    public LightDepression()
    {
    }

    //how long the stress needs to be above the resilience to get this Condition
   public const int TRIGGER_COUNTDOWN = 2;

   //How long the SimulationState needs to be below the Threshold
   public const int HEALING_TIME = 2;
}