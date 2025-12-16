using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimulationFIN31.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
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

    public SettingsViewModel()
    {
        socialEnergyLevel = SocialEnergyOptions[2];
        parentsRelationshipQuality = ParentsRelationshipOptions[1];
    }
}