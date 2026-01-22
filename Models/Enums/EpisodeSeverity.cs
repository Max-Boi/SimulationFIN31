namespace SimulationFIN31.Models.Enums;

/// <summary>
///     Severity level of a mental illness episode.
///     Rolled at illness onset and affects debuff intensity.
/// </summary>
public enum EpisodeSeverity
{
    /// <summary>60-80% of max debuff intensity.</summary>
    Mild,

    /// <summary>80-100% of max debuff intensity.</summary>
    Moderate,

    /// <summary>100-120% of max debuff intensity (can exceed base values).</summary>
    Severe
}
