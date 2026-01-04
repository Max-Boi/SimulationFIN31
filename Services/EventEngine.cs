using System;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;

namespace SimulationFIN31.Services;

/// <summary>
///     Engine responsible for applying life event effects to the simulation state.
/// </summary>
public class EventEngine
{

    /// <summary>
    ///     Applies debuffed effects to the simulation state.
    ///     Use this when mental illnesses are active to account for their impact on event effects.
    /// </summary>
    /// <param name="profile">The simulation state to modify.</param>
    /// <param name="debuffs">The pre-calculated debuffed effect values.</param>
    public virtual void ApplyEffectsWithDebuffs(SimulationState profile, DebuffedEffects debuffs)
    {
        profile.CurrentStress = Math.Clamp(profile.CurrentStress + debuffs.StressImpact, 0, 100);
        profile.CurrentMood = Math.Clamp(profile.CurrentMood + debuffs.MoodImpact, -100, 100);
        profile.SocialBelonging = Math.Clamp(profile.SocialBelonging + debuffs.SocialBelongingImpact, 0, 100);
        profile.ResilienceScore = Math.Clamp(profile.ResilienceScore + debuffs.ResilienceImpact, 0, 95);
        profile.PhysicalHealth = Math.Clamp(profile.PhysicalHealth + debuffs.HealthImpact, 0, 100);
    }
    
}