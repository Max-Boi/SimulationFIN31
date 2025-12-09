using System;
using SimulationFIN31.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
namespace SimulationFIN31.Services;

public partial class NavigationService : ObservableObject
{
    // Erstellt automatisch: 
    // 1. Property "CurrentViewModel"
    // 2. Event "PropertyChanged" (darauf lauscht Avalonia UI direkt)
    [ObservableProperty] 
    private ViewModelBase? _currentViewModel; 

    public void NavigateTo(ViewModelBase viewModel)
    {
        CurrentViewModel = viewModel;
    }
}
