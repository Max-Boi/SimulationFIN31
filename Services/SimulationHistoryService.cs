using System;
using System.Collections.Generic;
using System.Linq;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.History;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
/// Service that logs simulation history during execution and compiles
/// complete history for evaluation display.
/// Thread-safe for use with background simulation execution.
/// </summary>
public sealed class SimulationHistoryService : ISimulationHistoryService
{
    private readonly object _lock = new();

    private readonly List<TurnSnapshot> _turnSnapshots = new();
    private readonly List<EventRecord> _events = new();
    private readonly List<CopingUsageRecord> _copingUsage = new();
    private readonly Dictionary<string, (string DisplayName, int OnsetAge)> _activeIllnesses = new();
    private readonly List<IllnessRecord> _completedIllnesses = new();

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
            Age: state.CurrentAge,
            Stress: state.CurrentStress,
            Mood: state.CurrentMood,
            SocialBelonging: state.SocialBelonging,
            Resilience: state.ResilienceScore,
            PhysicalHealth: state.PhysicalHealth,
            LifePhase: state.LifePhase
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
            Id: lifeEvent.Id,
            Name: lifeEvent.Name,
            Description: lifeEvent.Description,
            AgeOccurred: age,
            IsTraumatic: lifeEvent.IsTraumatic,
            StressImpact: lifeEvent.StressImpact,
            MoodImpact: lifeEvent.MoodImpact,
            SocialImpact: lifeEvent.SocialBelongingImpact,
            ResilienceImpact: lifeEvent.ResilienceImpact,
            HealthImpact: lifeEvent.HealthImpact,
            Category: lifeEvent.Category
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
            CopingId: mechanism.Id,
            Name: mechanism.Name,
            Type: mechanism.Type,
            AgeUsed: age
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
                    IllnessKey: illnessKey,
                    DisplayName: illness.DisplayName,
                    OnsetAge: illness.OnsetAge,
                    HealedAge: healedAge
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
            // Convert any still-active illnesses to records with null HealedAge
            var allIllnesses = new List<IllnessRecord>(_completedIllnesses);
            foreach (var (key, (displayName, onsetAge)) in _activeIllnesses)
            {
                allIllnesses.Add(new IllnessRecord(
                    IllnessKey: key,
                    DisplayName: displayName,
                    OnsetAge: onsetAge,
                    HealedAge: null
                ));
            }

            return new SimulationHistory(
                TurnSnapshots: _turnSnapshots.ToList().AsReadOnly(),
                Events: _events.ToList().AsReadOnly(),
                Illnesses: allIllnesses.AsReadOnly(),
                CopingUsage: _copingUsage.ToList().AsReadOnly(),
                FinalState: finalState
            );
        }
    }
}
