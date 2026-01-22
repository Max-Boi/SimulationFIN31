using System.Linq;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Services;

/// <summary>
///     Checks trigger conditions for mental illnesses based on simulation state.
///     Extracted from IllnessManagerService for Clean Code compliance.
/// </summary>
public static class IllnessTriggerChecker
{
    /// <summary>
    ///     Checks if the trigger condition for a specific illness is met.
    /// </summary>
    /// <param name="illnessKey">Key of the illness to check</param>
    /// <param name="state">Current simulation state</param>
    /// <returns>True if trigger condition is met</returns>
    public static bool CheckTriggerCondition(string illnessKey, SimulationState state)
    {
        return illnessKey switch
        {
            "MildDepression" => state.CurrentStress > state.ResilienceScore,

            "generalisierteAngststörung" => state is { AnxietyLevel: > 60, CurrentStress: > 50 },

            "sozialePhobie" => state is { SocialBelonging: < 40, AnxietyLevel: > 50 },

            "Panikstörung" => state is { AnxietyLevel: > 70, CurrentStress: > 70 },

            "PTBS" => HasRecentTraumaticEvent(state, 2),

            "Alkoholismus" => GetCopingPreference(state, "coping_substance_use") > 0.5,

            "Substanzmissbrauch" => GetCopingPreference(state, "coping_substance_use") > 0.3
                                   && state.ParentsWithAddiction,

            "Magersucht" => state is { CurrentStress: > 70, CurrentMood: < -60 },

            "Bulimie" => GetCopingPreference(state, "coping_emotional_eating") > 0.5
                         && state.CurrentMood < -20,

            "BingeEatingStörung" => GetCopingPreference(state, "coping_emotional_eating") > 0.5
                                    && state.CurrentStress > 50,

            "Zwangsstörung" => state is { AnxietyLevel: > 90, CurrentStress: > 70 },

            "BorderlinePersonality" => state is
                { FamilyCloseness: < 30, CurrentMood: < -40, LifePhase: >= LifePhase.Adolescence },

            "DissociativeDisorder" => HasLifetimeTrauma(state) && state.CurrentStress > 80,

            _ => false
        };
    }

    /// <summary>
    ///     Checks if a traumatic event occurred within the last N years.
    /// </summary>
    private static bool HasRecentTraumaticEvent(SimulationState state, int yearsBack = 2)
    {
        if (state.TraumaticEventAges.Count == 0)
            return false;

        var recentThreshold = state.CurrentAge - yearsBack;
        return state.TraumaticEventAges.Any(age => age >= recentThreshold);
    }

    /// <summary>
    ///     Checks if any traumatic event occurred in the person's lifetime.
    /// </summary>
    private static bool HasLifetimeTrauma(SimulationState state)
    {
        return state.TraumaticEventAges.Count > 0;
    }

    /// <summary>
    ///     Gets a coping preference value from the state.
    /// </summary>
    private static double GetCopingPreference(SimulationState state, string copingId)
    {
        return state.CopingPreferences.TryGetValue(copingId, out var value) ? value : 0.0;
    }
}
