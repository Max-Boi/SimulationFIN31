namespace SimulationFIN31.Models.History;

/// <summary>
/// Records a mental illness occurrence with onset and healing ages for timeline display.
/// Tracks the duration of each illness episode during the simulation.
/// </summary>
public sealed record IllnessRecord(
    string IllnessKey,
    string DisplayName,
    int OnsetAge,
    int? HealedAge
)
{
    /// <summary>
    /// Indicates whether the illness was still active at simulation end.
    /// </summary>
    public bool IsOngoing => HealedAge is null;

    /// <summary>
    /// Duration of the illness in years. Returns null if still ongoing.
    /// </summary>
    public int? Duration => HealedAge.HasValue ? HealedAge.Value - OnsetAge : null;
}
