using System;
using System.Reflection;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Services.Interfaces;
using SimulationFIN31.ViewModels.Util;

namespace SimulationFIN31.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public HomeViewModel(INavigationService navigationService)
    {
        try
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var cherryPath = $"avares://{assemblyName}/Assets/blossom.png";
            var sunsetPath = $"avares://{assemblyName}/Assets/sunset.png";

            SunsetPic = ImageHelper.LoadFromResource(sunsetPath);
            CherryBlossomPic = ImageHelper.LoadFromResource(cherryPath);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Fehler beim Laden der Bilder: {ex.Message}");
            SunsetPic = null;
            CherryBlossomPic = null;
        }
        _navigationService = navigationService;
    }

    public Bitmap? CherryBlossomPic { get; }
    public Bitmap? SunsetPic { get; }

  
    [RelayCommand]
    private void NavigateSettings()
    {
        // Reload settings from file when navigating to settings
        // This ensures unsaved in-memory changes are discarded
        _navigationService.NavigateTo<SettingsViewModel>(vm => vm.LoadSettingsFromFile());
    }

    [RelayCommand]
    private void NavigateSimulation()
    {
        _navigationService.NavigateTo<SimulationViewModel>();
    }
}