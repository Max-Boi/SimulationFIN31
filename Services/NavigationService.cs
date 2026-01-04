using System;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using SimulationFIN31.Services.Interfaces;
using SimulationFIN31.ViewModels;

namespace SimulationFIN31.Services;

public partial class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, ViewModelBase> _viewModelFactory;

    // Das [ObservableProperty] Attribut generiert automatisch:
    // public ViewModelBase CurrentViewModel { get; set; }
    // und kümmert sich um PropertyChanged Events.
    [ObservableProperty] private ViewModelBase _currentViewModel;

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    /// <inheritdoc />
    public void NavigateTo<T>() where T : ViewModelBase
    {
        var viewModel = _viewModelFactory(typeof(T));

        // Sicherheitshalber immer auf den UI-Thread zwingen
        Dispatcher.UIThread.Post(() => { CurrentViewModel = viewModel; });
    }

    /// <inheritdoc />
    public void NavigateTo<T>(Action<T> initializer) where T : ViewModelBase
    {
        ArgumentNullException.ThrowIfNull(initializer);

        var viewModel = (T)_viewModelFactory(typeof(T));
        initializer(viewModel);

        Dispatcher.UIThread.Post(() => { CurrentViewModel = viewModel; });
    }
}