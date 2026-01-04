using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models.History;

/// <summary>
///     Records a coping mechanism usage occurrence during the simulation.
///     Tracks which coping strategies were used and when.
/// </summary>
public sealed record CopingUsageRecord(
    string CopingId,
    string Name,
    CopingType Type,
    int AgeUsed
);