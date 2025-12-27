using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.ViewModels;

/// <summary>
/// ViewModel for the simulation view, managing simulation state display,
/// event logging, and simulation control (start/pause/stop).
/// </summary>
public partial class SimulationViewModel : ViewModelBase
{
    private const double DEFAULT_SIMULATION_SPEED = 1.0;
 

    private readonly INavigationService _navigationService;
    private readonly ISimulationService _simulationService;

    private CancellationTokenSource? _simulationCts;

    #region -- Observable Properties --

    /// <summary>
    /// The current simulation state being displayed.
    /// </summary>
    [ObservableProperty]
    private SimulationState _simulationState;

    /// <summary>
    /// Current stress level (0-100).
    /// </summary>
    [ObservableProperty]
    private double _stressLevel;

    /// <summary>
    /// Current mood level (-100 to 100).
    /// </summary>
    [ObservableProperty]
    private double _moodLevel;

    /// <summary>
    /// Current social belonging level (0-100).
    /// </summary>
    [ObservableProperty]
    private double _socialLevel;

    /// <summary>
    /// Current age in the simulation.
    /// </summary>
    [ObservableProperty]
    private int _currentAge;

    /// <summary>
    /// Current life phase display name.
    /// </summary>
    [ObservableProperty]
    private string _currentLifePhase = string.Empty;

    /// <summary>
    /// Simulation speed multiplier (0.25 to 5.0).
    /// </summary>
    [ObservableProperty]
    private double _simulationSpeed = DEFAULT_SIMULATION_SPEED;

    /// <summary>
    /// Indicates whether the simulation is currently running.
    /// </summary>
    [ObservableProperty]
    private bool _isRunning;

    /// <summary>
    /// Indicates whether the simulation is paused.
    /// </summary>
    [ObservableProperty]
    private bool _isPaused;

    #endregion

    #region -- Collections --

    /// <summary>
    /// Log of events that have occurred during the simulation.
    /// </summary>
    public ObservableCollection<EventLogEntry> EventLog { get; } = new();

    #endregion

    #region -- Commands --

    /// <summary>
    /// Command to navigate back to the home view.
    /// </summary>
    public ICommand GoBackCommand { get; }

    #endregion

    /// <summary>
    /// Creates a new SimulationViewModel with required dependencies.
    /// </summary>
    /// <param name="navigationService">Service for view navigation.</param>
    /// <param name="simulationService">Service for running the simulation.</param>
    /// <exception cref="ArgumentNullException">When any dependency is null.</exception>
    public SimulationViewModel(
        INavigationService navigationService,
        ISimulationService simulationService)
    {
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));

        _simulationState = CreateInitialState();
        UpdateDisplayProperties();

        GoBackCommand = new RelayCommand(NavigateBack);

        _simulationService.EventOccurred += OnEventOccurred;
        _simulationService.StateUpdated += OnStateUpdated;
        _simulationService.IllnessChanged += OnIllnessChanged;
    }

    #region -- Simulation Control Commands --

    /// <summary>
    /// Starts or resumes the simulation.
    /// </summary>
    [RelayCommand]
    private async Task StartSimulationAsync()
    {
        if (IsRunning && !IsPaused)
        {
            return;
        }

        if (IsPaused)
        {
            IsPaused = false;
            return;
        }

        IsRunning = true;
        IsPaused = false;

        _simulationCts?.Dispose();
        _simulationCts = new CancellationTokenSource();

        await RunSimulationLoopAsync(_simulationCts.Token);
    }

    /// <summary>
    /// Pauses the currently running simulation.
    /// </summary>
    [RelayCommand]
    private void PauseSimulation()
    {
        if (!IsRunning || IsPaused)
        {
            return;
        }

        IsPaused = true;
    }

    /// <summary>
    /// Stops the simulation completely.
    /// </summary>
    [RelayCommand]
    private async Task StopSimulationAsync()
    {
        if (!IsRunning)
        {
            return;
        }

        _simulationCts?.Cancel();

        IsRunning = false;
        IsPaused = false;

        await Task.Delay(50);

        _simulationCts?.Dispose();
        _simulationCts = null;
    }

    /// <summary>
    /// Resets the simulation to initial state.
    /// </summary>
    [RelayCommand]
    private async Task ResetSimulationAsync()
    {
        await StopSimulationAsync();

        SimulationState = CreateInitialState();
        UpdateDisplayProperties();

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            EventLog.Clear();
        });
    }

    #endregion

    #region -- Simulation Loop --

    /// <summary>
    /// Main simulation loop running asynchronously.
    /// Respects pause state and simulation speed settings.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the simulation.</param>
    private async Task RunSimulationLoopAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && SimulationState.CurrentAge < 30)
            {
                if (IsPaused)
                {
                    await Task.Delay(100, cancellationToken);
                    continue;
                }

                await _simulationService.RunStepAsync(SimulationState, cancellationToken);

                var delay = _simulationService.GetStepDelay(SimulationSpeed);
                await Task.Delay(delay, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when simulation is stopped
        }
        finally
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsRunning = false;
                IsPaused = false;
            });
        }
    }

    #endregion

    #region -- Event Handlers --

    /// <summary>
    /// Handles life events occurring during simulation.
    /// Updates the event log on the UI thread.
    /// </summary>
    private void OnEventOccurred(object? sender, SimulationEventArgs e)
    {
        var entry = new EventLogEntry(
            e.Event.Name,
            e.Event.Description,
            e.State.CurrentAge,
            e.OccurredAt);

        Dispatcher.UIThread.Post(() =>
        {
            EventLog.Insert(0, entry);

            if (EventLog.Count > 150)
            {
                EventLog.RemoveAt(EventLog.Count - 1);
            }
        });
    }

    /// <summary>
    /// Handles simulation state updates.
    /// Updates display properties on the UI thread.
    /// </summary>
    private void OnStateUpdated(object? sender, SimulationState state)
    {
        Dispatcher.UIThread.Post(UpdateDisplayProperties);
    }

    /// <summary>
    /// Handles illness state changes (onset or healing).
    /// Logs German messages to the event log.
    /// </summary>
    private void OnIllnessChanged(object? sender, IllnessEventArgs e)
    {
        var entry = new EventLogEntry(
            e.ChangeType == IllnessChangeType.Onset ? "Krankheitsbeginn" : "Genesung",
            e.GermanMessage,
            SimulationState.CurrentAge,
            DateTime.UtcNow);

        Dispatcher.UIThread.Post(() =>
        {
            EventLog.Insert(0, entry);

            if (EventLog.Count > 150)
            {
                EventLog.RemoveAt(EventLog.Count - 1);
            }
        });
    }

    #endregion

    #region -- Private Methods --

    /// <summary>
    /// Updates all display properties from the current simulation state.
    /// </summary>
    private void UpdateDisplayProperties()
    {
        StressLevel = SimulationState.CurrentStress;
        MoodLevel = SimulationState.CurrentMood;
        SocialLevel = SimulationState.SocialBelonging;
        CurrentAge = SimulationState.CurrentAge;
        CurrentLifePhase = GetLifePhaseDisplayName(SimulationState.LifePhase);
    }

    /// <summary>
    /// Creates the initial simulation state with balanced starting values.
    /// </summary>
    private static SimulationState CreateInitialState() => new()
    {
        CurrentAge = 0,
        LifePhase = LifePhase.Childhood,
        CurrentStress = 20,
        CurrentMood = 0,
        SocialBelonging = 50,
        ResilienceScore = 50,
        PhysicalHealth = 100,
        IncomeLevel = IncomeLevel.Medium,
        ParentsEducationLevel = ParentsEducationLevel.Medium,
        JobStatus = JobStatus.MediumPrestige,
        SocialEnvironmentLevel = 50,
        FamilyCloseness = 60,
        ParentsRelationshipQuality = 60,
        ParentsWithAddiction = false,
        HasAdhd = false,
        HasAutism = false,
        IntelligenceScore = 100,
        AnxietyLevel = 30,
        SocialEnergyLevel = SocialEnergyLevel.Ambivert,
        Gender = GenderType.Female
    };

    /// <summary>
    /// Gets a human-readable display name for the life phase.
    /// </summary>
    private static string GetLifePhaseDisplayName(LifePhase phase) => phase switch
    {
        LifePhase.Childhood => "Kindheit",
        LifePhase.SchoolBeginning => "Schulbeginn",
        LifePhase.Adolescence => "Jugend",
        LifePhase.EmergingAdulthood => "Frühes Erwachsenenalter",
        LifePhase.Adulthood => "Erwachsenenalter",
        _ => "Unbekannt"
    };

    /// <summary>
    /// Navigates back to the home view.
    /// </summary>
    private void NavigateBack()
    {
        StopSimulationAsync().ConfigureAwait(false);
        _navigationService.NavigateTo<HomeViewModel>();
    }

    #endregion
}

/// <summary>
/// Represents a single entry in the simulation event log.
/// </summary>
/// <param name="EventName">Name of the event that occurred.</param>
/// <param name="Description">Description of what happened.</param>
/// <param name="Age">Age at which the event occurred.</param>
/// <param name="Timestamp">When the event was logged.</param>
public sealed record EventLogEntry(
    string EventName,
    string Description,
    int Age,
    DateTime Timestamp)
{
    /// <summary>
    /// Formatted display text for the event log.
    /// </summary>
    public string DisplayText => $"[Alter {Age}] {EventName}";
}
