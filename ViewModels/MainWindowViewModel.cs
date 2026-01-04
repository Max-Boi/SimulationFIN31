using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    // Wir spiegeln das ViewModel des Services in die View
    [ObservableProperty] private ViewModelBase _currentViewModel;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        // 1. Auf Änderungen im Service hören
        _navigationService.PropertyChanged += OnServicePropertyChanged;

        // 2. Startseite setzen
        _navigationService.NavigateTo<HomeViewModel>();
    }

    // Event Handler: Wenn sich im Service was ändert, übernehmen wir es
    private void OnServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(INavigationService.CurrentViewModel))
            CurrentViewModel = _navigationService.CurrentViewModel;
    }
}