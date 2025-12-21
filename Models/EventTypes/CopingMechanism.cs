using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.EventTypes;

/// <summary>
/// Represents a coping mechanism that can be triggered in response to stress,
/// low mood, or lack of social belonging. Coping mechanisms can be functional
/// (healthy), dysfunctional (harmful), or neutral.
/// </summary>
public class CopingMechanism : LifeEvent
{
    /// <summary>
    /// Classification of the coping mechanism's long-term health impact.
    /// Functional mechanisms promote resilience, dysfunctional ones may provide
    /// short-term relief but cause long-term harm.
    /// </summary>
    public CopingType Type { get; init; }

    /// <summary>
    /// Threshold conditions that trigger this coping mechanism.
    /// For example, high stress or low mood may activate certain coping behaviors.
    /// </summary>
    public CopingTrigger Trigger { get; init; }

    /// <summary>
    /// Short-term relief provided by this coping mechanism (0.0 to 1.0).
    /// Higher values indicate more immediate stress relief.
    /// </summary>
    public double ShortTermRelief { get; init; }

    /// <summary>
    /// Long-term impact multiplier on mental health.
    /// Values less than 1.0 indicate compounding negative effects over time.
    /// Values greater than 1.0 indicate building resilience.
    /// Neutral is 1.0.
    /// </summary>
    public double LongTermImpactMultiplier { get; init; } = 1.0;

    /// <summary>
    /// Indicates whether using this coping mechanism increases
    /// the likelihood of using it again in the future (habit formation).
    /// </summary>
    public bool IsHabitForming { get; init; }
}