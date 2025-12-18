using System;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;

namespace SimulationFIN31.Services;

public class EventEngine
{
    public virtual void ApplyEffects(SimulationState profile, LifeEvent lifeEvent)
    {
        profile.CurrentStress = Clamp(profile.CurrentStress + lifeEvent.StressImpact, 0, 100);
        profile.CurrentMood = Clamp(profile.CurrentMood + lifeEvent.MoodImpact, -100, 100);
        profile.SocialBelonging = Clamp(profile.SocialBelonging + lifeEvent.SocialBelongingImpact, 0, 100);
        profile.ResilienceScore = Clamp(profile.ResilienceScore + lifeEvent.ResilienceImpact, 0, 100);
        profile.PhysicalHealth = Clamp(profile.PhysicalHealth + lifeEvent.HealthImpact, 0, 100);
    }
        
    protected double Clamp(double value, double min, double max)
    {
        return Math.Max(min, Math.Min(max, value));
    }
}