using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models.History;

/// <summary>
///     Immutable snapshot of simulation state at a specific turn/age.
///     Used for tracking mental health metrics over time for evaluation charts.
/// </summary>
public sealed record TurnSnapshot(
    int Age,
    double Stress,
    double Mood,
    double SocialBelonging,
    double Resilience,
    double PhysicalHealth,
    LifePhase LifePhase
);