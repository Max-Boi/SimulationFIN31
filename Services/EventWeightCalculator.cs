using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;


public sealed class EventWeightCalculator : IEventWeightCalculator
{
   
    private const double MINIMUM_WHEIGHT = 0.01;
    
    private const double MAXIMUM_WHEIGHT = 0.99;
    
    private const double HABIT_BOOST_FACTOR = 1.5;

    private readonly IFactorNormalizer _factorNormalizer;
    private readonly IInfluenceCalculator _influenceCalculator;

   
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

  
    public double CalculateWeight(LifeEvent lifeEvent, SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(lifeEvent);
        ArgumentNullException.ThrowIfNull(state);

        try
        {
            var weight = lifeEvent.BaseProbability;

            foreach (var factor in lifeEvent.InfluenceFactors)
            {
                var normalizedValue = _factorNormalizer.Normalize(state, factor.FactorName);
                var influence = _influenceCalculator.CalculateInfluence(normalizedValue, factor.Exponent);
                weight *= influence;
            }

            if (lifeEvent is CopingMechanism copingMechanism && copingMechanism.IsHabitForming)
                weight *= CalculateHabitBoost(copingMechanism, state);

            return Math.Clamp(weight, MINIMUM_WHEIGHT, MAXIMUM_WHEIGHT);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Fehler bei der Gewichtungsberechnung für '{lifeEvent.Name}': {ex.Message}");
            return lifeEvent.BaseProbability; // Fallback to base probability
        }
    }

    
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

        if (totalWeight <= 0.0) return weightedEvents;

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
    ///     Calculates a boost multiplier for habit-forming coping mechanisms
    ///     based on previous usage tracked in CopingPreferences.
    /// </summary>
    /// <param name="mechanism">The coping mechanism to evaluate.</param>
    /// <param name="state">Simulation state containing coping preferences.</param>
    /// <returns>Multiplier >= 1.0 based on previous usage.</returns>
    private double CalculateHabitBoost(CopingMechanism mechanism, SimulationState state)
    {
        if (state.CopingPreferences.TryGetValue(mechanism.Id, out var preference))
        {
            var normalizedPreference = Math.Clamp(preference / 100.0, 0.0, 1.0);
            return 1.0 + normalizedPreference * (HABIT_BOOST_FACTOR - 1.0);
        }

        return 1.0;
    }
}