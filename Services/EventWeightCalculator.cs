using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
/// Calculates event weights by combining base probability with influence factors.
///
/// Weight calculation follows the formula from CLAUDE.md:
/// Event_Weight = Base_Probability × Π(Influence_Factor_i)
///
/// Where each influence factor is calculated as:
/// Influence_Factor = f(normalized_state_value, exponent)
///
/// The calculator also handles coping preferences, increasing the weight
/// of habit-forming coping mechanisms that have been used before.
/// </summary>
public sealed class EventWeightCalculator : IEventWeightCalculator
{
    private readonly IFactorNormalizer _factorNormalizer;
    private readonly IInfluenceCalculator _influenceCalculator;

    /// <summary>
    /// Minimum weight to ensure all eligible events have at least some chance.
    /// Prevents events from being completely excluded due to rounding.
    /// </summary>
    private const double MINIMUM_WHEIGHT = 0.001;

    /// <summary>
    /// Maximum weight to prevent single events from dominating selection.
    /// </summary>
    private const double MAXIMUM_WHEIGHT = 0.99;

    /// <summary>
    /// Multiplier applied to habit-forming coping mechanisms based on preference score.
    /// </summary>
    private const double HABIT_BOOST_FACTOR = 1.5;

    /// <summary>
    /// Creates a new EventWeightCalculator with required dependencies.
    /// </summary>
    /// <param name="factorNormalizer">Service to normalize state values to [0,1] range.</param>
    /// <param name="influenceCalculator">Service to calculate influence multipliers.</param>
    /// <exception cref="ArgumentNullException">When any dependency is null.</exception>
    public EventWeightCalculator(
        IFactorNormalizer factorNormalizer,
        IInfluenceCalculator influenceCalculator)
    {
        _factorNormalizer = factorNormalizer ?? throw new ArgumentNullException(nameof(factorNormalizer));
        _influenceCalculator = influenceCalculator ?? throw new ArgumentNullException(nameof(influenceCalculator));
    }

    /// <summary>
    /// Calculates the weight for a single life event based on current simulation state.
    /// </summary>
    /// <param name="lifeEvent">The event to calculate weight for.</param>
    /// <param name="state">Current simulation state providing factor values.</param>
    /// <returns>Calculated weight clamped to [0.001, 0.99] range.</returns>
    /// <exception cref="ArgumentNullException">When lifeEvent or state is null.</exception>
    public double CalculateWeight(LifeEvent lifeEvent, SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(lifeEvent);
        ArgumentNullException.ThrowIfNull(state);

        var weight = lifeEvent.BaseProbability;

        foreach (var factor in lifeEvent.InfluenceFactors)
        {
            var normalizedValue = _factorNormalizer.Normalize(state, factor.FactorName);
            var influence = _influenceCalculator.CalculateInfluence(normalizedValue, factor.Exponent);
            weight *= influence;
        }

        if (lifeEvent is CopingMechanism copingMechanism && copingMechanism.IsHabitForming)
        {
            weight *= CalculateHabitBoost(copingMechanism, state);
        }

        return Math.Clamp(weight, MINIMUM_WHEIGHT, MAXIMUM_WHEIGHT);
    }

    /// <summary>
    /// Calculates weights for all events and returns them with normalized probabilities.
    /// Normalized probabilities sum to 1.0, making them suitable for SUS selection.
    /// </summary>
    /// <param name="events">Collection of events to weight.</param>
    /// <param name="state">Current simulation state providing factor values.</param>
    /// <returns>List of weighted events with raw weights and normalized probabilities.</returns>
    /// <exception cref="ArgumentNullException">When events or state is null.</exception>
    public List<WeightedEvent> CalculateAllWeights(IEnumerable<LifeEvent> events, SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(events);
        ArgumentNullException.ThrowIfNull(state);

        var weightedEvents = new List<WeightedEvent>();
        var totalWeight = 0.0;

        foreach (var lifeEvent in events)
        {
            var weight = CalculateWeight(lifeEvent, state);
            weightedEvents.Add(new WeightedEvent(lifeEvent, weight, 0.0));
            totalWeight += weight;
        }

        if (totalWeight <= 0.0)
        {
            return weightedEvents;
        }

        var normalizedEvents = new List<WeightedEvent>(weightedEvents.Count);
        foreach (var weighted in weightedEvents)
        {
            var normalizedProbability = weighted.Weight / totalWeight;
            normalizedEvents.Add(new WeightedEvent(
                weighted.Event,
                weighted.Weight,
                normalizedProbability));
        }

        return normalizedEvents;
    }

    /// <summary>
    /// Calculates a boost multiplier for habit-forming coping mechanisms
    /// based on previous usage tracked in CopingPreferences.
    /// </summary>
    /// <param name="mechanism">The coping mechanism to evaluate.</param>
    /// <param name="state">Simulation state containing coping preferences.</param>
    /// <returns>Multiplier >= 1.0 based on previous usage.</returns>
    private double CalculateHabitBoost(CopingMechanism mechanism, SimulationState state)
    {
        if (state.CopingPreferences.TryGetValue(mechanism.Id, out var preference))
        {
            var normalizedPreference = Math.Clamp(preference / 100.0, 0.0, 1.0);
            return 1.0 + (normalizedPreference * (HABIT_BOOST_FACTOR - 1.0));
        }

        return 1.0;
    }
}