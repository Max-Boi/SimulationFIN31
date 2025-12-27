using System;
using System.Collections.Generic;
using System.Linq;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.MentalDiseases;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
/// Service that manages mental illness lifecycle including trigger checking,
/// probability-based onset, time-based healing, and debuff application.
/// </summary>
public sealed class IllnessManagerService : IIllnessManagerService
{
    private const int MAX_ACTIVE_ILLNESSES = 3;
    private const int ILLNESS_TRIGGER_COOLDOWN = 2;
    private const double MAX_STRESS_DEBUFF = 2.5;
    private const double MIN_MOOD_DEBUFF = 0.3;
    private const double MIN_SOCIAL_DEBUFF = 0.3;

    private static readonly Dictionary<string, string> GermanOnsetMessages = new()
    {
        ["MildDepression"] = "Avatar befindet sich nun in einer depressiven Episode",
        ["GeneralizedAnxiety"] = "Avatar leidet nun unter einer generalisierten Angststoerung",
        ["SocialPhobia"] = "Avatar entwickelt eine Soziale Angststoerung",
        ["PanicDisorder"] = "Avatar entwickelt eine Panikstörung",
        ["PTSD"] = "Avatar entwickelt eine Posttraumatische Belastungsstoerung",
        ["Alcoholism"] = "Avatar entwickelt eine Alkoholabhaengigkeit",
        ["SubstanceAbuse"] = "Avatar entwickelt eine Substanzabhaengigkeit",
        ["AnorexiaNervosa"] = "Avatar entwickelt Anorexia Nervosa",
        ["BulimiaNervosa"] = "Avatar entwickelt Bulimia Nervosa",
        ["BingeEatingDisorder"] = "Avatar entwickelt eine Binge-Eating-Stoerung",
        ["OCD"] = "Avatar entwickelt eine Zwangsstoerung",
        ["BorderlinePersonality"] = "Avatar entwickelt eine Borderline-Persoenlichkeitsstoerung",
        ["AvoidantPersonality"] = "Avatar entwickelt eine aengstlich-vermeidende Persoenlichkeitsstoerung",
        ["DissociativeDisorder"] = "Avatar entwickelt eine dissoziative Stoerung"
    };

    private static readonly Dictionary<string, string> GermanHealingMessages = new()
    {
        ["MildDepression"] = "Avatar hat die depressive Episode ueberwunden",
        ["GeneralizedAnxiety"] = "Avatar hat die Angststoerung in den Griff bekommen",
        ["SocialPhobia"] = "Avatar hat die soziale Angst ueberwunden",
        ["PanicDisorder"] = "Avatar hat die Panikstörung ueberwunden",
        ["PTSD"] = "Avatar hat die Posttraumatische Belastungsstoerung ueberwunden",
        ["Alcoholism"] = "Avatar hat die Alkoholabhaengigkeit ueberwunden",
        ["SubstanceAbuse"] = "Avatar hat die Substanzabhaengigkeit ueberwunden",
        ["AnorexiaNervosa"] = "Avatar hat Anorexia Nervosa ueberwunden",
        ["BulimiaNervosa"] = "Avatar hat Bulimia Nervosa ueberwunden",
        ["BingeEatingDisorder"] = "Avatar hat die Binge-Eating-Stoerung ueberwunden",
        ["OCD"] = "Avatar hat die Zwangsstoerung in den Griff bekommen",
        ["BorderlinePersonality"] = "Avatar hat die Borderline-Persoenlichkeitsstoerung stabilisiert",
        ["AvoidantPersonality"] = "Avatar hat die vermeidende Persoenlichkeitsstoerung ueberwunden",
        ["DissociativeDisorder"] = "Avatar hat die dissoziative Stoerung ueberwunden"
    };

    private readonly Random _random = new();

    /// <inheritdoc />
    public event EventHandler<IllnessEventArgs>? IllnessChanged;

    /// <inheritdoc />
    public int MaxActiveIllnesses => MAX_ACTIVE_ILLNESSES;

    /// <inheritdoc />
    public void ProcessIllnessStep(SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        ProcessHealing(state);

        // Apply bounce-back bonus during cooldown period
        if (state.StepsSinceLastIllnessTrigger < ILLNESS_TRIGGER_COOLDOWN)
        {
            ApplyBounceBackBonus(state);
        }

        ProcessTriggers(state);
        IncrementActiveIllnessSteps(state);

        // Increment cooldown counter each step
        state.StepsSinceLastIllnessTrigger++;
    }

    /// <summary>
    /// Applies a flat positive bonus to state metrics during illness trigger cooldown,
    /// signaling a bounce-back recovery period.
    /// </summary>
    private static void ApplyBounceBackBonus(SimulationState state)
    {
        const double bounceBackBonus = 10.0;

        state.CurrentMood = Math.Clamp(state.CurrentMood + bounceBackBonus, -100, 100);
        state.ResilienceScore = Math.Clamp(state.ResilienceScore + bounceBackBonus, 0, 100);
        state.CurrentStress = Math.Clamp(state.CurrentStress - bounceBackBonus, 0, 100);
    }

    /// <inheritdoc />
    public DebuffedEffects ApplyDebuffs(SimulationState state, LifeEvent lifeEvent)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(lifeEvent);

        if (state.CurrentIllnesses.Count == 0)
        {
            return new DebuffedEffects(
                lifeEvent.StressImpact,
                lifeEvent.MoodImpact,
                lifeEvent.SocialBelongingImpact,
                lifeEvent.ResilienceImpact,
                lifeEvent.HealthImpact
            );
        }

        var (stressDebuff, moodDebuff, socialDebuff) = CalculateCombinedDebuffs(state);

        var stressImpact = lifeEvent.StressImpact;
        var moodImpact = lifeEvent.MoodImpact;
        var socialImpact = lifeEvent.SocialBelongingImpact;

        // StressDebuff: amplify stress from stressful events
        if (stressImpact > 0)
        {
            stressImpact *= stressDebuff;
        }

        // MoodDebuff: reduce mood gains, amplify mood losses
        if (moodImpact > 0)
        {
            moodImpact *= moodDebuff;
        }
        else if (moodImpact < 0)
        {
            moodImpact /= moodDebuff;
        }

        // SocialProximityDebuff: reduce social gains, amplify social losses
        if (socialImpact > 0)
        {
            socialImpact *= socialDebuff;
        }
        else if (socialImpact < 0)
        {
            socialImpact /= socialDebuff;
        }

        return new DebuffedEffects(
            stressImpact,
            moodImpact,
            socialImpact,
            lifeEvent.ResilienceImpact,
            lifeEvent.HealthImpact
        );
    }

    /// <inheritdoc />
    public IReadOnlyDictionary<string, IllnessProgressionState> GetActiveIllnessStates(SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(state);
        return state.IllnessProgressionStates;
    }

    private void ProcessHealing(SimulationState state)
    {
        var healedIllnesses = new List<string>();

        foreach (var (key, progressionState) in state.IllnessProgressionStates)
        {
            if (!state.CurrentIllnesses.TryGetValue(key, out var config))
            {
                continue;
            }

            if (progressionState.StepsActive >= config.HealingTime)
            {
                healedIllnesses.Add(key);
            }
        }

        foreach (var key in healedIllnesses)
        {
            var config = state.CurrentIllnesses[key];
            state.CurrentIllnesses.Remove(key);
            state.IllnessProgressionStates.Remove(key);

            var message = GermanHealingMessages.GetValueOrDefault(key, $"Avatar hat {config.Name} ueberwunden");
            RaiseIllnessChanged(key, config.Name, IllnessChangeType.Healed, message);
        }
    }

    private void ProcessTriggers(SimulationState state)
    {
        // Check cooldown - must wait at least 2 steps between illness triggers
        if (state.StepsSinceLastIllnessTrigger < ILLNESS_TRIGGER_COOLDOWN)
        {
            return;
        }

        if (state.CurrentIllnesses.Count >= MAX_ACTIVE_ILLNESSES)
        {
            return;
        }

        foreach (var (key, config) in MentalIllnesses.Illnesses)
        {
            if (state.CurrentIllnesses.ContainsKey(key))
            {
                continue;
            }

            if (state.CurrentIllnesses.Count >= MAX_ACTIVE_ILLNESSES)
            {
                break;
            }

            // Check minimum age requirement
            if (state.CurrentAge < config.MinAge)
            {
                continue;
            }

            if (!CheckTriggerCondition(key, state))
            {
                continue;
            }

            // Probability-based onset: 1 / TriggerChance chance each step
            var probability = 1.0 / config.TriggerChance;
            if (_random.NextDouble() < probability)
            {
                TriggerIllness(key, config, state);
            }
        }
    }

    private bool CheckTriggerCondition(string illnessKey, SimulationState state)
    {
        return illnessKey switch
        {
            "MildDepression" => state.CurrentStress > state.ResilienceScore,

            "GeneralizedAnxiety" => state.AnxietyLevel > 60 && state.CurrentStress > 50,

            "SocialPhobia" => state.SocialBelonging < 40 && state.AnxietyLevel > 50,

            "PanicDisorder" => state.AnxietyLevel > 70 && state.CurrentStress > 70,

            "PTSD" => HasRecentTraumaticEvent(state, yearsBack: 2),

            "Alcoholism" => GetCopingPreference(state, "coping_substance_use") > 0.5,

            "SubstanceAbuse" => GetCopingPreference(state, "coping_substance_use") > 0.3
                                && state.ParentsWithAddiction,

            "AnorexiaNervosa" => state.CurrentStress > 70 && state.CurrentMood < -60,

            "BulimiaNervosa" => GetCopingPreference(state, "coping_emotional_eating") > 0.5
                               && state.CurrentMood < -20,

            "BingeEatingDisorder" => GetCopingPreference(state, "coping_emotional_eating") > 0.5
                                     && state.CurrentStress > 50,

            "OCD" => state.AnxietyLevel > 60 && state.CurrentStress > 60,

            "BorderlinePersonality" => state.FamilyCloseness < 30
                                       && state.CurrentMood < -40
                                       && state.LifePhase >= LifePhase.Adolescence,

            "AvoidantPersonality" => state.SocialBelonging < 30
                                     && state.AnxietyLevel > 60
                                     && GetCopingPreference(state, "coping_avoidance") > 0.4,

            "DissociativeDisorder" => HasLifetimeTrauma(state) && state.CurrentStress > 80,

            _ => false
        };
    }

    private void TriggerIllness(string key, DiseaseConfig config, SimulationState state)
    {
        state.CurrentIllnesses[key] = config;
        state.IllnessProgressionStates[key] = new IllnessProgressionState
        {
            IllnessKey = key,
            StepsActive = 0,
            OnsetAge = state.CurrentAge
        };

        // Reset cooldown counter to prevent rapid illness triggers
        state.StepsSinceLastIllnessTrigger = 0;

        var message = GermanOnsetMessages.GetValueOrDefault(key, $"Avatar entwickelt {config.Name}");
        RaiseIllnessChanged(key, config.Name, IllnessChangeType.Onset, message);
    }

    private void IncrementActiveIllnessSteps(SimulationState state)
    {
        foreach (var progressionState in state.IllnessProgressionStates.Values)
        {
            progressionState.StepsActive++;
        }
    }

    private (double stressDebuff, double moodDebuff, double socialDebuff) CalculateCombinedDebuffs(SimulationState state)
    {
        var stressDebuff = 1.0;
        var moodDebuff = 1.0;
        var socialDebuff = 1.0;

        foreach (var config in state.CurrentIllnesses.Values)
        {
            stressDebuff *= config.StressDebuff;
            moodDebuff *= config.MoodDebuff;
            socialDebuff *= config.SocialProximityDebuff;
        }

        // Cap at reasonable maximums
        stressDebuff = Math.Min(stressDebuff, MAX_STRESS_DEBUFF);
        moodDebuff = Math.Max(moodDebuff, MIN_MOOD_DEBUFF);
        socialDebuff = Math.Max(socialDebuff, MIN_SOCIAL_DEBUFF);

        return (stressDebuff, moodDebuff, socialDebuff);
    }

    /// <summary>
    /// Checks if a traumatic event occurred within the last N years.
    /// Used for PTSD trigger which requires recent trauma exposure.
    /// </summary>
    /// <param name="state">Current simulation state.</param>
    /// <param name="yearsBack">Number of years to look back (default 2).</param>
    /// <returns>True if a traumatic event occurred within the specified timeframe.</returns>
    private static bool HasRecentTraumaticEvent(SimulationState state, int yearsBack = 2)
    {
        if (state.TraumaticEventAges.Count == 0)
            return false;

        var recentThreshold = state.CurrentAge - yearsBack;
        return state.TraumaticEventAges.Any(age => age >= recentThreshold);
    }

    /// <summary>
    /// Checks if any traumatic event occurred in the person's lifetime.
    /// Used for disorders that can develop from childhood trauma regardless of recency.
    /// </summary>
    /// <param name="state">Current simulation state.</param>
    /// <returns>True if at least one traumatic event occurred.</returns>
    private static bool HasLifetimeTrauma(SimulationState state)
    {
        return state.TraumaticEventAges.Count > 0;
    }

    private static double GetCopingPreference(SimulationState state, string copingId)
    {
        return state.CopingPreferences.GetValueOrDefault(copingId, 0.0);
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
}
