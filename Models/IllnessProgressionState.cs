namespace SimulationFIN31.Models;

/// <summary>
///     Tracks the progression state of an active mental illness.
/// </summary>
public sealed class IllnessProgressionState
{
    /// <summary>
    ///     The illness key (e.g., "MildDepression", "Alkoholismus").
    /// </summary>
    public required string IllnessKey { get; init; }

    /// <summary>
    ///     Number of simulation steps the illness has been active.
    ///     Used for time-based healing.
    /// </summary>
    public int StepsActive { get; set; }

    /// <summary>
    ///     The age at which the illness was triggered.
    /// </summary>
    public int OnsetAge { get; init; }
}