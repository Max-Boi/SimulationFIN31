using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Services.Interfaces;

/// <summary>
///     Provides weighted random selection of events using Stochastic Universal Sampling (SUS).
///     SUS ensures proportional selection while minimizing selection bias compared to simple
///     roulette wheel selection. This algorithm is based on Baker's SUS (1987) commonly used
///     in genetic algorithms for its superior statistical properties.
/// </summary>
public interface IWeightedRandomService
{
    /// <summary>
    ///     Selects a single random generic event from the provided pool based on weighted probabilities.
    ///     Weights are calculated from base probability modified by influence factors and simulation state.
    /// </summary>
    /// <param name="events">Pool of generic events to select from.</param>
    /// <param name="state">Current simulation state used for weight calculation.</param>
    /// <returns>Selected generic event, or null if no eligible events exist.</returns>
    /// <exception cref="ArgumentNullException">When events or state is null.</exception>
    GenericEvent? SelectGenericEvent(IReadOnlyList<GenericEvent> events, SimulationState state);

    /// <summary>
    ///     Selects a single random personal event from the provided pool based on weighted probabilities.
    ///     Personal events affect personality traits and are influenced by individual characteristics.
    /// </summary>
    /// <param name="events">Pool of personal events to select from.</param>
    /// <param name="state">Current simulation state used for weight calculation.</param>
    /// <returns>Selected personal event, or null if no eligible events exist.</returns>
    /// <exception cref="ArgumentNullException">When events or state is null.</exception>
    PersonalEvent? SelectPersonalEvent(IReadOnlyList<PersonalEvent> events, SimulationState state);

    /// <summary>
    ///     Selects a single random coping mechanism from the provided pool based on weighted probabilities.
    ///     Only coping mechanisms whose triggers are satisfied will be considered for selection.
    /// </summary>
    /// <param name="mechanisms">Pool of coping mechanisms to select from.</param>
    /// <param name="state">Current simulation state used for weight calculation and trigger evaluation.</param>
    /// <returns>Selected coping mechanism, or null if no triggered mechanisms exist.</returns>
    /// <exception cref="ArgumentNullException">When mechanisms or state is null.</exception>
    CopingMechanism? SelectCopingMechanism(IReadOnlyList<CopingMechanism> mechanisms, SimulationState state);

    /// <summary>
    ///     Selects multiple events from the provided pool using Stochastic Universal Sampling.
    ///     SUS places evenly-spaced pointers on the cumulative probability distribution,
    ///     ensuring proportional representation with lower variance than repeated roulette selection.
    /// </summary>
    /// <typeparam name="TEvent">Type of life event to select.</typeparam>
    /// <param name="events">Pool of events to select from.</param>
    /// <param name="state">Current simulation state used for weight calculation.</param>
    /// <param name="count">Number of events to select.</param>
    /// <returns>List of selected events (may contain fewer items if pool is smaller than count).</returns>
    /// <exception cref="ArgumentNullException">When events or state is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">When count is less than 1.</exception>
    IReadOnlyList<TEvent> SelectMultiple<TEvent>(
        IReadOnlyList<TEvent> events,
        SimulationState state,
        int count) where TEvent : LifeEvent;

    /// <summary>
    ///     Gets the calculated weights for all provided events based on current simulation state.
    ///     Useful for debugging, visualization, or external probability analysis.
    /// </summary>
    /// <typeparam name="TEvent">Type of life event to weight.</typeparam>
    /// <param name="events">Pool of events to calculate weights for.</param>
    /// <param name="state">Current simulation state used for weight calculation.</param>
    /// <returns>List of weighted events with raw weights and normalized probabilities.</returns>
    /// <exception cref="ArgumentNullException">When events or state is null.</exception>
    IReadOnlyList<WeightedEvent> GetWeightedEvents<TEvent>(
        IReadOnlyList<TEvent> events,
        SimulationState state) where TEvent : LifeEvent;

    /// <summary>
    ///     Selects multiple generic events using Stochastic Universal Sampling.
    /// </summary>
    /// <param name="events">Pool of generic events to select from.</param>
    /// <param name="state">Current simulation state used for weight calculation.</param>
    /// <param name="count">Number of events to select.</param>
    /// <returns>List of selected generic events.</returns>
    IReadOnlyList<GenericEvent> SelectGenericEvents(IReadOnlyList<GenericEvent> events, SimulationState state, int count);

    /// <summary>
    ///     Selects multiple personal events using Stochastic Universal Sampling.
    /// </summary>
    /// <param name="events">Pool of personal events to select from.</param>
    /// <param name="state">Current simulation state used for weight calculation.</param>
    /// <param name="count">Number of events to select.</param>
    /// <returns>List of selected personal events.</returns>
    IReadOnlyList<PersonalEvent> SelectPersonalEvents(IReadOnlyList<PersonalEvent> events, SimulationState state, int count);
}