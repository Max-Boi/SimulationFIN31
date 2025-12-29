using System;
using System.ComponentModel;
using SimulationFIN31.ViewModels;

namespace SimulationFIN31.Services.Interfaces;

/// <summary>
/// Service responsible for navigation between views in the application.
/// Manages the current view model and supports data passing during navigation.
/// </summary>
public interface INavigationService : INotifyPropertyChanged
{
    /// <summary>
    /// Gets the currently active view model.
    /// </summary>
    ViewModelBase CurrentViewModel { get; }

    /// <summary>
    /// Navigates to the specified view model type.
    /// </summary>
    /// <typeparam name="T">The type of view model to navigate to.</typeparam>
    void NavigateTo<T>() where T : ViewModelBase;

    /// <summary>
    /// Navigates to the specified view model type with an initialization action.
    /// Allows passing data to the target view model after construction.
    /// </summary>
    /// <typeparam name="T">The type of view model to navigate to.</typeparam>
    /// <param name="initializer">Action to initialize the view model before navigation completes.</param>
    void NavigateTo<T>(Action<T> initializer) where T : ViewModelBase;
}