using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
/// Implements weighted random selection using Stochastic Universal Sampling (SUS).
///
/// SUS Algorithm (Baker, 1987):
/// Unlike simple roulette wheel selection which samples independently for each selection,
/// SUS places evenly-spaced pointers on the cumulative probability distribution.
/// This ensures:
/// 1. Lower variance - Selected items more closely match expected proportions
/// 2. No bias - Each individual has exactly proportional selection chance
/// 3. O(n) complexity - Single pass through population for multiple selections
///
/// For single selection, SUS reduces to a single random pointer, equivalent to
/// fitness-proportionate selection but integrated with the multi-select capability.
///
/// Mathematical foundation:
/// Given population P with weights w_i, cumulative probabilities c_i = Σ(w_j)/Σ(w_k),
/// and n selections with spacing d = 1/n and random start r ∈ [0, d):
/// Select individual i where c_{i-1} &lt; (r + j*d) ≤ c_i for each pointer j.
/// </summary>
public sealed class WeightedRandomService : IWeightedRandomService, IEventSelector
{
    private readonly IEventWeightCalculator _weightCalculator;
    private readonly ICopingTriggerChecker _triggerChecker;
    private readonly Random _random;

    /// <summary>
    /// Creates a new WeightedRandomService with required dependencies.
    /// </summary>
    /// <param name="weightCalculator">Service to calculate event weights.</param>
    /// <param name="triggerChecker">Service to check coping mechanism triggers.</param>
    /// <exception cref="ArgumentNullException">When any dependency is null.</exception>
    public WeightedRandomService(
        IEventWeightCalculator weightCalculator,
        ICopingTriggerChecker triggerChecker)
    {
        _weightCalculator = weightCalculator ?? throw new ArgumentNullException(nameof(weightCalculator));
        _triggerChecker = triggerChecker ?? throw new ArgumentNullException(nameof(triggerChecker));
        _random = new Random();
    }

    /// <summary>
    /// Creates a new WeightedRandomService with a specific random seed for deterministic testing.
    /// </summary>
    /// <param name="weightCalculator">Service to calculate event weights.</param>
    /// <param name="triggerChecker">Service to check coping mechanism triggers.</param>
    /// <param name="randomSeed">Seed for the random number generator.</param>
    /// <exception cref="ArgumentNullException">When any dependency is null.</exception>
    public WeightedRandomService(
        IEventWeightCalculator weightCalculator,
        ICopingTriggerChecker triggerChecker,
        int randomSeed)
    {
        _weightCalculator = weightCalculator ?? throw new ArgumentNullException(nameof(weightCalculator));
        _triggerChecker = triggerChecker ?? throw new ArgumentNullException(nameof(triggerChecker));
        _random = new Random(randomSeed);
    }

    /// <inheritdoc />
    public GenericEvent? SelectGenericEvent(IReadOnlyList<GenericEvent> events, SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(events);
        ArgumentNullException.ThrowIfNull(state);

        var eligible = FilterEligible(events, state);
        if (eligible.Count == 0)
        {
            return null;
        }

        return SelectSingleUsingSus(eligible, state) as GenericEvent;
    }

    /// <inheritdoc />
    public PersonalEvent? SelectPersonalEvent(IReadOnlyList<PersonalEvent> events, SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(events);
        ArgumentNullException.ThrowIfNull(state);

        var eligible = FilterEligible(events, state);
        if (eligible.Count == 0)
        {
            return null;
        }

        return SelectSingleUsingSus(eligible, state) as PersonalEvent;
    }

    /// <inheritdoc />
    public CopingMechanism? SelectCopingMechanism(IReadOnlyList<CopingMechanism> mechanisms, SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(mechanisms);
        ArgumentNullException.ThrowIfNull(state);

        var triggered = _triggerChecker.FilterTriggered(mechanisms, state);
        if (triggered.Count == 0)
        {
            return null;
        }

        return SelectSingleUsingSus(triggered, state) as CopingMechanism;
    }

    /// <inheritdoc />
    public IReadOnlyList<TEvent> SelectMultiple<TEvent>(
        IReadOnlyList<TEvent> events,
        SimulationState state,
        int count) where TEvent : LifeEvent
    {
        ArgumentNullException.ThrowIfNull(events);
        ArgumentNullException.ThrowIfNull(state);

        if (count < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be at least 1.");
        }

        var eligible = FilterEligible(events, state);
        if (eligible.Count == 0)
        {
            return Array.Empty<TEvent>();
        }

        var actualCount = Math.Min(count, eligible.Count);
        return SelectMultipleUsingSus(eligible, state, actualCount);
    }

    /// <inheritdoc />
    public IReadOnlyList<WeightedEvent> GetWeightedEvents<TEvent>(
        IReadOnlyList<TEvent> events,
        SimulationState state) where TEvent : LifeEvent
    {
        ArgumentNullException.ThrowIfNull(events);
        ArgumentNullException.ThrowIfNull(state);

        var eligible = FilterEligible(events, state);
        return _weightCalculator.CalculateAllWeights(eligible, state);
    }

    /// <summary>
    /// Implementation of IEventSelector for compatibility with existing interfaces.
    /// </summary>
    /// <param name="weightedEvents">Pre-calculated weighted events.</param>
    /// <returns>Selected event or null if list is empty.</returns>
    public LifeEvent? SelectEvent(List<WeightedEvent> weightedEvents)
    {
        if (weightedEvents.Count == 0)
        {
            return null;
        }

        var totalProbability = 0.0;
        foreach (var weighted in weightedEvents)
        {
            totalProbability += weighted.NormalizedProbability;
        }

        if (totalProbability <= 0.0)
        {
            return null;
        }

        var pointer = _random.NextDouble() * totalProbability;
        var cumulative = 0.0;

        foreach (var weighted in weightedEvents)
        {
            cumulative += weighted.NormalizedProbability;
            if (pointer <= cumulative)
            {
                return weighted.Event;
            }
        }

        return weightedEvents[^1].Event;
    }

    /// <summary>
    /// Filters events to only those that can occur in the current state.
    /// </summary>
    /// <typeparam name="TEvent">Type of life event.</typeparam>
    /// <param name="events">Events to filter.</param>
    /// <param name="state">Current simulation state.</param>
    /// <returns>List of eligible events.</returns>
    private static List<TEvent> FilterEligible<TEvent>(IReadOnlyList<TEvent> events, SimulationState state)
        where TEvent : LifeEvent
    {
        var eligible = new List<TEvent>();

        foreach (var lifeEvent in events)
        {
            if (lifeEvent.CanOccur(state))
            {
                eligible.Add(lifeEvent);
            }
        }

        return eligible;
    }

    /// <summary>
    /// Selects a single event using Stochastic Universal Sampling with one pointer.
    /// </summary>
    /// <typeparam name="TEvent">Type of life event.</typeparam>
    /// <param name="eligible">Eligible events to select from.</param>
    /// <param name="state">Current simulation state.</param>
    /// <returns>Selected event.</returns>
    private LifeEvent SelectSingleUsingSus<TEvent>(List<TEvent> eligible, SimulationState state)
        where TEvent : LifeEvent
    {
        var weightedEvents = _weightCalculator.CalculateAllWeights(eligible, state);

        var cumulativeProbabilities = BuildCumulativeProbabilities(weightedEvents);
        var totalProbability = cumulativeProbabilities[^1];

        if (totalProbability <= 0.0)
        {
            return eligible[_random.Next(eligible.Count)];
        }

        var pointer = _random.NextDouble() * totalProbability;

        return SelectAtPointer(weightedEvents, cumulativeProbabilities, pointer);
    }

    /// <summary>
    /// Selects multiple events using Stochastic Universal Sampling.
    ///
    /// Algorithm:
    /// 1. Build cumulative probability distribution
    /// 2. Calculate pointer spacing = total / count
    /// 3. Generate random start point in [0, spacing)
    /// 4. Place evenly-spaced pointers starting from start point
    /// 5. Select event at each pointer position
    /// </summary>
    /// <typeparam name="TEvent">Type of life event.</typeparam>
    /// <param name="eligible">Eligible events to select from.</param>
    /// <param name="state">Current simulation state.</param>
    /// <param name="count">Number of events to select.</param>
    /// <returns>List of selected events.</returns>
    private List<TEvent> SelectMultipleUsingSus<TEvent>(List<TEvent> eligible, SimulationState state, int count)
        where TEvent : LifeEvent
    {
        var weightedEvents = _weightCalculator.CalculateAllWeights(eligible, state);
        var cumulativeProbabilities = BuildCumulativeProbabilities(weightedEvents);
        var totalProbability = cumulativeProbabilities[^1];

        if (totalProbability <= 0.0)
        {
            return SelectRandomSubset(eligible, count);
        }

        var pointerSpacing = totalProbability / count;
        var startPointer = _random.NextDouble() * pointerSpacing;

        var selected = new List<TEvent>(count);

        for (var i = 0; i < count; i++)
        {
            var pointer = startPointer + (i * pointerSpacing);

            if (pointer >= totalProbability)
            {
                pointer -= totalProbability;
            }

            var selectedEvent = SelectAtPointer(weightedEvents, cumulativeProbabilities, pointer);

            if (selectedEvent is TEvent typedEvent)
            {
                selected.Add(typedEvent);
            }
        }

        return selected;
    }

    /// <summary>
    /// Builds cumulative probability array for efficient pointer lookup.
    /// </summary>
    /// <param name="weightedEvents">Events with normalized probabilities.</param>
    /// <returns>Array of cumulative probabilities.</returns>
    private static double[] BuildCumulativeProbabilities(List<WeightedEvent> weightedEvents)
    {
        var cumulative = new double[weightedEvents.Count];
        var runningTotal = 0.0;

        for (var i = 0; i < weightedEvents.Count; i++)
        {
            runningTotal += weightedEvents[i].NormalizedProbability;
            cumulative[i] = runningTotal;
        }

        return cumulative;
    }

    /// <summary>
    /// Binary searches for the event at a specific pointer position in the cumulative distribution.
    /// </summary>
    /// <param name="weightedEvents">Events with weights.</param>
    /// <param name="cumulativeProbabilities">Cumulative probability distribution.</param>
    /// <param name="pointer">Pointer position [0, total probability).</param>
    /// <returns>Event at the pointer position.</returns>
    private static LifeEvent SelectAtPointer(
        List<WeightedEvent> weightedEvents,
        double[] cumulativeProbabilities,
        double pointer)
    {
        var left = 0;
        var right = cumulativeProbabilities.Length - 1;

        while (left < right)
        {
            var mid = left + ((right - left) / 2);

            if (cumulativeProbabilities[mid] < pointer)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return weightedEvents[left].Event;
    }

    /// <summary>
    /// Fallback selection when all probabilities are zero.
    /// Selects a random subset without replacement.
    /// </summary>
    /// <typeparam name="TEvent">Type of life event.</typeparam>
    /// <param name="eligible">Events to select from.</param>
    /// <param name="count">Number to select.</param>
    /// <returns>Random subset of events.</returns>
    private List<TEvent> SelectRandomSubset<TEvent>(List<TEvent> eligible, int count) where TEvent : LifeEvent
    {
        var shuffled = new List<TEvent>(eligible);

        for (var i = shuffled.Count - 1; i > 0; i--)
        {
            var j = _random.Next(i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }

        return shuffled.GetRange(0, Math.Min(count, shuffled.Count));
    }
}