using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Svg.Skia;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.ViewModels;

public partial class SimulationViewModel : ViewModelBase
{
    private const double DEFAULT_SIMULATION_SPEED = 1.0;
    private const double BASE_ANIMATION_INTERVAL_MS = 400.0;
    private const double MIN_ANIMATION_INTERVAL_MS = 80.0;
    private readonly ISimulationHistoryService _historyService;

    private readonly INavigationService _navigationService;
    private readonly ISimulationService _simulationService;
    private int _animationFrame;
    private DispatcherTimer? _animationTimer;

    private CancellationTokenSource? _simulationCts;

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

    public ICommand GoBackCommand { get; }

    private void InitializeAnimationTimer()
    {
        _animationTimer = new DispatcherTimer
        {
            Interval = CalculateAnimationInterval(SimulationSpeed)
        };
        _animationTimer.Tick += OnAnimationTick;
    }

    private static TimeSpan CalculateAnimationInterval(double speed)
    {
        var adjustedMs = BASE_ANIMATION_INTERVAL_MS / speed;
        return TimeSpan.FromMilliseconds(Math.Max(adjustedMs, MIN_ANIMATION_INTERVAL_MS));
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        _animationFrame = _animationFrame == 1 ? 2 : 1;
        UpdateAnimatedSprite();
    }

    private void UpdateAnimatedSprite()
    {
        var illnessCount = SimulationState.CurrentIllnesses.Count;
        var newPath = GetAvatarPath(SimulationState.LifePhase, illnessCount, _animationFrame);

        if (CurrentTreePath != newPath)
        {
            CurrentTreePath = newPath;
            LoadTreeImage(newPath);
        }
    }

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

    partial void OnSimulationSpeedChanged(double value)
    {
        if (_animationTimer != null) _animationTimer.Interval = CalculateAnimationInterval(value);
    }

    partial void OnIsRunningChanged(bool value)
    {
        UpdateAnimationState();
    }

    partial void OnIsPausedChanged(bool value)
    {
        UpdateAnimationState();
    }

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

            simulationCompleted = SimulationState.CurrentAge >= 30;
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsRunning = false;
                IsPaused = false;
            });
        }

        if (simulationCompleted)
        {
            var history = _historyService.GetCompleteHistory(SimulationState);

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _navigationService.NavigateTo<EvaluationViewModel>(vm => vm.Initialize(history));
            });
        }
    }

    [ObservableProperty] private SimulationState _simulationState;

    [ObservableProperty] private double _stressLevel;

    [ObservableProperty] private double _moodLevel;

    [ObservableProperty] private double _socialLevel;

    [ObservableProperty] private int _currentAge;

    [ObservableProperty] private string _currentLifePhase = string.Empty;

    [ObservableProperty] private double _simulationSpeed = DEFAULT_SIMULATION_SPEED;

    [ObservableProperty] private bool _isRunning;

    [ObservableProperty] private bool _isPaused;

    [ObservableProperty] private string _currentTreePath = "avares://SimulationFIN31/Assets/Avatar/Baby.svg";

    [ObservableProperty] private string _currentLifePhaseKey = "Childhood_0";

    [ObservableProperty] private IImage? _currentTreeImage;

    [ObservableProperty] private EventLogEntry? _latestGenericEvent;

    [ObservableProperty] private EventLogEntry? _latestPersonalEvent;

    [ObservableProperty] private EventLogEntry? _latestCopingEvent;

    public ObservableCollection<string> ActiveIllnessNames { get; } = new();

    public ObservableCollection<EventLogEntry> EventLog { get; } = new();

    [RelayCommand]
    private async Task StartSimulationAsync()
    {
        if (IsRunning && !IsPaused) return;

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

    [RelayCommand]
    private void PauseSimulation()
    {
        if (!IsRunning || IsPaused) return;

        IsPaused = true;
    }

    [RelayCommand]
    private async Task StopSimulationAsync()
    {
        if (!IsRunning) return;

        _simulationCts?.Cancel();

        IsRunning = false;
        IsPaused = false;

        await Task.Delay(50);

        _simulationCts?.Dispose();
        _simulationCts = null;
    }

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

            if (EventLog.Count > 150) EventLog.RemoveAt(EventLog.Count - 1);

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

    private void OnStateUpdated(object? sender, SimulationState state)
    {
        _historyService.RecordTurnSnapshot(state);

        Dispatcher.UIThread.Post(UpdateDisplayProperties);
    }

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

            if (EventLog.Count > 150) EventLog.RemoveAt(EventLog.Count - 1);

            UpdateActiveIllnessNames();

            UpdateTreeDisplay();
        });
    }

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

    private void UpdateTreeDisplay()
    {
        var illnessCount = SimulationState.CurrentIllnesses.Count;
        var newPath = GetAvatarPath(SimulationState.LifePhase, illnessCount, _animationFrame);
        var newKey = $"{SimulationState.LifePhase}_{illnessCount}";

        if (CurrentLifePhaseKey != newKey) CurrentLifePhaseKey = newKey;

        if (CurrentTreeImage == null || CurrentTreePath != newPath)
        {
            CurrentTreePath = newPath;
            LoadTreeImage(newPath);
        }
    }

    private void LoadTreeImage(string path)
    {
        try
        {
            var source = SvgSource.Load(path, null);
            CurrentTreeImage = new SvgImage { Source = source };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load SVG: {ex.Message}");
            CurrentTreeImage = null;
        }
    }

    private void UpdateActiveIllnessNames()
    {
        ActiveIllnessNames.Clear();
        foreach (var illness in SimulationState.CurrentIllnesses.Values) ActiveIllnessNames.Add(illness.Name);
    }

    private static string GetAvatarPath(LifePhase phase, int illnessCount, int animationFrame)
    {
        var baseName = GetSpriteBaseName(phase, illnessCount);
        var frameSuffix = animationFrame switch
        {
            1 => "movement1",
            2 => "movement2",
            _ => string.Empty
        };

        return string.IsNullOrEmpty(frameSuffix)
            ? $"avares://SimulationFIN31/Assets/Avatar/{baseName}.svg"
            : $"avares://SimulationFIN31/Assets/Avatar/{baseName}{frameSuffix}.svg";
    }

    private static string GetSpriteBaseName(LifePhase phase, int illnessCount)
    {
        return phase switch
        {
            LifePhase.Childhood => "Baby",
            LifePhase.SchoolBeginning => "Child",
            LifePhase.Adolescence => "Teenager",
            LifePhase.EmergingAdulthood => "EmergingAdulthood",
            LifePhase.Adulthood => "Adult",
            _ => "Baby"
        };
    }

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
            ResilienceScore = 85,
            PhysicalHealth = 100,

            IncomeLevel = EnumConverter.MapIncomeLevel(settings.IncomeLevel),
            ParentsEducationLevel = EnumConverter.MapParentsEducationLevel(settings.ParentsEducationLevel),
            JobStatus = EnumConverter.MapJobStatus(settings.JobStatus),
            SocialEnvironmentLevel = settings.SocialEnvironmentLevel,
            FamilyCloseness = settings.FamilyCloseness,
            ParentsRelationshipQuality =
                EnumConverter.ToParentsRelationshipQuality(settings.ParentsRelationshipQuality),
            ParentsWithAddiction = settings.ParentsWithAddiction,
            HasAdhd = settings.HasAdhd,
            HasAutism = settings.HasAutism,
            IntelligenceScore = settings.IntelligenceScore,
            AnxietyLevel = settings.AnxietyLevel,
            SocialEnergyLevel = EnumConverter.ToSocialEnergyLevel(settings.SocialEnergyLevel),
            Gender = EnumConverter.ToGenderType(settings.Gender)
        };
    }

    private static SimulationSettings LoadSettings()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "settings.json");

        if (!File.Exists(settingsPath))
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
                IntelligenceScore = 100,
                AnxietyLevel = 40,
                SocialEnergyLevel = "Ambivertiert",
                Gender = "Männlich"
            };

        try
        {
            var json = File.ReadAllText(settingsPath);
            return JsonSerializer.Deserialize<SimulationSettings>(json)
                   ?? throw new InvalidOperationException("Settings deserialization returned null");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load settings: {ex.Message}");
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

    private static string GetLifePhaseDisplayName(LifePhase phase)
    {
        return phase switch
        {
            LifePhase.Childhood => "Kindheit",
            LifePhase.SchoolBeginning => "Schulbeginn",
            LifePhase.Adolescence => "Jugend",
            LifePhase.EmergingAdulthood => "Frühes Erwachsenenalter",
            LifePhase.Adulthood => "Erwachsenenalter",
            _ => "Unbekannt"
        };
    }

    private void NavigateBack()
    {
        StopSimulationAsync().ConfigureAwait(false);
        _navigationService.NavigateTo<HomeViewModel>();
    }
}

public sealed record EventLogEntry(
    string EventName,
    string Description,
    int Age,
    DateTime Timestamp,
    EventCategory Category,
    VisualCategory VisualCategory)
{
    private static readonly Dictionary<string, Bitmap> IconCache = new();

    public string DisplayText => $"[Alter {Age}] {EventName}";

    public string TooltipText => $"{EventName}\n{Description}";

    public Bitmap? Icon => LoadIcon(VisualCategory);

    private static Bitmap? LoadIcon(VisualCategory category)
    {
        var path = GetIconPathForVisualCategory(category);

        if (IconCache.TryGetValue(path, out var cached)) return cached;

        try
        {
            var bitmap = new Bitmap(AssetLoader.Open(new Uri(path)));
            IconCache[path] = bitmap;
            return bitmap;
        }
        catch
        {
            return null;
        }
    }

    private static string GetIconPathForVisualCategory(VisualCategory category)
    {
        return category switch
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
}