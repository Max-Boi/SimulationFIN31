using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Data;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
/// Service that manages the simulation loop, selecting and applying life events
/// based on the current simulation state and weighted probabilities.
/// </summary>
public sealed class SimulationService : ISimulationService
{
    private const int BASE_STEP_DELAY_MS = 2000;
    private const int MIN_STEP_DELAY_MS = 100;
    private const int MAX_STEP_DELAY_MS = 5000;

    private readonly IWeightedRandomService _weightedRandomService;
    private readonly EventEngine _eventEngine;

    /// <inheritdoc />
    public event EventHandler<SimulationEventArgs>? EventOccurred;

    /// <inheritdoc />
    public event EventHandler<SimulationState>? StateUpdated;

    /// <summary>
    /// Creates a new SimulationService with required dependencies.
    /// </summary>
    /// <param name="weightedRandomService">Service for weighted random event selection.</param>
    /// <exception cref="ArgumentNullException">When weightedRandomService is null.</exception>
    public SimulationService(IWeightedRandomService weightedRandomService)
    {
        _weightedRandomService = weightedRandomService ?? throw new ArgumentNullException(nameof(weightedRandomService));
        _eventEngine = new EventEngine();
    }

    /// <inheritdoc />
    public async Task RunStepAsync(SimulationState state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);

        cancellationToken.ThrowIfCancellationRequested();

        var events = GetEventsForPhase(state.LifePhase);
        var copingMechanisms = EventCatalog.AllcopingMechanisms;

        await Task.Run(() =>
        {
            ProcessGenericEvents(events.GenericEvents, state);
            ProcessPersonalEvents(events.PersonalEvents, state);
            ProcessCopingMechanisms(copingMechanisms, state);

            AdvanceAge(state);
        }, cancellationToken);

        StateUpdated?.Invoke(this, state);
    }

    /// <inheritdoc />
    public int GetStepDelay(double speedMultiplier)
    {
        if (speedMultiplier <= 0)
        {
            return BASE_STEP_DELAY_MS;
        }

        var delay = (int)(BASE_STEP_DELAY_MS / speedMultiplier);
        return Math.Clamp(delay, MIN_STEP_DELAY_MS, MAX_STEP_DELAY_MS);
    }

    /// <summary>
    /// Processes generic events for the current step.
    /// </summary>
    private void ProcessGenericEvents(IReadOnlyList<GenericEvent> genericEvents, SimulationState state)
    {
        var selectedEvent = _weightedRandomService.SelectGenericEvent(genericEvents, state);
        if (selectedEvent != null)
        {
            ApplyEvent(selectedEvent, state);
        }
    }

    /// <summary>
    /// Processes personal events for the current step.
    /// </summary>
    private void ProcessPersonalEvents(IReadOnlyList<PersonalEvent> personalEvents, SimulationState state)
    {
        var selectedEvent = _weightedRandomService.SelectPersonalEvent(personalEvents, state);
        if (selectedEvent != null)
        {
            ApplyEvent(selectedEvent, state);
            ApplyPersonalEventEffects(selectedEvent, state);
        }
    }

    /// <summary>
    /// Processes coping mechanisms when triggers are met.
    /// </summary>
    private void ProcessCopingMechanisms(IReadOnlyList<CopingMechanism> mechanisms, SimulationState state)
    {
        var selectedMechanism = _weightedRandomService.SelectCopingMechanism(mechanisms, state);
        if (selectedMechanism != null)
        {
            ApplyEvent(selectedMechanism, state);
            UpdateCopingPreferences(selectedMechanism, state);
        }
    }

    /// <summary>
    /// Applies a life event's effects to the simulation state and raises the EventOccurred event.
    /// </summary>
    private void ApplyEvent(LifeEvent lifeEvent, SimulationState state)
    {
        _eventEngine.ApplyEffects(state, lifeEvent);
        state.EventHistory.Add(lifeEvent.Id);

        EventOccurred?.Invoke(this, new SimulationEventArgs(lifeEvent, state));
    }

    /// <summary>
    /// Applies personality-specific effects from personal events.
    /// </summary>
    private static void ApplyPersonalEventEffects(PersonalEvent personalEvent, SimulationState state)
    {
        state.AnxietyLevel = (int)Math.Clamp(state.AnxietyLevel + personalEvent.AnxietyChange, 0, 100);
    }

    /// <summary>
    /// Updates coping preferences when a habit-forming mechanism is used.
    /// </summary>
    private static void UpdateCopingPreferences(CopingMechanism mechanism, SimulationState state)
    {
        if (!mechanism.IsHabitForming)
        {
            return;
        }

        if (state.CopingPreferences.TryGetValue(mechanism.Id, out var currentValue))
        {
            state.CopingPreferences[mechanism.Id] = Math.Min(currentValue + 0.1, 1.0);
        }
        else
        {
            state.CopingPreferences[mechanism.Id] = 0.1;
        }
    }

    /// <summary>
    /// Advances the simulation age and updates life phase if necessary.
    /// </summary>
    private static void AdvanceAge(SimulationState state)
    {
        state.CurrentAge++;
        state.LifePhase = DetermineLifePhase(state.CurrentAge);
    }

    /// <summary>
    /// Determines the life phase based on current age.
    /// </summary>
    private static LifePhase DetermineLifePhase(int age) => age switch
    {
        < 6 => LifePhase.Childhood,
        < 12 => LifePhase.SchoolBeginning,
        < 18 => LifePhase.Adolescence,
        < 24 => LifePhase.EmergingAdulthood,
        _ => LifePhase.Adulthood
    };

    /// <summary>
    /// Gets the appropriate event collections for the current life phase.
    /// </summary>
    private static PhaseEvents GetEventsForPhase(LifePhase phase) => phase switch
    {
        LifePhase.Childhood => new PhaseEvents(
            ChildhoodEvents.AllGenericEvents,
            ChildhoodEvents.AllPersonalEvents),
        LifePhase.SchoolBeginning => new PhaseEvents(
            SchoolBeginningEvents.AllGenericEvents,
            SchoolBeginningEvents.AllPersonalEvents),
        LifePhase.Adolescence => new PhaseEvents(
            AdolescenceEvents.AllGenericEvents,
            AdolescenceEvents.AllPersonalEvents),
        LifePhase.EmergingAdulthood => new PhaseEvents(
            EmergingAdulthoodEvents.AllGenericEvents,
            EmergingAdulthoodEvents.AllPersonalEvents),
        LifePhase.Adulthood => new PhaseEvents(
            AdulthoodEvents.AllGenericEvents,
            AdulthoodEvents.AllPersonalEvents),
        _ => new PhaseEvents(
            ChildhoodEvents.AllGenericEvents,
            ChildhoodEvents.AllPersonalEvents)
    };

    /// <summary>
    /// Container for phase-specific event collections.
    /// </summary>
    private readonly record struct PhaseEvents(
        IReadOnlyList<GenericEvent> GenericEvents,
        IReadOnlyList<PersonalEvent> PersonalEvents);
}
