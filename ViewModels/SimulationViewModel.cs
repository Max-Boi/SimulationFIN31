using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Svg.Skia;
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
    private const double BASE_ANIMATION_INTERVAL_MS = 400.0;
    private const double MIN_ANIMATION_INTERVAL_MS = 80.0;

    private readonly INavigationService _navigationService;
    private readonly ISimulationService _simulationService;
    private readonly ISimulationHistoryService _historyService;

    private CancellationTokenSource? _simulationCts;
    private DispatcherTimer? _animationTimer;
    private int _animationFrame;

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

    /// <summary>
    /// Current character SVG path based on life phase, illness count, and animation frame.
    /// </summary>
    [ObservableProperty]
    private string _currentTreePath = "avares://SimulationFIN31/Assets/TreeVectors/Baby.svg";

    /// <summary>
    /// Key that changes when tree should transition (triggers CrossFade animation).
    /// </summary>
    [ObservableProperty]
    private string _currentLifePhaseKey = "Childhood_0";

    /// <summary>
    /// Current tree image for display, loaded from SVG.
    /// </summary>
    [ObservableProperty]
    private IImage? _currentTreeImage;

    /// <summary>
    /// Most recent generic event for icon display.
    /// </summary>
    [ObservableProperty]
    private EventLogEntry? _latestGenericEvent;

    /// <summary>
    /// Most recent personal event for icon display.
    /// </summary>
    [ObservableProperty]
    private EventLogEntry? _latestPersonalEvent;

    /// <summary>
    /// Most recent coping event for icon display.
    /// </summary>
    [ObservableProperty]
    private EventLogEntry? _latestCopingEvent;

    #endregion

    #region -- Collections --

    /// <summary>
    /// Names of currently active mental illnesses.
    /// </summary>
    public ObservableCollection<string> ActiveIllnessNames { get; } = new();

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

        InitializeAnimationTimer();
    }

    /// <summary>
    /// Initializes the animation timer for character sprite cycling.
    /// </summary>
    private void InitializeAnimationTimer()
    {
        _animationTimer = new DispatcherTimer
        {
            Interval = CalculateAnimationInterval(SimulationSpeed)
        };
        _animationTimer.Tick += OnAnimationTick;
    }

    /// <summary>
    /// Calculates the animation frame interval based on simulation speed.
    /// Higher speed = faster animation.
    /// </summary>
    /// <param name="speed">The current simulation speed multiplier.</param>
    /// <returns>TimeSpan for the animation interval.</returns>
    private static TimeSpan CalculateAnimationInterval(double speed)
    {
        var adjustedMs = BASE_ANIMATION_INTERVAL_MS / speed;
        return TimeSpan.FromMilliseconds(Math.Max(adjustedMs, MIN_ANIMATION_INTERVAL_MS));
    }

    /// <summary>
    /// Handles animation timer tick - toggles between animation frames.
    /// </summary>
    private void OnAnimationTick(object? sender, EventArgs e)
    {
        _animationFrame = _animationFrame == 1 ? 2 : 1;
        UpdateAnimatedSprite();
    }

    /// <summary>
    /// Updates only the sprite image without triggering phase transition effects.
    /// </summary>
    private void UpdateAnimatedSprite()
    {
        var illnessCount = SimulationState.CurrentIllnesses.Count;
        var newPath = GetTreePath(SimulationState.LifePhase, illnessCount, _animationFrame);

        if (CurrentTreePath != newPath)
        {
            CurrentTreePath = newPath;
            LoadTreeImage(newPath);
        }
    }

    /// <summary>
    /// Starts or stops the animation based on simulation running state.
    /// </summary>
    private void UpdateAnimationState()
    {
        if (IsRunning && !IsPaused)
        {
            _animationFrame = 1;
            _animationTimer?.Start();
        }
        else
        {
            _animationTimer?.Stop();
            _animationFrame = 0;
            UpdateAnimatedSprite();
        }
    }

    /// <summary>
    /// Called when SimulationSpeed property changes.
    /// Updates the animation timer interval accordingly.
    /// </summary>
    partial void OnSimulationSpeedChanged(double value)
    {
        if (_animationTimer != null)
        {
            _animationTimer.Interval = CalculateAnimationInterval(value);
        }
    }

    /// <summary>
    /// Called when IsRunning property changes.
    /// </summary>
    partial void OnIsRunningChanged(bool value)
    {
        UpdateAnimationState();
    }

    /// <summary>
    /// Called when IsPaused property changes.
    /// </summary>
    partial void OnIsPausedChanged(bool value)
    {
        UpdateAnimationState();
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
            LatestGenericEvent = null;
            LatestPersonalEvent = null;
            LatestCopingEvent = null;
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
    /// Updates the event log and latest event icons on the UI thread.
    /// </summary>
    private void OnEventOccurred(object? sender, SimulationEventArgs e)
    {
        var entry = new EventLogEntry(
            e.Event.Name,
            e.Event.Description,
            e.State.CurrentAge,
            e.OccurredAt,
            e.Event.Category,
            e.Event.VisualCategory);

        Dispatcher.UIThread.Post(() =>
        {
            EventLog.Insert(0, entry);

            if (EventLog.Count > 150)
            {
                EventLog.RemoveAt(EventLog.Count - 1);
            }

            // Update latest event per category for icon display
            switch (entry.Category)
            {
                case EventCategory.Generic:
                    LatestGenericEvent = entry;
                    break;
                case EventCategory.Personal:
                    LatestPersonalEvent = entry;
                    break;
                case EventCategory.Coping:
                    LatestCopingEvent = entry;
                    break;
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
    /// Logs German messages to the event log and updates illness names display.
    /// </summary>
    private void OnIllnessChanged(object? sender, IllnessEventArgs e)
    {
        var entry = new EventLogEntry(
            e.ChangeType == IllnessChangeType.Onset ? "Krankheitsbeginn" : "Genesung",
            e.GermanMessage,
            SimulationState.CurrentAge,
            DateTime.UtcNow,
            EventCategory.Personal,
            VisualCategory.MentalHealth);

        Dispatcher.UIThread.Post(() =>
        {
            EventLog.Insert(0, entry);

            if (EventLog.Count > 150)
            {
                EventLog.RemoveAt(EventLog.Count - 1);
            }

            // Update active illness names display
            UpdateActiveIllnessNames();

            // Update tree display (may switch to BadAdultTree if 3 illnesses)
            UpdateTreeDisplay();
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
        UpdateTreeDisplay();
        UpdateActiveIllnessNames();
    }

    /// <summary>
    /// Updates the tree display based on current life phase and illness count.
    /// Only updates the phase key when the life phase actually changes (triggers CrossFade).
    /// </summary>
    private void UpdateTreeDisplay()
    {
        var illnessCount = SimulationState.CurrentIllnesses.Count;
        var newPath = GetTreePath(SimulationState.LifePhase, illnessCount, _animationFrame);
        var newKey = $"{SimulationState.LifePhase}_{illnessCount}";

        // Update key only on phase changes to trigger CrossFade transition
        if (CurrentLifePhaseKey != newKey)
        {
            CurrentLifePhaseKey = newKey;
        }

        // Always load if image is null (initial load) or path changed
        if (CurrentTreeImage == null || CurrentTreePath != newPath)
        {
            CurrentTreePath = newPath;
            LoadTreeImage(newPath);
        }
    }

    /// <summary>
    /// Loads an SVG image from the specified resource path.
    /// </summary>
    private void LoadTreeImage(string path)
    {
        try
        {
            var source = SvgSource.Load(path, baseUri: null);
            CurrentTreeImage = new SvgImage { Source = source };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load SVG: {ex.Message}");
            CurrentTreeImage = null;
        }
    }

    /// <summary>
    /// Updates the active illness names collection from current state.
    /// </summary>
    private void UpdateActiveIllnessNames()
    {
        ActiveIllnessNames.Clear();
        foreach (var illness in SimulationState.CurrentIllnesses.Values)
        {
            ActiveIllnessNames.Add(illness.Name);
        }
    }

    /// <summary>
    /// Gets the appropriate character SVG path based on life phase, illness count, and animation frame.
    /// </summary>
    /// <param name="phase">Current life phase.</param>
    /// <param name="illnessCount">Number of active illnesses.</param>
    /// <param name="animationFrame">Animation frame (0 = static, 1 = movement1, 2 = movement2).</param>
    /// <returns>The resource path to the SVG file.</returns>
    private static string GetTreePath(LifePhase phase, int illnessCount, int animationFrame)
    {
        var baseName = GetSpriteBaseName(phase, illnessCount);
        var frameSuffix = animationFrame switch
        {
            1 => "movement1",
            2 => "movement2",
            _ => string.Empty
        };

        return string.IsNullOrEmpty(frameSuffix)
            ? $"avares://SimulationFIN31/Assets/TreeVectors/{baseName}.svg"
            : $"avares://SimulationFIN31/Assets/TreeVectors/{baseName}{frameSuffix}.svg";
    }

    /// <summary>
    /// Gets the base sprite name for the given life phase.
    /// </summary>
    private static string GetSpriteBaseName(LifePhase phase, int illnessCount) => phase switch
    {
        LifePhase.Childhood => "Baby",
        LifePhase.SchoolBeginning => "Child",
        LifePhase.Adolescence => "Teenager",
        LifePhase.EmergingAdulthood => "EmergingAdulthood",
        LifePhase.Adulthood => illnessCount >= 3 ? "BadAdult" : "Adult",
        _ => "Baby"
    };

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
/// <param name="Category">The event category (Generic, Personal, or Coping).</param>
/// <param name="VisualCategory">The visual category for icon display.</param>
public sealed record EventLogEntry(
    string EventName,
    string Description,
    int Age,
    DateTime Timestamp,
    EventCategory Category,
    VisualCategory VisualCategory)
{
    private static readonly Dictionary<string, Avalonia.Media.Imaging.Bitmap> IconCache = new();

    /// <summary>
    /// Formatted display text for the event log.
    /// </summary>
    public string DisplayText => $"[Alter {Age}] {EventName}";

    /// <summary>
    /// Tooltip text for event icon hover display.
    /// </summary>
    public string TooltipText => $"{EventName}\n{Description}";

    /// <summary>
    /// Gets the icon image for this event based on its visual category.
    /// </summary>
    public Avalonia.Media.Imaging.Bitmap? Icon => LoadIcon(VisualCategory);

    /// <summary>
    /// Loads and caches the icon bitmap for the given visual category.
    /// </summary>
    private static Avalonia.Media.Imaging.Bitmap? LoadIcon(VisualCategory category)
    {
        var path = GetIconPathForVisualCategory(category);

        if (IconCache.TryGetValue(path, out var cached))
        {
            return cached;
        }

        try
        {
            var bitmap = new Avalonia.Media.Imaging.Bitmap(AssetLoader.Open(new Uri(path)));
            IconCache[path] = bitmap;
            return bitmap;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Maps a visual category to its corresponding icon asset path.
    /// </summary>
    private static string GetIconPathForVisualCategory(VisualCategory category) => category switch
    {
        VisualCategory.Family => "avares://SimulationFIN31/Assets/eventIcons/FamilyEvent.png",
        VisualCategory.Career => "avares://SimulationFIN31/Assets/eventIcons/CareerEvent.jpg",
        VisualCategory.Education => "avares://SimulationFIN31/Assets/eventIcons/EducationEvent.png",
        VisualCategory.Death => "avares://SimulationFIN31/Assets/eventIcons/DeathEvent.png",
        VisualCategory.Health => "avares://SimulationFIN31/Assets/eventIcons/HealthEvent.png",
        VisualCategory.Social => "avares://SimulationFIN31/Assets/eventIcons/SocialEvent.jpg",
        VisualCategory.Romance => "avares://SimulationFIN31/Assets/eventIcons/RomanceEvent.jpg",
        VisualCategory.Creativity => "avares://SimulationFIN31/Assets/eventIcons/CreativeEvents.png",
        VisualCategory.Sports => "avares://SimulationFIN31/Assets/eventIcons/SportsEvent.png",
        VisualCategory.Financial => "avares://SimulationFIN31/Assets/eventIcons/FinancialEvent.png",
        VisualCategory.MentalHealth => "avares://SimulationFIN31/Assets/eventIcons/MentalHealthEvent.png",
        VisualCategory.Leisure => "avares://SimulationFIN31/Assets/eventIcons/LeisureEvent.jpg",
        VisualCategory.Home => "avares://SimulationFIN31/Assets/eventIcons/HomeEvent.png",
        VisualCategory.Identity => "avares://SimulationFIN31/Assets/eventIcons/IdentityEvent.png",
        VisualCategory.Pet => "avares://SimulationFIN31/Assets/eventIcons/PetEvent.png",
        VisualCategory.Nature => "avares://SimulationFIN31/Assets/eventIcons/NatureEvent.png",
        VisualCategory.CopingFunctional => "avares://SimulationFIN31/Assets/CopingIcons/FunctionalCoping.png",
        VisualCategory.CopingDysfunctional => "avares://SimulationFIN31/Assets/CopingIcons/Dyfsfunctional.png",
        VisualCategory.CopingNeutral => "avares://SimulationFIN31/Assets/CopingIcons/NeutralCoping.png",
        _ => "avares://SimulationFIN31/Assets/eventIcons/MentalHealthEvent.png"
    };
}
