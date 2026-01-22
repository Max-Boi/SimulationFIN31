using System;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;

namespace SimulationFIN31.Services;


public class EventEngine
{

   
    public virtual void ApplyEffectsWithDebuffs(SimulationState profile, DebuffedEffects debuffs)
    {
        profile.CurrentStress = Math.Clamp(profile.CurrentStress + debuffs.StressImpact, 0, 100);
        profile.CurrentMood = Math.Clamp(profile.CurrentMood + debuffs.MoodImpact, -100, 100);
        profile.SocialBelonging = Math.Clamp(profile.SocialBelonging + debuffs.SocialBelongingImpact, 0, 100);
        profile.ResilienceScore = Math.Clamp(profile.ResilienceScore + debuffs.ResilienceImpact, 0, 80);
        profile.PhysicalHealth = Math.Clamp(profile.PhysicalHealth + debuffs.HealthImpact, 0, 100);
    }
    
}