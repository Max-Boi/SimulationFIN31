using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.History;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.ViewModels;

/// <summary>
/// ViewModel for the evaluation/results view displaying simulation history
/// with charts, timelines, and impact analysis.
/// </summary>
public partial class EvaluationViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    private SimulationHistory? _history;

    #region -- Chart Data Properties --

    /// <summary>
    /// Ages for X-axis of the chart.
    /// </summary>
    [ObservableProperty]
    private double[] _ages = Array.Empty<double>();

    /// <summary>
    /// Stress values over time for chart display.
    /// </summary>
    [ObservableProperty]
    private double[] _stressValues = Array.Empty<double>();

    /// <summary>
    /// Mood values over time for chart display.
    /// </summary>
    [ObservableProperty]
    private double[] _moodValues = Array.Empty<double>();

    /// <summary>
    /// Social belonging values over time for chart display.
    /// </summary>
    [ObservableProperty]
    private double[] _socialValues = Array.Empty<double>();

    /// <summary>
    /// Resilience values over time for chart display.
    /// </summary>
    [ObservableProperty]
    private double[] _resilienceValues = Array.Empty<double>();

    #endregion

    #region -- Summary Statistics --

    /// <summary>
    /// Final stress level at simulation end.
    /// </summary>
    [ObservableProperty]
    private double _finalStress;

    /// <summary>
    /// Final mood level at simulation end.
    /// </summary>
    [ObservableProperty]
    private double _finalMood;

    /// <summary>
    /// Final social belonging level at simulation end.
    /// </summary>
    [ObservableProperty]
    private double _finalSocial;

    /// <summary>
    /// Final resilience level at simulation end.
    /// </summary>
    [ObservableProperty]
    private double _finalResilience;

    /// <summary>
    /// Total number of events that occurred during simulation.
    /// </summary>
    [ObservableProperty]
    private int _totalEventsCount;

    /// <summary>
    /// Total number of illnesses experienced during simulation.
    /// </summary>
    [ObservableProperty]
    private int _totalIllnessesCount;

    /// <summary>
    /// Number of times functional coping mechanisms were used.
    /// </summary>
    [ObservableProperty]
    private int _functionalCopingCount;

    /// <summary>
    /// Number of times dysfunctional coping mechanisms were used.
    /// </summary>
    [ObservableProperty]
    private int _dysfunctionalCopingCount;

    #endregion

    #region -- Collections --

    /// <summary>
    /// Top 10 most impactful events ranked by total absolute impact.
    /// </summary>
    public ObservableCollection<EventRecord> TopImpactfulEvents { get; } = new();

    /// <summary>
    /// Traumatic events that occurred during simulation.
    /// </summary>
    public ObservableCollection<EventRecord> TraumaticEvents { get; } = new();

    /// <summary>
    /// Illness timeline records for visualization.
    /// </summary>
    public ObservableCollection<IllnessRecord> IllnessTimeline { get; } = new();

    /// <summary>
    /// Coping mechanism usage summary grouped by mechanism.
    /// </summary>
    public ObservableCollection<CopingUsageSummary> CopingUsageSummary { get; } = new();

    #endregion

    /// <summary>
    /// Command to navigate back to home view.
    /// </summary>
    public ICommand GoHomeCommand { get; }

    /// <summary>
    /// Creates a new EvaluationViewModel with required dependencies.
    /// </summary>
    /// <param name="navigationService">Service for view navigation.</param>
    /// <exception cref="ArgumentNullException">When navigationService is null.</exception>
    public EvaluationViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

        GoHomeCommand = new RelayCommand(NavigateHome);
    }

    /// <summary>
    /// Initializes the ViewModel with simulation history data.
    /// Called by NavigationService after construction.
    /// </summary>
    /// <param name="history">The complete simulation history to display.</param>
    /// <exception cref="ArgumentNullException">When history is null.</exception>
    public void Initialize(SimulationHistory history)
    {
        _history = history ?? throw new ArgumentNullException(nameof(history));

        ProcessChartData();
        ProcessTopEvents();
        ProcessTraumaticEvents();
        ProcessIllnessTimeline();
        ProcessCopingUsage();
        CalculateSummaryStatistics();
    }

    #region -- Data Processing Methods --

    /// <summary>
    /// Processes turn snapshots into arrays for chart display.
    /// </summary>
    private void ProcessChartData()
    {
        var snapshots = _history!.TurnSnapshots;

        Ages = snapshots.Select(s => (double)s.Age).ToArray();
        StressValues = snapshots.Select(s => s.Stress).ToArray();
        MoodValues = snapshots.Select(s => s.Mood).ToArray();
        SocialValues = snapshots.Select(s => s.SocialBelonging).ToArray();
        ResilienceValues = snapshots.Select(s => s.Resilience).ToArray();
    }

    /// <summary>
    /// Processes and ranks top 10 most impactful events.
    /// </summary>
    private void ProcessTopEvents()
    {
        var topEvents = _history!.Events
            .Where(e => !e.IsTraumatic) // Exclude traumatic events (shown separately)
            .OrderByDescending(e => e.TotalAbsoluteImpact)
            .Take(10);

        TopImpactfulEvents.Clear();
        foreach (var evt in topEvents)
        {
            TopImpactfulEvents.Add(evt);
        }
    }

    /// <summary>
    /// Extracts traumatic events for separate display.
    /// </summary>
    private void ProcessTraumaticEvents()
    {
        var traumatic = _history!.Events
            .Where(e => e.IsTraumatic)
            .OrderBy(e => e.AgeOccurred);

        TraumaticEvents.Clear();
        foreach (var evt in traumatic)
        {
            TraumaticEvents.Add(evt);
        }
    }

    /// <summary>
    /// Processes illness records for timeline display.
    /// </summary>
    private void ProcessIllnessTimeline()
    {
        IllnessTimeline.Clear();
        foreach (var illness in _history!.Illnesses.OrderBy(i => i.OnsetAge))
        {
            IllnessTimeline.Add(illness);
        }
    }

    /// <summary>
    /// Summarizes coping mechanism usage by type and frequency.
    /// </summary>
    private void ProcessCopingUsage()
    {
        var grouped = _history!.CopingUsage
            .GroupBy(c => c.Name)
            .Select(g => new CopingUsageSummary(
                g.Key,
                g.First().Type,
                g.Count()))
            .OrderByDescending(s => s.UsageCount);

        CopingUsageSummary.Clear();
        foreach (var summary in grouped)
        {
            CopingUsageSummary.Add(summary);
        }
    }

    /// <summary>
    /// Calculates summary statistics from the simulation history.
    /// </summary>
    private void CalculateSummaryStatistics()
    {
        var final = _history!.FinalState;

        FinalStress = final.CurrentStress;
        FinalMood = final.CurrentMood;
        FinalSocial = final.SocialBelonging;
        FinalResilience = final.ResilienceScore;

        TotalEventsCount = _history.Events.Count;
        TotalIllnessesCount = _history.Illnesses.Count;

        FunctionalCopingCount = _history.CopingUsage
            .Count(c => c.Type == CopingType.Functional);
        DysfunctionalCopingCount = _history.CopingUsage
            .Count(c => c.Type == CopingType.Dysfunctional);
    }

    #endregion

    /// <summary>
    /// Navigates back to the home view.
    /// </summary>
    private void NavigateHome()
    {
        _navigationService.NavigateTo<HomeViewModel>();
    }
}

/// <summary>
/// Summary record for coping mechanism usage display.
/// </summary>
/// <param name="Name">Name of the coping mechanism.</param>
/// <param name="Type">Type classification (Functional/Dysfunctional/Neutral).</param>
/// <param name="UsageCount">Number of times this mechanism was used.</param>
public sealed record CopingUsageSummary(
    string Name,
    CopingType Type,
    int UsageCount
);
