using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Models;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    private const int DEFAULT_INCOME_LEVEL = 4;
    private const int DEFAULT_PARENTS_EDUCATION_LEVEL = 4;
    private const int DEFAULT_JOB_STATUS = 4;
    private const int DEFAULT_SOCIAL_ENVIRONMENT_LEVEL = 50;
    private const int DEFAULT_INTELLIGENCE_SCORE = 100;
    private const int DEFAULT_ANXIETY_LEVEL = 40;
    private const int DEFAULT_FAMILY_CLOSENESS = 50;
    private const int DEFAULT_MAX_AGE = 30;
    private const bool DEFAULT_USE_DOUBLE_EVENTS = true;
    private static readonly string SettingsFilePath = Path.Combine(AppContext.BaseDirectory, "settings.json");
    private static bool _cleanupRegistered;

    private readonly INavigationService _navigationService;

    [ObservableProperty] private int _anxietyLevel = DEFAULT_ANXIETY_LEVEL;

    [ObservableProperty] private int _familyCloseness = DEFAULT_FAMILY_CLOSENESS;

    [ObservableProperty] private string _gender;

    [ObservableProperty] private bool _hasAdhd;

    [ObservableProperty] private bool _hasAutism;

    [ObservableProperty] private int _incomeLevel = DEFAULT_INCOME_LEVEL;

    [ObservableProperty] private int _intelligenceScore = DEFAULT_INTELLIGENCE_SCORE;

    [ObservableProperty] private int _jobStatus = DEFAULT_JOB_STATUS;

    [ObservableProperty] private int _parentsEducationLevel = DEFAULT_PARENTS_EDUCATION_LEVEL;

    [ObservableProperty] private string _parentsRelationshipQuality;

    [ObservableProperty] private bool _parentsWithAddiction;

    [ObservableProperty] private bool _showSaveConfirmation;

    [ObservableProperty] private string _socialEnergyLevel;

    [ObservableProperty] private int _socialEnvironmentLevel = DEFAULT_SOCIAL_ENVIRONMENT_LEVEL;

    [ObservableProperty] private int _maxAge = DEFAULT_MAX_AGE;

    [ObservableProperty] private bool _useDoubleEvents = DEFAULT_USE_DOUBLE_EVENTS;

    public SettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        _socialEnergyLevel = SocialEnergyOptions[2];
        _parentsRelationshipQuality = ParentsRelationshipOptions[1];
        _gender = GenderOptions[0];

        RegisterCleanup();
    }

    public string SesToolTip { get; } =
        "Ein niedriger Status in den Bereichen Bildung," +
        " Einkommen und Beruf erhöht nachweislich das Risiko, an Depressionen," +
        " Angststörungen oder Sucht zu erkranken, signifikant (Sozialer Gradient)." +
        "\n\nInsbesondere geringe Bildung und finanzielle Unsicherheit erzeugen chronischen Stress" +
        " und begrenzen gleichzeitig die persönlichen Fähigkeiten, Krisen gesund zu bewältigen." +
        "\n\nUmgekehrt wirken ein sicheres Einkommen und ein hoher Berufsstatus als Schutzfaktoren," +
        " da sie dem Individuum mehr Kontrolle über das eigene Leben und besseren Zugang zu Hilfe ermöglichen.";

    public string FamilyEnvironmentToolTip { get; } =
        "Das familiäre und soziale Umfeld bildet die primäre Sozialisationsinstanz und prägt " +
        "Bindungsmuster, emotionale Regulation sowie Stressbewältigungsstrategien nachhaltig." +
        "\n\nKonflikte zwischen Eltern, Suchterkrankungen oder mangelnde emotionale Nähe erhöhen " +
        "das Risiko für Angststörungen und Depression, während ein stabiles Umfeld als Schutzfaktor wirkt.";

    public string PersonalResourcesToolTip { get; } =
        "Individuelle Ressourcen wie kognitive Fähigkeiten, Temperament und neurologische Besonderheiten " +
        "beeinflussen die Vulnerabilität für psychische Erkrankungen gemäß dem Diathese-Stress-Modell." +
        "\n\nHöhere Intelligenz und Extraversion fördern adaptive Bewältigungsstrategien, während " +
        "erhöhte Trait-Angst und introvertierte Tendenzen das Risiko für Angststörungen erhöhen können.";

    public IReadOnlyList<string> ParentsRelationshipOptions { get; } =
    [
        "harmonisch",
        "neutral",
        "konfliktgeladen"
    ];

    public IReadOnlyList<string> SocialEnergyOptions { get; } =
    [
        "Stark introvertiert",
        "Eher introvertiert",
        "Ambivertiert",
        "Eher extrovertiert",
        "Stark extrovertiert"
    ];

    public IReadOnlyList<string> GenderOptions { get; } =
    [
        "Männlich",
        "Weiblich",
        "Non-Binär"
    ];

    [RelayCommand]
    public void NavigateStart()
    {
        _navigationService.NavigateTo<HomeViewModel>();
    }

    [RelayCommand]
    public async Task SaveSettingsAsync()
    {
        var settings = new SimulationSettings
        {
            IncomeLevel = IncomeLevel,
            ParentsEducationLevel = ParentsEducationLevel,
            JobStatus = JobStatus,
            SocialEnvironmentLevel = SocialEnvironmentLevel,
            HasAdhd = HasAdhd,
            HasAutism = HasAutism,
            ParentsWithAddiction = ParentsWithAddiction,
            IntelligenceScore = IntelligenceScore,
            AnxietyLevel = AnxietyLevel,
            ParentsRelationshipQuality = ParentsRelationshipQuality,
            FamilyCloseness = FamilyCloseness,
            SocialEnergyLevel = SocialEnergyLevel,
            Gender = Gender,
            MaxAge = MaxAge,
            UseDoubleEvents = UseDoubleEvents
        };

        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(SettingsFilePath, json);

        ShowSaveConfirmation = true;
        await Task.Delay(1000);
        ShowSaveConfirmation = false;
    }

    [RelayCommand]
    public void ResetSettings()
    {
        IncomeLevel = DEFAULT_INCOME_LEVEL;
        ParentsEducationLevel = DEFAULT_PARENTS_EDUCATION_LEVEL;
        JobStatus = DEFAULT_JOB_STATUS;
        SocialEnvironmentLevel = DEFAULT_SOCIAL_ENVIRONMENT_LEVEL;
        HasAdhd = false;
        HasAutism = false;
        ParentsWithAddiction = false;
        IntelligenceScore = DEFAULT_INTELLIGENCE_SCORE;
        AnxietyLevel = DEFAULT_ANXIETY_LEVEL;
        ParentsRelationshipQuality = ParentsRelationshipOptions[1];
        FamilyCloseness = DEFAULT_FAMILY_CLOSENESS;
        SocialEnergyLevel = SocialEnergyOptions[2];
        Gender = GenderOptions[0];
        MaxAge = DEFAULT_MAX_AGE;
        UseDoubleEvents = DEFAULT_USE_DOUBLE_EVENTS;

        ResetSettingsFile();
    }

    private static void RegisterCleanup()
    {
        if (_cleanupRegistered) return;

        AppDomain.CurrentDomain.ProcessExit += (_, _) => ResetSettingsFile();
        AppDomain.CurrentDomain.DomainUnload += (_, _) => ResetSettingsFile();
        _cleanupRegistered = true;
    }

    private static void ResetSettingsFile()
    {
        if (!File.Exists(SettingsFilePath)) return;

        File.Delete(SettingsFilePath);
    }
}