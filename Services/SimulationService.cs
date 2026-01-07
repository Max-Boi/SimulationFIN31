using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Data;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Services.Interfaces;
using CopingMechanism = SimulationFIN31.Models.Data.CopingMechanism;

namespace SimulationFIN31.Services;

/// <summary>
///     Service that manages the simulation loop, selecting and applying life events
///     based on the current simulation state and weighted probabilities.
/// </summary>
public sealed class SimulationService : ISimulationService
{
    private const int BASE_STEP_DELAY_MS = 2000;
    private const int MIN_STEP_DELAY_MS = 100;
    private const int MAX_STEP_DELAY_MS = 5000;
    private readonly EventEngine _eventEngine;
    private readonly ISimulationHistoryService _historyService;
    private readonly IIllnessManagerService _illnessManagerService;

    private readonly IWeightedRandomService _weightedRandomService;

    // Tracks current state for illness event handling
    private SimulationState? _currentState;

    /// <summary>
    ///     Creates a new SimulationService with required dependencies.
    /// </summary>
    /// <param name="weightedRandomService">Service for weighted random event selection.</param>
    /// <param name="illnessManagerService">Service for managing mental illness lifecycle.</param>
    /// <param name="historyService">Service for logging simulation history.</param>
    /// <exception cref="ArgumentNullException">When any required service is null.</exception>
    public SimulationService(
        IWeightedRandomService weightedRandomService,
        IIllnessManagerService illnessManagerService,
        ISimulationHistoryService historyService)
    {
        _weightedRandomService =
            weightedRandomService ?? throw new ArgumentNullException(nameof(weightedRandomService));
        _illnessManagerService =
            illnessManagerService ?? throw new ArgumentNullException(nameof(illnessManagerService));
        _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
        _eventEngine = new EventEngine();

        _illnessManagerService.IllnessChanged += OnIllnessChanged;
    }

    /// <inheritdoc />
    public event EventHandler<SimulationEventArgs>? EventOccurred;

    /// <inheritdoc />
    public event EventHandler<SimulationState>? StateUpdated;

    /// <summary>
    ///     Event raised when an illness state changes (onset or healing).
    /// </summary>
    public event EventHandler<IllnessEventArgs>? IllnessChanged;

    /// <inheritdoc />
    public async Task RunStepAsync(SimulationState state, CancellationToken cancellationToken = default, double speedMultiplier = 1.0, bool useDoubleEvents = true)
    {
        ArgumentNullException.ThrowIfNull(state);

        cancellationToken.ThrowIfCancellationRequested();

        // Store current state for illness event handling
        _currentState = state;

        var events = GetEventsForPhase(state.LifePhase);
        var copingMechanisms = CopingMechanism.AllcopingMechanisms;

        // Calculate delay between events based on speed
        var eventDelay = GetEventDelay(speedMultiplier);
        
        // Determine number of events to select per category
        var eventCount = useDoubleEvents ? 2 : 1;

        // Process generic events (1 or 2 events with delay)
        await ProcessGenericEventsAsync(events.GenericEvents, state, eventDelay, cancellationToken, eventCount);

        // Process personal events (1 or 2 events with delay)
        await ProcessPersonalEventsAsync(events.PersonalEvents, state, eventDelay, cancellationToken, eventCount);

        // Process coping mechanisms (still single selection)
        ProcessCopingMechanisms(copingMechanisms, state);

        // Process illness triggers and healing after all events
        _illnessManagerService.ProcessIllnessStep(state);

        AdvanceAge(state);

        StateUpdated?.Invoke(this, state);
    }

    /// <inheritdoc />
    public int GetStepDelay(double speedMultiplier)
    {
        if (speedMultiplier <= 0) return BASE_STEP_DELAY_MS;

        var delay = (int)(BASE_STEP_DELAY_MS / speedMultiplier);
        return Math.Clamp(delay, MIN_STEP_DELAY_MS, MAX_STEP_DELAY_MS);
    }

    /// <summary>
    ///     Gets the delay between individual events within a step, based on simulation speed.
    /// </summary>
    private int GetEventDelay(double speedMultiplier)
    {
        const int baseEventDelayMs = 800;
        const int minEventDelayMs = 50;
        const int maxEventDelayMs = 2000;

        if (speedMultiplier <= 0) return baseEventDelayMs;

        var delay = (int)(baseEventDelayMs / speedMultiplier);
        return Math.Clamp(delay, minEventDelayMs, maxEventDelayMs);
    }

    private void OnIllnessChanged(object? sender, IllnessEventArgs e)
    {
        // Record illness changes in history
        if (_currentState != null)
        {
            if (e.ChangeType == IllnessChangeType.Onset)
                _historyService.RecordIllnessOnset(e.IllnessKey, e.IllnessName, _currentState.CurrentAge);
            else if (e.ChangeType == IllnessChangeType.Healed)
                _historyService.RecordIllnessHealed(e.IllnessKey, _currentState.CurrentAge);
        }

        IllnessChanged?.Invoke(this, e);
    }

    /// <summary>
    ///     Processes generic events for the current step using SUS algorithm with configurable event count.
    /// </summary>
    private async Task ProcessGenericEventsAsync(IReadOnlyList<GenericEvent> genericEvents, SimulationState state, int eventDelay, CancellationToken cancellationToken, int eventCount)
    {
        var selectedEvents = _weightedRandomService.SelectGenericEvents(genericEvents, state, eventCount);
        
        for (int i = 0; i < selectedEvents.Count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            ApplyEvent(selectedEvents[i], state);
            
            // Add delay between events (but not after the last one)
            if (i < selectedEvents.Count - 1)
            {
                await Task.Delay(eventDelay, cancellationToken);
            }
        }
    }

    /// <summary>
    ///     Processes personal events for the current step using SUS algorithm with configurable event count.
    /// </summary>
    private async Task ProcessPersonalEventsAsync(IReadOnlyList<PersonalEvent> personalEvents, SimulationState state, int eventDelay, CancellationToken cancellationToken, int eventCount)
    {
        var selectedEvents = _weightedRandomService.SelectPersonalEvents(personalEvents, state, eventCount);
        
        for (int i = 0; i < selectedEvents.Count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var selectedEvent = selectedEvents[i];
            ApplyEvent(selectedEvent, state);
            ApplyPersonalEventEffects(selectedEvent, state);
            
            // Add delay between events (but not after the last one)
            if (i < selectedEvents.Count - 1)
            {
                await Task.Delay(eventDelay, cancellationToken);
            }
        }
    }

    /// <summary>
    ///     Processes coping mechanisms when triggers are met.
    /// </summary>
    private void ProcessCopingMechanisms(IReadOnlyList<Models.EventTypes.CopingMechanism> mechanisms,
        SimulationState state)
    {
        var selectedMechanism = _weightedRandomService.SelectCopingMechanism(mechanisms, state);
        if (selectedMechanism != null)
        {
            ApplyEvent(selectedMechanism, state);
            UpdateCopingPreferences(selectedMechanism, state);

            // Record coping mechanism usage to history
            _historyService.RecordCopingUsage(selectedMechanism, state.CurrentAge);
        }
    }

    /// <summary>
    ///     Applies a life event's effects to the simulation state and raises the EventOccurred event.
    ///     Effects are modified by any active mental illness debuffs.
    /// </summary>
    private void ApplyEvent(LifeEvent lifeEvent, SimulationState state)
    {
        // Apply debuffs from active illnesses
        var debuffedEffects = _illnessManagerService.ApplyDebuffs(state, lifeEvent);
        _eventEngine.ApplyEffectsWithDebuffs(state, debuffedEffects);

        state.EventHistory.Add(lifeEvent.Id);

        // Track traumatic events with current age for PTSD/trauma-related illness triggers
        if (lifeEvent.IsTraumatic) state.TraumaticEventAges.Add(state.CurrentAge);

        // Record event to history (excluding coping mechanisms which are recorded separately)
        if (lifeEvent.Category != EventCategory.Coping) _historyService.RecordEvent(lifeEvent, state.CurrentAge);

        EventOccurred?.Invoke(this, new SimulationEventArgs(lifeEvent, state));
    }

    /// <summary>
    ///     Applies personality-specific effects from personal events.
    /// </summary>
    private static void ApplyPersonalEventEffects(PersonalEvent personalEvent, SimulationState state)
    {
        state.AnxietyLevel = (int)Math.Clamp(state.AnxietyLevel + personalEvent.AnxietyChange, 0, 100);
    }

    /// <summary>
    ///     Updates coping preferences when a habit-forming mechanism is used.
    /// </summary>
    private static void UpdateCopingPreferences(Models.EventTypes.CopingMechanism mechanism, SimulationState state)
    {
        if (!mechanism.IsHabitForming) return;

        if (state.CopingPreferences.TryGetValue(mechanism.Id, out var currentValue))
            state.CopingPreferences[mechanism.Id] = Math.Min(currentValue + 0.1, 1.0);
        else
            state.CopingPreferences[mechanism.Id] = 0.1;
    }

    /// <summary>
    ///     Advances the simulation age and updates life phase if necessary.
    /// </summary>
    private static void AdvanceAge(SimulationState state)
    {
        state.CurrentAge++;
        state.LifePhase = DetermineLifePhase(state.CurrentAge);
    }

    /// <summary>
    ///     Determines the life phase based on current age.
    /// </summary>
    private static LifePhase DetermineLifePhase(int age)
    {
        return age switch
        {
            < 6 => LifePhase.Childhood,
            < 12 => LifePhase.SchoolBeginning,
            < 18 => LifePhase.Adolescence,
            < 24 => LifePhase.EmergingAdulthood,
            _ => LifePhase.Adulthood
        };
    }

    /// <summary>
    ///     Gets the appropriate event collections for the current life phase.
    /// </summary>
    private static PhaseEvents GetEventsForPhase(LifePhase phase)
    {
        return phase switch
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
    }

    /// <summary>
    ///     Container for phase-specific event collections.
    /// </summary>
    private readonly record struct PhaseEvents(
        IReadOnlyList<GenericEvent> GenericEvents,
        IReadOnlyList<PersonalEvent> PersonalEvents);
}