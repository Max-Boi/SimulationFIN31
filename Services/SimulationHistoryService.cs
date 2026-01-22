using System;
using System.Collections.Generic;
using System.Linq;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.History;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
///     Service that logs simulation history during execution and compiles
///     complete history for evaluation display.
///     Thread-safe for use with background simulation execution.
/// </summary>
public sealed class SimulationHistoryService : ISimulationHistoryService
{
    private readonly Dictionary<string, (string DisplayName, int OnsetAge)> _activeIllnesses = new();
    private readonly List<IllnessRecord> _completedIllnesses = new();
    private readonly List<CopingUsageRecord> _copingUsage = new();
    private readonly List<EventRecord> _events = new();
    private readonly object _lock = new();

    private readonly List<TurnSnapshot> _turnSnapshots = new();

    /// <inheritdoc />
    public void BeginNewSimulation()
    {
        lock (_lock)
        {
            _turnSnapshots.Clear();
            _events.Clear();
            _copingUsage.Clear();
            _activeIllnesses.Clear();
            _completedIllnesses.Clear();
        }
    }

    /// <inheritdoc />
    public void RecordTurnSnapshot(SimulationState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var snapshot = new TurnSnapshot(
            state.CurrentAge,
            state.CurrentStress,
            state.CurrentMood,
            state.SocialBelonging,
            state.ResilienceScore,
            state.PhysicalHealth,
            state.LifePhase
        );

        lock (_lock)
        {
            _turnSnapshots.Add(snapshot);
        }
    }

    /// <inheritdoc />
    public void RecordEvent(LifeEvent lifeEvent, int age)
    {
        ArgumentNullException.ThrowIfNull(lifeEvent);

        var record = new EventRecord(
            lifeEvent.Id,
            lifeEvent.Name,
            lifeEvent.Description,
            age,
            lifeEvent.IsTraumatic,
            lifeEvent.StressImpact,
            lifeEvent.MoodImpact,
            lifeEvent.SocialBelongingImpact,
            lifeEvent.ResilienceImpact,
            lifeEvent.HealthImpact,
            lifeEvent.Category
        );

        lock (_lock)
        {
            _events.Add(record);
        }
    }

    /// <inheritdoc />
    public void RecordCopingUsage(CopingMechanism mechanism, int age)
    {
        ArgumentNullException.ThrowIfNull(mechanism);

        var record = new CopingUsageRecord(
            mechanism.Id,
            mechanism.Name,
            mechanism.Type,
            age
        );

        lock (_lock)
        {
            _copingUsage.Add(record);
        }
    }

    /// <inheritdoc />
    public void RecordIllnessOnset(string illnessKey, string displayName, int onsetAge)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(illnessKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        lock (_lock)
        {
            _activeIllnesses[illnessKey] = (displayName, onsetAge);
        }
    }

    /// <inheritdoc />
    public void RecordIllnessHealed(string illnessKey, int healedAge)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(illnessKey);

        lock (_lock)
        {
            if (_activeIllnesses.TryGetValue(illnessKey, out var illness))
            {
                var record = new IllnessRecord(
                    illnessKey,
                    illness.DisplayName,
                    illness.OnsetAge,
                    healedAge
                );
                _completedIllnesses.Add(record);
                _activeIllnesses.Remove(illnessKey);
            }
        }
    }

    /// <inheritdoc />
    public SimulationHistory GetCompleteHistory(SimulationState finalState)
    {
        ArgumentNullException.ThrowIfNull(finalState);

        lock (_lock)
        {
            try
            {
                // Convert any still-active illnesses to records with null HealedAge
                var allIllnesses = new List<IllnessRecord>(_completedIllnesses);
                foreach (var (key, (displayName, onsetAge)) in _activeIllnesses)
                    allIllnesses.Add(new IllnessRecord(
                        key,
                        displayName,
                        onsetAge,
                        null
                    ));

                return new SimulationHistory(
                    _turnSnapshots.ToList().AsReadOnly(),
                    _events.ToList().AsReadOnly(),
                    allIllnesses.AsReadOnly(),
                    _copingUsage.ToList().AsReadOnly(),
                    finalState
                );
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Erstellen der Simulationshistorie: {ex.Message}");
                // Return minimal history on error
                return new SimulationHistory(
                    new List<TurnSnapshot>().AsReadOnly(),
                    new List<EventRecord>().AsReadOnly(),
                    new List<IllnessRecord>().AsReadOnly(),
                    new List<CopingUsageRecord>().AsReadOnly(),
                    finalState
                );
            }
        }
    }
}