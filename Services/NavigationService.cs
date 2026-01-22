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
        try
        {
            var viewModel = _viewModelFactory(typeof(T));

            // Sicherheitshalber immer auf den UI-Thread zwingen
            Dispatcher.UIThread.Post(() => { CurrentViewModel = viewModel; });
        }
        catch (InvalidOperationException ex)
        {
            System.Diagnostics.Debug.WriteLine($"ViewModel konnte nicht erstellt werden: {ex.Message}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Navigationsfehler: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public void NavigateTo<T>(Action<T> initializer) where T : ViewModelBase
    {
        ArgumentNullException.ThrowIfNull(initializer);

        try
        {
            var viewModel = (T)_viewModelFactory(typeof(T));
            initializer(viewModel);

            Dispatcher.UIThread.Post(() => { CurrentViewModel = viewModel; });
        }
        catch (InvalidCastException ex)
        {
            System.Diagnostics.Debug.WriteLine($"ViewModel-Typ stimmt nicht überein: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            System.Diagnostics.Debug.WriteLine($"ViewModel konnte nicht initialisiert werden: {ex.Message}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Navigationsfehler: {ex.Message}");
        }
    }
}