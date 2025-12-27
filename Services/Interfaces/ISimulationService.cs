using System;
using System.Threading;
using System.Threading.Tasks;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;

namespace SimulationFIN31.Services.Interfaces;

/// <summary>
/// Service responsible for running the life simulation loop.
/// Processes life phases, generates weighted events, and applies their effects to the simulation state.
/// </summary>
public interface ISimulationService
{
    /// <summary>
    /// Event raised when a life event occurs during simulation.
    /// Subscribers receive the event and updated state for UI updates.
    /// </summary>
    event EventHandler<SimulationEventArgs>? EventOccurred;

    /// <summary>
    /// Event raised when the simulation state is updated.
    /// </summary>
    event EventHandler<SimulationState>? StateUpdated;

    /// <summary>
    /// Event raised when an illness state changes (onset or healing).
    /// </summary>
    event EventHandler<IllnessEventArgs>? IllnessChanged;

    /// <summary>
    /// Runs a single simulation step, potentially triggering one or more events.
    /// </summary>
    /// <param name="state">The current simulation state to process.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Task representing the async operation.</returns>
    Task RunStepAsync(SimulationState state, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the delay in milliseconds between simulation steps based on speed multiplier.
    /// </summary>
    /// <param name="speedMultiplier">Speed multiplier (1.0 = normal, 2.0 = double speed).</param>
    /// <returns>Delay in milliseconds.</returns>
    int GetStepDelay(double speedMultiplier);
}

/// <summary>
/// Event arguments for simulation events containing the occurred event and current state.
/// </summary>
public sealed class SimulationEventArgs : EventArgs
{
    /// <summary>
    /// The life event that occurred during this simulation step.
    /// </summary>
    public LifeEvent Event { get; }

    /// <summary>
    /// The current simulation state after the event was applied.
    /// </summary>
    public SimulationState State { get; }

    /// <summary>
    /// Timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredAt { get; }

    /// <summary>
    /// Creates a new SimulationEventArgs instance.
    /// </summary>
    /// <param name="lifeEvent">The event that occurred.</param>
    /// <param name="state">The current simulation state.</param>
    public SimulationEventArgs(LifeEvent lifeEvent, SimulationState state)
    {
        Event = lifeEvent ?? throw new ArgumentNullException(nameof(lifeEvent));
        State = state ?? throw new ArgumentNullException(nameof(state));
        OccurredAt = DateTime.UtcNow;
    }
}
