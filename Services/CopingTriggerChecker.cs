using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
///     Evaluates whether coping mechanism triggers are satisfied based on simulation state.
///     Trigger logic follows these rules:
///     - StressThreshold: Triggered when CurrentStress >= threshold
///     - MoodThreshold: Triggered when CurrentMood <= threshold (mood is negative when low)
///     - BelongingThreshold: Triggered when SocialBelonging <= threshold
///     If multiple thresholds are defined, ANY satisfied threshold triggers the mechanism
///     (OR logic), reflecting how coping can be activated by various distress signals.
///     Psychology basis: This models the multi-pathway activation of coping responses,
///     where stress, emotional distress, or social isolation can each independently
///     trigger coping behaviors (Lazarus & Folkman, 1984).
///     Age constraint: Coping mechanisms only become available at age 14+, reflecting
///     developmental psychology research showing that deliberate coping strategies
///     emerge during adolescence (Compas et al., 2001).
/// </summary>
public sealed class CopingTriggerChecker : ICopingTriggerChecker
{
    /// <summary>
    ///     Minimum age at which coping mechanisms can be triggered.
    ///     Based on developmental psychology: deliberate coping strategies
    ///     typically emerge during adolescence.
    /// </summary>
    private const int MINIMUM_COPING_AGE = 14;

    /// <summary>
    ///     Checks if the coping mechanism's trigger conditions are met.
    /// </summary>
    /// <param name="coping">The coping mechanism to evaluate.</param>
    /// <param name="state">Current simulation state.</param>
    /// <returns>True if any trigger threshold is met, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">When coping or state is null.</exception>
    public bool IsTriggered(CopingMechanism coping, SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(coping);
        ArgumentNullException.ThrowIfNull(state);

        var trigger = coping.Trigger;

        if (!trigger.HasAnyTrigger()) return true;

        if (trigger.StressThreshold.HasValue &&
            state.CurrentStress >= trigger.StressThreshold.Value)
            return true;

        if (trigger.MoodThreshold.HasValue &&
            state.CurrentMood <= trigger.MoodThreshold.Value)
            return true;

        if (trigger.BelongingThreshold.HasValue &&
            state.SocialBelonging <= trigger.BelongingThreshold.Value)
            return true;

        return false;
    }

    /// <summary>
    ///     Filters a collection of coping mechanisms to only those whose triggers are satisfied.
    ///     Also filters by age constraints using the CanOccur check.
    ///     Coping mechanisms are only available at age 14 or older.
    /// </summary>
    /// <param name="mechanisms">Collection of coping mechanisms to filter.</param>
    /// <param name="state">Current simulation state.</param>
    /// <returns>List of coping mechanisms that can be triggered, empty if age is below 14.</returns>
    /// <exception cref="ArgumentNullException">When mechanisms or state is null.</exception>
    public List<CopingMechanism> FilterTriggered(
        IEnumerable<CopingMechanism> mechanisms,
        SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(mechanisms);
        ArgumentNullException.ThrowIfNull(state);

        if (state.CurrentAge < MINIMUM_COPING_AGE) return [];

        var triggered = new List<CopingMechanism>();

        foreach (var mechanism in mechanisms)
            if (mechanism.CanOccur(state) && IsTriggered(mechanism, state))
                triggered.Add(mechanism);

        return triggered;
    }
}