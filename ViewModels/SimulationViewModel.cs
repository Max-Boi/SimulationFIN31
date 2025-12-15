using System.Windows.Input;
using ReactiveUI;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.ViewModels;

public class SimulationViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public ICommand GoBackCommand { get; }

    public SimulationViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        GoBackCommand = ReactiveCommand.Create(() => 
        {
            _navigationService.NavigateTo<HomeViewModel>();
        });
    }
}