using SimulationFIN31.Models.Enums;

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

    /// <summary>
    ///     Severity of this episode (Mild, Moderate, Severe).
    ///     Rolled at illness onset and affects debuff intensity.
    /// </summary>
    public EpisodeSeverity Severity { get; init; }

    /// <summary>
    ///     Random seed for Perlin noise generation.
    ///     Used for consistent, smooth debuff fluctuation over time.
    /// </summary>
    public int PerlinSeed { get; init; }

    /// <summary>
    ///     The last calculated fluctuation value (0.0 to 1.0).
    ///     Used for UI visualization of "good/bad days".
    /// </summary>
    public double LastFluctuation { get; set; }
}