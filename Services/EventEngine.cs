using System;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;

namespace SimulationFIN31.Services;


/// <summary>
///     Core engine for applying event effects to the simulation state.
///     Handles the direct modification of state properties based on event impacts.
/// </summary>
public class EventEngine
{
    /// <summary>
    ///     Applies the calculated effects of an event, including any active debuffs,
    ///     to the simulation profile. Clamps values to their valid ranges.
    /// </summary>
    /// <param name="profile">The simulation state to modify.</param>
    /// <param name="debuffs">The calculated effects to apply.</param>
    public virtual void ApplyEffectsWithDebuffs(SimulationState profile, DebuffedEffects debuffs)
    {
        profile.CurrentStress = Math.Clamp(profile.CurrentStress + debuffs.StressImpact, 0, 100);
        profile.CurrentMood = Math.Clamp(profile.CurrentMood + debuffs.MoodImpact, -100, 100);
        profile.SocialBelonging = Math.Clamp(profile.SocialBelonging + debuffs.SocialBelongingImpact, 0, 100);
        profile.ResilienceScore = Math.Clamp(profile.ResilienceScore + debuffs.ResilienceImpact, 0, 80);
        profile.PhysicalHealth = Math.Clamp(profile.PhysicalHealth + debuffs.HealthImpact, 0, 100);
    }
}