using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.MentalDiseases;

namespace SimulationFIN31.Services;

/// <summary>
///     Calculates combined debuffs from active mental illnesses.
///     Uses Perlin noise for smooth fluctuation and episode severity for intensity scaling.
/// </summary>
public sealed class DebuffCalculator
{
    private const double MaxStressDebuff = 2.5;
    private const double MinMoodDebuff = 0.3;
    private const double MinSocialDebuff = 0.3;

    private readonly Dictionary<string, PerlinNoise> _noiseGenerators = new();

    /// <summary>
    ///     Calculates combined debuffs for all active illnesses.
    /// </summary>
    /// <param name="currentIllnesses">Currently active illnesses</param>
    /// <param name="progressionStates">Progression states including severity and noise seed</param>
    /// <returns>Combined stress, mood, and social debuffs</returns>
    public (double stressDebuff, double moodDebuff, double socialDebuff) CalculateCombinedDebuffs(
        IReadOnlyDictionary<string, DiseaseConfig> currentIllnesses,
        IReadOnlyDictionary<string, IllnessProgressionState> progressionStates)
    {
        var stressDebuff = 1.0;
        var moodDebuff = 1.0;
        var socialDebuff = 1.0;

        foreach (var (key, config) in currentIllnesses)
        {
            if (!progressionStates.TryGetValue(key, out var progression))
                continue;

            var (stress, mood, social) = CalculateSingleDebuff(config, progression);
            stressDebuff *= stress;
            moodDebuff *= mood;
            socialDebuff *= social;
        }

        // Cap at reasonable maximums
        stressDebuff = Math.Min(stressDebuff, MaxStressDebuff);
        moodDebuff = Math.Max(moodDebuff, MinMoodDebuff);
        socialDebuff = Math.Max(socialDebuff, MinSocialDebuff);

        return (stressDebuff, moodDebuff, socialDebuff);
    }

    /// <summary>
    ///     Calculates dynamic debuffs for a single illness using Perlin noise.
    /// </summary>
    private (double stress, double mood, double social) CalculateSingleDebuff(
        DiseaseConfig config,
        IllnessProgressionState progression)
    {
        // Get or create noise generator for this illness
        if (!_noiseGenerators.TryGetValue(progression.IllnessKey, out var noise))
        {
            noise = new PerlinNoise(progression.PerlinSeed);
            _noiseGenerators[progression.IllnessKey] = noise;
        }

        // Get fluctuation value (0 to 1)
        var fluctuation = noise.GetFluctuationValue(progression.StepsActive, config.Volatility);
        
        // Update state for UI visualization
        progression.LastFluctuation = fluctuation;

        // Calculate severity multiplier
        var severityMultiplier = GetSeverityMultiplier(progression.Severity);

        // Calculate recovery factor (debuffs reduce as healing approaches)
        var recoveryFactor = CalculateRecoveryFactor(progression.StepsActive, config.HealingTime);

        // Interpolate debuffs within range based on fluctuation
        double stress, mood, social;

        if (config.HasDebuffRanges)
        {
            stress = InterpolateDebuff(config.StressDebuffMin, config.StressDebuffMax, fluctuation);
            mood = InterpolateDebuff(config.MoodDebuffMin, config.MoodDebuffMax, fluctuation);
            social = InterpolateDebuff(config.SocialDebuffMin, config.SocialDebuffMax, fluctuation);
        }
        else
        {
            // Fallback to fixed values with small fluctuation
            var variance = 0.1 * (fluctuation - 0.5); // ±5% variance
            stress = config.StressDebuff * (1 + variance);
            mood = config.MoodDebuff * (1 + variance);
            social = config.SocialProximityDebuff * (1 + variance);
        }

        // Apply severity multiplier (affects how much debuffs deviate from 1.0)
        stress = ApplySeverityToDebuff(stress, severityMultiplier, isStressDebuff: true);
        mood = ApplySeverityToDebuff(mood, severityMultiplier, isStressDebuff: false);
        social = ApplySeverityToDebuff(social, severityMultiplier, isStressDebuff: false);

        // Apply recovery factor
        stress = ApplyRecoveryToDebuff(stress, recoveryFactor, isStressDebuff: true);
        mood = ApplyRecoveryToDebuff(mood, recoveryFactor, isStressDebuff: false);
        social = ApplyRecoveryToDebuff(social, recoveryFactor, isStressDebuff: false);

        return (stress, mood, social);
    }

    /// <summary>
    ///     Interpolates between min and max debuff based on fluctuation.
    ///     For stress debuffs (>1.0): higher fluctuation = worse (higher value).
    ///     For mood/social debuffs (<1.0): lower fluctuation = worse (lower value).
    /// </summary>
    private static double InterpolateDebuff(double min, double max, double fluctuation)
    {
        return min + (max - min) * fluctuation;
    }

    /// <summary>
    ///     Gets multiplier based on episode severity.
    ///     Mild = 0.7, Moderate = 1.0, Severe = 1.2
    /// </summary>
    private static double GetSeverityMultiplier(EpisodeSeverity severity)
    {
        return severity switch
        {
            EpisodeSeverity.Mild => 0.7,
            EpisodeSeverity.Moderate => 1.0,
            EpisodeSeverity.Severe => 1.2,
            _ => 1.0
        };
    }

    /// <summary>
    ///     Calculates recovery factor based on how close to healing.
    ///     Returns 1.0 at onset, gradually decreasing to 0.5 at healing time.
    /// </summary>
    private static double CalculateRecoveryFactor(int stepsActive, int healingTime)
    {
        if (healingTime <= 0) return 1.0;

        var progress = Math.Min((double)stepsActive / healingTime, 1.0);
        return 1.0 - progress * 0.5; // Reduce debuff effect by up to 50% as healing approaches
    }

    /// <summary>
    ///     Applies severity multiplier to a debuff value.
    ///     For stress (>1.0): severity amplifies deviation from 1.0.
    ///     For mood/social (<1.0): severity amplifies deviation from 1.0.
    /// </summary>
    private static double ApplySeverityToDebuff(double debuff, double severityMultiplier, bool isStressDebuff)
    {
        var deviation = debuff - 1.0;
        var adjustedDeviation = deviation * severityMultiplier;
        return 1.0 + adjustedDeviation;
    }

    /// <summary>
    ///     Applies recovery factor to reduce debuff effect.
    /// </summary>
    private static double ApplyRecoveryToDebuff(double debuff, double recoveryFactor, bool isStressDebuff)
    {
        if (isStressDebuff)
        {
            // Stress debuffs are > 1.0, so we reduce the excess
            var excess = debuff - 1.0;
            return 1.0 + excess * recoveryFactor;
        }
        else
        {
            // Mood/social debuffs are < 1.0, so we reduce the deficit
            var deficit = 1.0 - debuff;
            return 1.0 - deficit * recoveryFactor;
        }
    }

    /// <summary>
    ///     Clears cached noise generators (call when simulation resets).
    /// </summary>
    public void Reset()
    {
        _noiseGenerators.Clear();
    }
}
