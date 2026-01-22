using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.MentalDiseases;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
///     Service that manages mental illness lifecycle including trigger checking,
///     probability-based onset, time-based healing, and debuff application.
/// </summary>
public sealed class IllnessManagerService : IIllnessManagerService
{
    private const int MaxConcurrentIllnesses = 3;
    private const int IllnessTriggerCooldown = 2;
    private const double BounceBackBonus = 10.0;

    private static readonly Dictionary<string, string> OnsetMessages = new()
    {
        ["MildDepression"] = "Avatar befindet sich nun in einer depressiven Episode",
        ["generalisierte Angststörung"] = "Avatar leidet nun unter einer generalisierten Angststörung",
        ["sozialePhobie"] = "Avatar entwickelt eine Soziale Angststörung",
        ["Panikstörung"] = "Avatar entwickelt eine Panikstörung",
        ["PTBS"] = "Avatar entwickelt eine Posttraumatische Belastungsstörung",
        ["Alkoholismus"] = "Avatar entwickelt eine Alkoholabhaengigkeit",
        ["Substanzmissbrauch"] = "Avatar entwickelt eine Substanzabhängigkeit",
        ["Magersucht"] = "Avatar entwickelt Magersucht",
        ["Bulimie"] = "Avatar entwickelt Bulimie",
        ["BingeEatingStörung"] = "Avatar entwickelt eine Binge-Eating-störung",
        ["Zwangsstörung"] = "Avatar entwickelt eine Zwangsstörung",
        ["BorderlinePersonality"] = "Avatar entwickelt eine Borderline-Persönlichkeitsstörung",
        ["DissociativeDisorder"] = "Avatar entwickelt eine dissoziative störung"
    };

    private static readonly Dictionary<string, string> HealingMessages = new()
    {
        ["MildDepression"] = "Avatar hat die depressive Episode überwunden",
        ["generalisierte Angststörung"] = "Avatar hat die Angststörung in den Griff bekommen",
        ["sozialePhobie"] = "Avatar hat die soziale Angst überwunden",
        ["Panikstörung"] = "Avatar hat die Panikstörung überwunden",
        ["PTBS"] = "Avatar hat die Posttraumatische Belastungsstörung überwunden",
        ["Alkoholismus"] = "Avatar hat die Alkoholabhaengigkeit überwunden",
        ["Substanzmissbrauch"] = "Avatar hat die Substanzabhängigkeit überwunden",
        ["Magersucht"] = "Avatar hat Magersucht überwunden",
        ["Bulimie"] = "Avatar hat Bulimie überwunden",
        ["BingeEatingStörung"] = "Avatar hat die Binge-Eating-störung überwunden",
        ["Zwangsstörung"] = "Avatar hat die Zwangsstörung in den Griff bekommen",
        ["BorderlinePersonality"] = "Avatar hat die Borderline-Persönlichkeitsstörung stabilisiert",
        ["DissociativeDisorder"] = "Avatar hat die dissoziative störung überwunden"
    };

    private readonly DebuffCalculator _debuffCalculator = new();
    private readonly Random _random = new();

    /// <inheritdoc />
    public event EventHandler<IllnessEventArgs>? IllnessChanged;

    /// <inheritdoc />
    public int MaxActiveIllnesses => MaxConcurrentIllnesses;

    /// <inheritdoc />
    public void ProcessIllnessStep(SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        try
        {
            ProcessHealing(state);
            ApplyBounceBackIfInCooldown(state);
            ProcessTriggers(state);
            IncrementActiveIllnessSteps(state);
            state.StepsSinceLastIllnessTrigger++;
        }
        catch (KeyNotFoundException ex)
        {
            System.Diagnostics.Debug.WriteLine($"Krankheit nicht gefunden: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            System.Diagnostics.Debug.WriteLine($"Ungültiger Krankheitszustand: {ex.Message}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Fehler bei der Krankheitsverarbeitung: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public DebuffedEffects ApplyDebuffs(SimulationState state, LifeEvent lifeEvent)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(lifeEvent);

        if (state.CurrentIllnesses.Count == 0)
            return CreateUnmodifiedEffects(lifeEvent);

        var (stressDebuff, moodDebuff, socialDebuff) = _debuffCalculator.CalculateCombinedDebuffs(
            state.CurrentIllnesses,
            state.IllnessProgressionStates);

        return ApplyDebuffsToEvent(lifeEvent, stressDebuff, moodDebuff, socialDebuff);
    }

    /// <inheritdoc />
    public IReadOnlyDictionary<string, IllnessProgressionState> GetActiveIllnessStates(SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(state);
        return state.IllnessProgressionStates;
    }

    #region Healing Logic

    private void ProcessHealing(SimulationState state)
    {
        var healedIllnesses = FindHealedIllnesses(state);
        foreach (var key in healedIllnesses)
            HealIllness(key, state);
    }

    private static List<string> FindHealedIllnesses(SimulationState state)
    {
        var healed = new List<string>();
        foreach (var (key, progression) in state.IllnessProgressionStates)
        {
            if (!state.CurrentIllnesses.TryGetValue(key, out var config))
                continue;

            if (progression.StepsActive >= config.HealingTime)
                healed.Add(key);
        }
        return healed;
    }

    private void HealIllness(string key, SimulationState state)
    {
        var config = state.CurrentIllnesses[key];
        state.CurrentIllnesses.Remove(key);
        state.IllnessProgressionStates.Remove(key);

        var message = HealingMessages.GetValueOrDefault(key, $"Avatar hat {config.Name} überwunden");
        RaiseIllnessChanged(key, config.Name, IllnessChangeType.Healed, message);
    }

    #endregion

    #region Trigger Logic

    private void ProcessTriggers(SimulationState state)
    {
        if (!CanTriggerNewIllness(state))
            return;

        foreach (var (key, config) in MentalIllnesses.Illnesses)
        {
            if (!ShouldConsiderIllness(key, config, state))
                continue;

            if (state.CurrentIllnesses.Count >= MaxConcurrentIllnesses)
                break;

            if (TryTriggerIllness(key, config, state))
                break; // Only trigger one illness per step
        }
    }

    private static bool CanTriggerNewIllness(SimulationState state)
    {
        return state.StepsSinceLastIllnessTrigger >= IllnessTriggerCooldown
               && state.CurrentIllnesses.Count < MaxConcurrentIllnesses;
    }

    private static bool ShouldConsiderIllness(string key, DiseaseConfig config, SimulationState state)
    {
        return !state.CurrentIllnesses.ContainsKey(key)
               && state.CurrentAge >= config.MinAge
               && IllnessTriggerChecker.CheckTriggerCondition(key, state);
    }

    private bool TryTriggerIllness(string key, DiseaseConfig config, SimulationState state)
    {
        var probability = CalculateGenderAdjustedProbability(config, state);
        if (_random.NextInt64(0, 20) < probability)
        {
            TriggerIllness(key, config, state);
            return true;
        }
        return false;
    }

    private void TriggerIllness(string key, DiseaseConfig config, SimulationState state)
    {
        var severity = RollEpisodeSeverity();
        var perlinSeed = _random.Next();

        state.CurrentIllnesses[key] = config;
        state.IllnessProgressionStates[key] = new IllnessProgressionState
        {
            IllnessKey = key,
            StepsActive = 0,
            OnsetAge = state.CurrentAge,
            Severity = severity,
            PerlinSeed = perlinSeed
        };

        state.StepsSinceLastIllnessTrigger = 0;

        var message = OnsetMessages.GetValueOrDefault(key, $"Avatar entwickelt {config.Name}");
        RaiseIllnessChanged(key, config.Name, IllnessChangeType.Onset, message);
    }

    /// <summary>
    ///     Rolls episode severity at illness onset.
    ///     50% Mild, 35% Moderate, 15% Severe.
    /// </summary>
    private EpisodeSeverity RollEpisodeSeverity()
    {
        var roll = _random.Next(100);
        return roll switch
        {
            < 50 => EpisodeSeverity.Mild,
            < 85 => EpisodeSeverity.Moderate,
            _ => EpisodeSeverity.Severe
        };
    }

    private static double CalculateGenderAdjustedProbability(DiseaseConfig config, SimulationState state)
    {
        var baseProbability = 1.0 / config.TriggerChance;
        var genderModifier = config.GetGenderModifier(state.Gender);
        return Math.Min(baseProbability * genderModifier, 1.0);
    }

    #endregion

    #region Bounce-Back Logic

    private static void ApplyBounceBackIfInCooldown(SimulationState state)
    {
        if (state.StepsSinceLastIllnessTrigger < IllnessTriggerCooldown)
            ApplyBounceBackBonus(state);
    }

    private static void ApplyBounceBackBonus(SimulationState state)
    {
        state.CurrentMood = Math.Clamp(state.CurrentMood + BounceBackBonus, -100, 100);
        state.ResilienceScore = Math.Clamp(state.ResilienceScore + BounceBackBonus, 0, 80);
        state.CurrentStress = Math.Clamp(state.CurrentStress - BounceBackBonus, 0, 100);
    }

    #endregion

    #region Debuff Application

    private static DebuffedEffects CreateUnmodifiedEffects(LifeEvent lifeEvent)
    {
        return new DebuffedEffects(
            lifeEvent.StressImpact,
            lifeEvent.MoodImpact,
            lifeEvent.SocialBelongingImpact,
            lifeEvent.ResilienceImpact,
            lifeEvent.HealthImpact);
    }

    private static DebuffedEffects ApplyDebuffsToEvent(
        LifeEvent lifeEvent,
        double stressDebuff,
        double moodDebuff,
        double socialDebuff)
    {
        var stressImpact = lifeEvent.StressImpact;
        var moodImpact = lifeEvent.MoodImpact;
        var socialImpact = lifeEvent.SocialBelongingImpact;

        // StressDebuff: amplify stress from stressful events
        if (stressImpact > 0)
            stressImpact *= stressDebuff;

        // MoodDebuff: reduce mood gains, amplify mood losses
        if (moodImpact > 0)
            moodImpact *= moodDebuff;
        else if (moodImpact < 0)
            moodImpact /= moodDebuff;

        // SocialDebuff: reduce social gains, amplify social losses
        if (socialImpact > 0)
            socialImpact *= socialDebuff;
        else if (socialImpact < 0)
            socialImpact /= socialDebuff;

        return new DebuffedEffects(
            stressImpact,
            moodImpact,
            socialImpact,
            lifeEvent.ResilienceImpact,
            lifeEvent.HealthImpact);
    }

    #endregion

    #region Utility Methods

    private static void IncrementActiveIllnessSteps(SimulationState state)
    {
        foreach (var progression in state.IllnessProgressionStates.Values)
            progression.StepsActive++;
    }

    private void RaiseIllnessChanged(string key, string name, IllnessChangeType changeType, string message)
    {
        IllnessChanged?.Invoke(this, new IllnessEventArgs
        {
            IllnessKey = key,
            IllnessName = name,
            ChangeType = changeType,
            GermanMessage = message
        });
    }

    #endregion
}