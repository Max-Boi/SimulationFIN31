using System;

namespace SimulationFIN31.Models;

/// <summary>
///     Event arguments for illness state changes (onset or healing).
/// </summary>
public sealed class IllnessEventArgs : EventArgs
{
    /// <summary>
    ///     The illness key (e.g., "MildDepression").
    /// </summary>
    public required string IllnessKey { get; init; }

    /// <summary>
    ///     The display name of the illness (e.g., "Depressive Episode").
    /// </summary>
    public required string IllnessName { get; init; }

    /// <summary>
    ///     The type of change that occurred.
    /// </summary>
    public required IllnessChangeType ChangeType { get; init; }

    /// <summary>
    ///     The German message to display in the log.
    /// </summary>
    public required string GermanMessage { get; init; }
}

/// <summary>
///     Represents the type of illness state change.
/// </summary>
public enum IllnessChangeType
{
    /// <summary>
    ///     The illness has just been triggered.
    /// </summary>
    Onset,

    /// <summary>
    ///     The illness has been healed.
    /// </summary>
    Healed
}