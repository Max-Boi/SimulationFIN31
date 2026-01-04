namespace SimulationFIN31.Models;

/// <summary>
///     Contains event effects after illness debuffs have been applied.
///     Used to modify how life events impact the avatar when illnesses are active.
/// </summary>
public readonly record struct DebuffedEffects(
    double StressImpact,
    double MoodImpact,
    double SocialBelongingImpact,
    double ResilienceImpact,
    double HealthImpact
);