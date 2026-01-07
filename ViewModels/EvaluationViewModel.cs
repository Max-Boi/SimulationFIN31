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

public partial class EvaluationViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    private SimulationHistory? _history;

    public EvaluationViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

        GoHomeCommand = new RelayCommand(NavigateHome);
    }

    public ICommand GoHomeCommand { get; }

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

    private void NavigateHome()
    {
        _navigationService.NavigateTo<HomeViewModel>();
    }

    [ObservableProperty] private double[] _ages = Array.Empty<double>();

    [ObservableProperty] private double[] _stressValues = Array.Empty<double>();

    [ObservableProperty] private double[] _moodValues = Array.Empty<double>();

    [ObservableProperty] private double[] _socialValues = Array.Empty<double>();

    [ObservableProperty] private double[] _resilienceValues = Array.Empty<double>();

    [ObservableProperty] private double _finalStress;

    [ObservableProperty] private double _finalMood;

    [ObservableProperty] private double _finalSocial;

    [ObservableProperty] private double _finalResilience;

    [ObservableProperty] private int _totalEventsCount;

    [ObservableProperty] private int _totalIllnessesCount;

    [ObservableProperty] private int _functionalCopingCount;

    [ObservableProperty] private int _dysfunctionalCopingCount;

    public ObservableCollection<EventRecord> TopImpactfulEvents { get; } = new();

    public ObservableCollection<EventRecord> TraumaticEvents { get; } = new();

    public ObservableCollection<IllnessRecord> IllnessTimeline { get; } = new();

    public ObservableCollection<CopingUsageSummary> CopingUsageSummary { get; } = new();

    private void ProcessChartData()
    {
        var snapshots = _history!.TurnSnapshots;

        Ages = snapshots.Select(s => (double)s.Age).ToArray();
        StressValues = snapshots.Select(s => s.Stress).ToArray();
        MoodValues = snapshots.Select(s => s.Mood).ToArray();
        SocialValues = snapshots.Select(s => s.SocialBelonging).ToArray();
        ResilienceValues = snapshots.Select(s => s.Resilience).ToArray();
    }

    private void ProcessTopEvents()
    {
        var topEvents = _history!.Events
            .Where(e => !e.IsTraumatic)
            .OrderByDescending(e => e.TotalAbsoluteImpact)
            .Take(10);

        TopImpactfulEvents.Clear();
        foreach (var evt in topEvents) TopImpactfulEvents.Add(evt);
    }

    private void ProcessTraumaticEvents()
    {
        var traumatic = _history!.Events
            .Where(e => e.IsTraumatic)
            .OrderBy(e => e.AgeOccurred);

        TraumaticEvents.Clear();
        foreach (var evt in traumatic) TraumaticEvents.Add(evt);
    }

    private void ProcessIllnessTimeline()
    {
        IllnessTimeline.Clear();
        foreach (var illness in _history!.Illnesses.OrderBy(i => i.OnsetAge)) IllnessTimeline.Add(illness);
    }

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
        foreach (var summary in grouped) CopingUsageSummary.Add(summary);
    }

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
}

public sealed record CopingUsageSummary(
    string Name,
    CopingType Type,
    int UsageCount
);