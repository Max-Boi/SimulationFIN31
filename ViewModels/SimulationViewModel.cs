using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.History;
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
    private readonly ISimulationHistoryService _historyService;

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
    /// <param name="historyService">Service for logging simulation history.</param>
    /// <exception cref="ArgumentNullException">When any dependency is null.</exception>
    public SimulationViewModel(
        INavigationService navigationService,
        ISimulationService simulationService,
        ISimulationHistoryService historyService)
    {
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));
        _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));

        _simulationState = CreateInitialState();
        _historyService.BeginNewSimulation();
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
        _historyService.BeginNewSimulation();
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
    /// Auto-navigates to evaluation view when simulation completes.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the simulation.</param>
    private async Task RunSimulationLoopAsync(CancellationToken cancellationToken)
    {
        var simulationCompleted = false;

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

            // Check if simulation completed naturally (reached age 30)
            simulationCompleted = SimulationState.CurrentAge >= 30;
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

        // Navigate to evaluation view if simulation completed naturally
        if (simulationCompleted)
        {
            var history = _historyService.GetCompleteHistory(SimulationState);

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _navigationService.NavigateTo<EvaluationViewModel>(vm => vm.Initialize(history));
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
    /// Records turn snapshot and updates display properties on the UI thread.
    /// </summary>
    private void OnStateUpdated(object? sender, SimulationState state)
    {
        // Record turn snapshot for evaluation charts
        _historyService.RecordTurnSnapshot(state);

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
    /// Creates initial simulation state by loading user settings from settings.json.
    /// Falls back to sensible defaults if settings file doesn't exist.
    /// </summary>
    private static SimulationState CreateInitialState()
    {
        var settings = LoadSettings();

        return new SimulationState
        {
            CurrentAge = 0,
            LifePhase = LifePhase.Childhood,
            CurrentStress = 20,
            CurrentMood = 0,
            SocialBelonging = 50,
            ResilienceScore = 50,
            PhysicalHealth = 100,

            // Load from user settings
            IncomeLevel = EnumConverter.MapIncomeLevel(settings.IncomeLevel),
            ParentsEducationLevel = EnumConverter.MapParentsEducationLevel(settings.ParentsEducationLevel),
            JobStatus = EnumConverter.MapJobStatus(settings.JobStatus),
            SocialEnvironmentLevel = settings.SocialEnvironmentLevel,
            FamilyCloseness = settings.FamilyCloseness,
            ParentsRelationshipQuality = EnumConverter.ToParentsRelationshipQuality(settings.ParentsRelationshipQuality),
            ParentsWithAddiction = settings.ParentsWithAddiction,
            HasAdhd = settings.HasAdhd,
            HasAutism = settings.HasAutism,
            IntelligenceScore = settings.IntelligenceScore,
            AnxietyLevel = settings.AnxietyLevel,
            SocialEnergyLevel = EnumConverter.ToSocialEnergyLevel(settings.SocialEnergyLevel),
            Gender = EnumConverter.ToGenderType(settings.Gender)
        };
    }

    /// <summary>
    /// Loads simulation settings from settings.json file.
    /// Returns default settings if file doesn't exist.
    /// </summary>
    private static SimulationSettings LoadSettings()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "settings.json");

        if (!File.Exists(settingsPath))
        {
            // Return defaults matching SettingsViewModel defaults
            return new SimulationSettings
            {
                IncomeLevel = 4,
                ParentsEducationLevel = 4,
                JobStatus = 4,
                SocialEnvironmentLevel = 50,
                FamilyCloseness = 50,
                ParentsRelationshipQuality = "neutral",
                HasAdhd = false,
                HasAutism = false,
                ParentsWithAddiction = false,
                IntelligenceScore = 50,
                AnxietyLevel = 40,
                SocialEnergyLevel = "Ambivertiert",
                Gender = "Männlich"
            };
        }

        try
        {
            var json = File.ReadAllText(settingsPath);
            return JsonSerializer.Deserialize<SimulationSettings>(json)
                   ?? throw new InvalidOperationException("Settings deserialization returned null");
        }
        catch (Exception ex)
        {
            // Log error and return defaults (in production, use proper logging)
            System.Diagnostics.Debug.WriteLine($"Failed to load settings: {ex.Message}");
            return new SimulationSettings
            {
                IncomeLevel = 4,
                ParentsEducationLevel = 4,
                JobStatus = 4,
                SocialEnvironmentLevel = 50,
                FamilyCloseness = 50,
                ParentsRelationshipQuality = "neutral",
                HasAdhd = false,
                HasAutism = false,
                ParentsWithAddiction = false,
                IntelligenceScore = 50,
                AnxietyLevel = 40,
                SocialEnergyLevel = "Ambivertiert",
                Gender = "Männlich"
            };
        }
    }

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
