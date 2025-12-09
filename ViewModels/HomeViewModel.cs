using System;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Services;

namespace SimulationFIN31.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly NavigationService _navigationService;

    // DI injiziert den Service und eine Factory für das Ziel-VM
    public HomeViewModel(NavigationService navService)
    {
        _navigationService = navService;
    }

    [RelayCommand] // Erzeugt automatisch einen Command "GoToLoginCommand"
    private void GoToLogin()
    {
        var newVm = _loginVmFactory();
        _navigationService.NavigateTo(newVm);
    }
}