using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;

namespace SimulationFIN31.Services.Interfaces;

/// <summary>
/// Service responsible for managing mental illness lifecycle:
/// checking triggers, tracking active illnesses, applying debuffs, and handling healing.
/// </summary>
public interface IIllnessManagerService
{
    /// <summary>
    /// Event raised when an illness is triggered or healed.
    /// </summary>
    event EventHandler<IllnessEventArgs>? IllnessChanged;

    /// <summary>
    /// Maximum number of concurrent active illnesses.
    /// </summary>
    int MaxActiveIllnesses { get; }

    /// <summary>
    /// Processes illness logic for the current simulation step.
    /// Checks triggers, rolls probabilities, handles healing.
    /// </summary>
    /// <param name="state">The current simulation state.</param>
    void ProcessIllnessStep(SimulationState state);

    /// <summary>
    /// Applies illness debuffs to event effects before they are applied.
    /// </summary>
    /// <param name="state">The current simulation state.</param>
    /// <param name="lifeEvent">The life event whose effects should be debuffed.</param>
    /// <returns>Modified effect values with debuffs applied.</returns>
    DebuffedEffects ApplyDebuffs(SimulationState state, LifeEvent lifeEvent);

    /// <summary>
    /// Gets the current active illnesses and their progression state.
    /// </summary>
    /// <param name="state">The current simulation state.</param>
    /// <returns>Read-only dictionary of illness keys to their progression states.</returns>
    IReadOnlyDictionary<string, IllnessProgressionState> GetActiveIllnessStates(SimulationState state);
}
