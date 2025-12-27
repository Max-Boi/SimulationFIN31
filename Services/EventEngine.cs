using System;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;

namespace SimulationFIN31.Services;

/// <summary>
/// Engine responsible for applying life event effects to the simulation state.
/// </summary>
public class EventEngine
{
    /// <summary>
    /// Applies the raw effects of a life event to the simulation state.
    /// </summary>
    /// <param name="profile">The simulation state to modify.</param>
    /// <param name="lifeEvent">The life event whose effects should be applied.</param>
    public virtual void ApplyEffects(SimulationState profile, LifeEvent lifeEvent)
    {
        profile.CurrentStress = Clamp(profile.CurrentStress + lifeEvent.StressImpact, 0, 100);
        profile.CurrentMood = Clamp(profile.CurrentMood + lifeEvent.MoodImpact, -100, 100);
        profile.SocialBelonging = Clamp(profile.SocialBelonging + lifeEvent.SocialBelongingImpact, 0, 100);
        profile.ResilienceScore = Clamp(profile.ResilienceScore + lifeEvent.ResilienceImpact, 0, 100);
        profile.PhysicalHealth = Clamp(profile.PhysicalHealth + lifeEvent.HealthImpact, 0, 100);
    }

    /// <summary>
    /// Applies debuffed effects to the simulation state.
    /// Use this when mental illnesses are active to account for their impact on event effects.
    /// </summary>
    /// <param name="profile">The simulation state to modify.</param>
    /// <param name="debuffs">The pre-calculated debuffed effect values.</param>
    public virtual void ApplyEffectsWithDebuffs(SimulationState profile, DebuffedEffects debuffs)
    {
        profile.CurrentStress = Clamp(profile.CurrentStress + debuffs.StressImpact, 0, 100);
        profile.CurrentMood = Clamp(profile.CurrentMood + debuffs.MoodImpact, -100, 100);
        profile.SocialBelonging = Clamp(profile.SocialBelonging + debuffs.SocialBelongingImpact, 0, 100);
        profile.ResilienceScore = Clamp(profile.ResilienceScore + debuffs.ResilienceImpact, 0, 100);
        profile.PhysicalHealth = Clamp(profile.PhysicalHealth + debuffs.HealthImpact, 0, 100);
    }

    /// <summary>
    /// Clamps a value between a minimum and maximum.
    /// </summary>
    protected double Clamp(double value, double min, double max)
    {
        return Math.Max(min, Math.Min(max, value));
    }
}