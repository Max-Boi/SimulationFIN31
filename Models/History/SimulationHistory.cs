using System.Collections.Generic;

namespace SimulationFIN31.Models.History;

/// <summary>
///     Complete historical record of a simulation run for evaluation.
///     Immutable container passed from SimulationViewModel to EvaluationViewModel.
/// </summary>
public sealed record SimulationHistory(
    IReadOnlyList<TurnSnapshot> TurnSnapshots,
    IReadOnlyList<EventRecord> Events,
    IReadOnlyList<IllnessRecord> Illnesses,
    IReadOnlyList<CopingUsageRecord> CopingUsage,
    SimulationState FinalState
);