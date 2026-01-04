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
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        var cherryPath = $"avares://{assemblyName}/Assets/blossom.png";
        var sunsetPath = $"avares://{assemblyName}/Assets/sunset.png";

        SunsetPic = ImageHelper.LoadFromResource(sunsetPath);
        CherryBlossomPic = ImageHelper.LoadFromResource(cherryPath);
        _navigationService = navigationService;
    }

    public Bitmap? CherryBlossomPic { get; }
    public Bitmap? SunsetPic { get; }

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