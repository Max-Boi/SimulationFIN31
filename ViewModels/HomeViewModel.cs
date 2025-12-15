using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public HomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    // Aus einer Methode wird automatisch ein Command generiert:
    // Name im XAML wird: "NavigateSettingsCommand" (Das "Async" oder Methoden-Suffix wird Command)
    [RelayCommand]
    private void NavigateSettings()
    {
        _navigationService.NavigateTo<SettingsViewModel>();
    }

    [RelayCommand]
    private void NavigateSimulation()
    {
        _navigationService.NavigateTo<SimulationViewModel>();
    }
}