using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    
    private readonly INavigationService _navigationService;
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
    [ObservableProperty]
    private int _incomeLevel = 4;

    [ObservableProperty]
    private int _parentsEducationLevel = 4;

    [ObservableProperty]
    private int _culturalPracticeLevel = 4;

    [ObservableProperty]
    private int _socialEnvironmentLevel = 4;

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
    private string _parentsRelationshipQuality ;

    [ObservableProperty]
    private int _familyCloseness = 50;

    [ObservableProperty]
    private string _socialEnergyLevel;

    [RelayCommand]
    public void  NavigateStart()
    {
        _navigationService.NavigateTo<HomeViewModel>();
    }

    [RelayCommand]
    public void SaveSettings()
    {
        throw  new System.NotImplementedException();
    }
    [RelayCommand]
    public void ResetSettings()
    {
        throw  new System.NotImplementedException();
    }
    public SettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        _socialEnergyLevel = _socialEnergyOptions[2];
        _parentsRelationshipQuality = _parentsRelationshipOptions[1];
    }
}