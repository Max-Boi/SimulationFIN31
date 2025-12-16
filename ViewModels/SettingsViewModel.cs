using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    private INavigationService _navigationService;
    private readonly IReadOnlyList<string> ParentsRelationshipOptions = new[]
    {
        "harmonisch",
        "neutral",
        "konfliktgeladen"
    };

    private readonly IReadOnlyList<string> SocialEnergyOptions = new[]
    {
        "Stark introvertiert",
        "Eher introvertiert",
        "Ambivertiert",
        "Eher extrovertiert",
        "Stark extrovertiert"
    };
    [ObservableProperty]
    private int incomeLevel = 4;

    [ObservableProperty]
    private int parentsEducationLevel = 4;

    [ObservableProperty]
    private int culturalPracticeLevel = 4;

    [ObservableProperty]
    private int socialEnvironmentLevel = 4;

    [ObservableProperty]
    private bool hasAdhd;

    [ObservableProperty]
    private bool hasAutism;

    [ObservableProperty]
    private bool parentsWithAddiction;

    [ObservableProperty]
    private int intelligenceScore = 50;

    [ObservableProperty]
    private int anxietyLevel = 40;

    [ObservableProperty]
    private string parentsRelationshipQuality ;

    [ObservableProperty]
    private int familyCloseness = 50;

    [ObservableProperty]
    private string socialEnergyLevel;

    [RelayCommand]
    public void  NavigateStart()
    {
        _navigationService.NavigateTo<HomeViewModel>();
    }
    public SettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        socialEnergyLevel = SocialEnergyOptions[2];
        parentsRelationshipQuality = ParentsRelationshipOptions[1];
    }
}