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
    public string SesToolTip { get;  } =
        "Ein niedriger Status in den Bereichen Bildung," +
        " Einkommen und Beruf erhöht nachweislich das Risiko, an Depressionen," +
        " Angststörungen oder Sucht zu erkranken, signifikant (Sozialer Gradient)." +
        "\n\nInsbesondere geringe Bildung und finanzielle Unsicherheit erzeugen chronischen Stress" +
        " und begrenzen gleichzeitig die persönlichen Fähigkeiten, Krisen gesund zu bewältigen." +
        "\n\nUmgekehrt wirken ein sicheres Einkommen und ein hoher Berufsstatus als Schutzfaktoren," +
        " da sie dem Individuum mehr Kontrolle über das eigene Leben und besseren Zugang zu Hilfe ermöglichen.";

    private const int DefaultIncomeLevel = 4;
    private const int DefaultParentsEducationLevel = 4;
    private const int DefaultCulturalPracticeLevel = 4;
    private const int DefaultSocialEnvironmentLevel = 4;
    private const int DefaultIntelligenceScore = 50;
    private const int DefaultAnxietyLevel = 40;
    private const int DefaultFamilyCloseness = 50;

    private readonly INavigationService _navigationService;
    private static readonly string SettingsFilePath = Path.Combine(AppContext.BaseDirectory, "settings.json");
    private static bool _cleanupRegistered;
    private readonly IReadOnlyList<string> _parentsRelationshipOptions =
    [
        "harmonisch",
        "neutral",
        "konfliktgeladen"
    ];

    private readonly IReadOnlyList<string> _socialEnergyOptions =
    [
        "Stark introvertiert",
        "Eher introvertiert",
        "Ambivertiert",
        "Eher extrovertiert",
        "Stark extrovertiert"
    ];

    public IReadOnlyList<string> ParentsRelationshipOptions => _parentsRelationshipOptions;
    public IReadOnlyList<string> SocialEnergyOptions => _socialEnergyOptions;

    [ObservableProperty]
    private int _incomeLevel = DefaultIncomeLevel;

    [ObservableProperty]
    private int _parentsEducationLevel = DefaultParentsEducationLevel;

    [ObservableProperty]
    private int _culturalPracticeLevel = DefaultCulturalPracticeLevel;

    [ObservableProperty]
    private int _socialEnvironmentLevel = DefaultSocialEnvironmentLevel;

    [ObservableProperty]
    private bool _hasAdhd;

    [ObservableProperty]
    private bool _hasAutism;

    [ObservableProperty]
    private bool _parentsWithAddiction;

    [ObservableProperty]
    private int _intelligenceScore = 50;

    [ObservableProperty]
    private int _anxietyLevel = 40;

    [ObservableProperty]
    private string _parentsRelationshipQuality;

    [ObservableProperty]
    private int _familyCloseness = 50;

    [ObservableProperty]
    private string _socialEnergyLevel;

    [ObservableProperty]
    private bool _showSaveConfirmation;

    [RelayCommand]
    public void  NavigateStart()
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
            CulturalPracticeLevel = CulturalPracticeLevel,
            SocialEnvironmentLevel = SocialEnvironmentLevel,
            HasAdhd = HasAdhd,
            HasAutism = HasAutism,
            ParentsWithAddiction = ParentsWithAddiction,
            IntelligenceScore = IntelligenceScore,
            AnxietyLevel = AnxietyLevel,
            ParentsRelationshipQuality = ParentsRelationshipQuality,
            FamilyCloseness = FamilyCloseness,
            SocialEnergyLevel = SocialEnergyLevel
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
        IncomeLevel = DefaultIncomeLevel;
        ParentsEducationLevel = DefaultParentsEducationLevel;
        CulturalPracticeLevel = DefaultCulturalPracticeLevel;
        SocialEnvironmentLevel = DefaultSocialEnvironmentLevel;
        HasAdhd = false;
        HasAutism = false;
        ParentsWithAddiction = false;
        IntelligenceScore = DefaultIntelligenceScore;
        AnxietyLevel = DefaultAnxietyLevel;
        ParentsRelationshipQuality = _parentsRelationshipOptions[1];
        FamilyCloseness = DefaultFamilyCloseness;
        SocialEnergyLevel = _socialEnergyOptions[2];

        ResetSettingsFile();
    }
    public SettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        _socialEnergyLevel = _socialEnergyOptions[2];
        _parentsRelationshipQuality = _parentsRelationshipOptions[1];

        RegisterCleanup();
    }

    private static void RegisterCleanup()
    {
        if (_cleanupRegistered)
        {
            return;
        }

        AppDomain.CurrentDomain.ProcessExit += (_, _) => ResetSettingsFile();
        AppDomain.CurrentDomain.DomainUnload += (_, _) => ResetSettingsFile();
        _cleanupRegistered = true;
    }

    private static void ResetSettingsFile()
    {
        if (!File.Exists(SettingsFilePath))
        {
            return;
        }

        File.Delete(SettingsFilePath);
    }
}