using System;
using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models.History;

/// <summary>
/// Records a life event occurrence with its impacts for evaluation display.
/// Captures the event details and effects at the time of occurrence.
/// </summary>
public sealed record EventRecord(
    string Id,
    string Name,
    string Description,
    int AgeOccurred,
    bool IsTraumatic,
    double StressImpact,
    double MoodImpact,
    double SocialImpact,
    double ResilienceImpact,
    double HealthImpact,
    EventCategory Category
)
{
    /// <summary>
    /// Calculates total absolute impact for ranking most impactful events.
    /// Sum of absolute values of stress, mood, social, and resilience impacts.
    /// </summary>
    public double TotalAbsoluteImpact =>
        Math.Abs(StressImpact) + Math.Abs(MoodImpact) +
        Math.Abs(SocialImpact) + Math.Abs(ResilienceImpact);
}
