using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using SimulationFIN31.Services.Interfaces;
using SimulationFIN31.ViewModels.Util;

namespace SimulationFIN31.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    
    public Bitmap? CherryBlossomPic { get; } 
    public Bitmap? SunsetPic { get; }
    
    private readonly INavigationService _navigationService;

    public HomeViewModel(INavigationService navigationService)
    {
        string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string cherryPath = $"avares://{assemblyName}/Assets/blossom.png";
        string sunsetPath = $"avares://{assemblyName}/Assets/sunset.png";
        
        SunsetPic = ImageHelper.LoadFromResource(sunsetPath);
        CherryBlossomPic = ImageHelper.LoadFromResource(cherryPath);
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