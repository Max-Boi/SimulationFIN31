using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.History;

namespace SimulationFIN31.Services.Interfaces;

/// <summary>
///     Service responsible for logging simulation history during execution
///     and providing compiled history for evaluation.
/// </summary>
public interface ISimulationHistoryService
{
    /// <summary>
    ///     Initializes/resets history for a new simulation run.
    ///     Must be called before starting a new simulation.
    /// </summary>
    void BeginNewSimulation();

    /// <summary>
    ///     Records a snapshot of the current state after a turn completes.
    /// </summary>
    /// <param name="state">The current simulation state to snapshot.</param>
    void RecordTurnSnapshot(SimulationState state);

    /// <summary>
    ///     Records a life event occurrence.
    /// </summary>
    /// <param name="lifeEvent">The event that occurred.</param>
    /// <param name="age">The avatar's age when the event occurred.</param>
    void RecordEvent(LifeEvent lifeEvent, int age);

    /// <summary>
    ///     Records a coping mechanism usage.
    /// </summary>
    /// <param name="mechanism">The coping mechanism that was used.</param>
    /// <param name="age">The avatar's age when the mechanism was used.</param>
    void RecordCopingUsage(CopingMechanism mechanism, int age);

    /// <summary>
    ///     Records an illness onset.
    /// </summary>
    /// <param name="illnessKey">Unique identifier for the illness.</param>
    /// <param name="displayName">Human-readable name of the illness.</param>
    /// <param name="onsetAge">The age when the illness started.</param>
    void RecordIllnessOnset(string illnessKey, string displayName, int onsetAge);

    /// <summary>
    ///     Records an illness healing.
    /// </summary>
    /// <param name="illnessKey">Unique identifier for the illness.</param>
    /// <param name="healedAge">The age when the illness was healed.</param>
    void RecordIllnessHealed(string illnessKey, int healedAge);

    /// <summary>
    ///     Compiles and returns the complete simulation history.
    /// </summary>
    /// <param name="finalState">The final simulation state at simulation end.</param>
    /// <returns>Complete simulation history for evaluation.</returns>
    SimulationHistory GetCompleteHistory(SimulationState finalState);
}