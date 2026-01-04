using System.Collections.Generic;
using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models.MentalDiseases;

public class DiseaseConfig
{
    public string Name { get; init; }
    public double StressDebuff { get; init; }
    public double MoodDebuff { get; init; }
    public double SocialProximityDebuff { get; init; }
    public int TriggerChance { get; init; }
    public int HealingTime { get; init; }

    /// <summary>
    ///     Minimum age at which this illness can be triggered.
    ///     Based on developmental psychology research.
    /// </summary>
    public int MinAge { get; init; }

    /// <summary>
    ///     Gender-based probability modifiers for this illness.
    ///     Values represent multipliers applied to the base TriggerChance.
    ///     1.0 = no modification, >1.0 = increased risk, &lt;1.0 = decreased risk.
    ///     Based on epidemiological research showing gender differences in mental illness prevalence.
    /// </summary>
    public IReadOnlyDictionary<GenderType, double>? GenderModifiers { get; init; }

    /// <summary>
    ///     Gets the gender-specific trigger chance modifier for the given gender.
    ///     Returns 1.0 (no modification) if no gender-specific modifier is defined.
    /// </summary>
    /// <param name="gender">The gender to get the modifier for</param>
    /// <returns>Multiplier to apply to base trigger chance</returns>
    public double GetGenderModifier(GenderType gender)
    {
        if (GenderModifiers == null || !GenderModifiers.TryGetValue(gender, out var modifier)) return 1.0;
        return modifier;
    }
}