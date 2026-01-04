using System;
using System.Collections.Generic;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.EventTypes;

/// <summary>
///     Base class for all life events in the simulation.
///     Events represent significant occurrences that affect mental health state.
/// </summary>
public class LifeEvent
{
    /// <summary>
    ///     Unique identifier for the event. Used for tracking occurrence history
    ///     and preventing duplicate unique events.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    ///     Display name of the event.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    ///     Detailed description of the event for display purposes.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    ///     Indicates if this event is traumatic and can trigger PTSD/trauma-related conditions.
    ///     Based on psychological severity criteria (death, abuse, violence, severe rejection, etc.).
    /// </summary>
    public bool IsTraumatic { get; init; }

    /// <summary>
    ///     Base probability of the event occurring (0.0 to 1.0).
    ///     Modified by influence factors during probability calculation.
    /// </summary>
    public double BaseProbability { get; init; }

    /// <summary>
    ///     Minimum age at which this event can occur.
    /// </summary>
    public int MinAge { get; init; }

    /// <summary>
    ///     Maximum age at which this event can occur.
    /// </summary>
    public int MaxAge { get; init; } = 30;

    /// <summary>
    ///     Category of the event (Generic, Personal, or Coping).
    /// </summary>
    public EventCategory Category { get; init; }

    /// <summary>
    ///     Visual category for icon/image representation in the UI.
    ///     Determines which icon or illustration to display for this event.
    /// </summary>
    public VisualCategory VisualCategory { get; init; }

    /// <summary>
    ///     Factors that influence the probability of this event based on state attributes.
    /// </summary>
    public List<InfluenceFactor> InfluenceFactors { get; init; } = [];

    /// <summary>
    ///     When true, this event can only occur once per simulation.
    ///     The CanOccur method checks EventHistory to enforce this constraint.
    /// </summary>
    public bool IsUnique { get; init; }

    /// <summary>
    ///     Event IDs that must have occurred before this event can trigger.
    ///     Empty list means no prerequisites required.
    /// </summary>
    public List<string> Prerequisites { get; init; } = [];

    /// <summary>
    ///     Event IDs that prevent this event from occurring if already in history.
    ///     Useful for mutually exclusive events (e.g., "ParentsDivorced" excludes "ParentsRenewedVows").
    /// </summary>
    public List<string> Exclusions { get; init; } = [];

    /// <summary>
    ///     Impact on stress level (-100 to +100). Positive increases stress.
    /// </summary>
    public double StressImpact { get; init; }

    /// <summary>
    ///     Impact on mood (-100 to +100). Positive improves mood.
    /// </summary>
    public double MoodImpact { get; init; }

    /// <summary>
    ///     Impact on social belonging (0 to 100). Positive increases belonging.
    /// </summary>
    public double SocialBelongingImpact { get; init; }

    /// <summary>
    ///     Impact on resilience score (0 to 100). Positive builds resilience.
    /// </summary>
    public double ResilienceImpact { get; init; }

    /// <summary>
    ///     Impact on physical health (0 to 100). Positive improves health.
    /// </summary>
    public double HealthImpact { get; init; }


    /// <summary>
    ///     Determines if this event can occur given the current simulation state.
    ///     Checks age constraints, unique event history, prerequisites, and exclusions.
    /// </summary>
    /// <param name="state">The current simulation state.</param>
    /// <returns>True if the event can occur, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">When state is null.</exception>
    public bool CanOccur(SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        if (state.CurrentAge < MinAge || state.CurrentAge > MaxAge)
            return false;

        if (IsUnique && state.EventHistory.Contains(Id))
            return false;

        foreach (var prerequisite in Prerequisites)
            if (!state.EventHistory.Contains(prerequisite))
                return false;

        foreach (var exclusion in Exclusions)
            if (state.EventHistory.Contains(exclusion))
                return false;

        return true;
    }
}