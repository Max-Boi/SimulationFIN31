using System;
using Microsoft.Extensions.DependencyInjection;
using SimulationFIN31.Services;
using SimulationFIN31.Services.Interfaces;
using SimulationFIN31.ViewModels;

namespace SimulationFIN31;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<SettingsViewModel>();
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<SimulationViewModel>();
        collection.AddTransient<HomeViewModel>();
        collection.AddSingleton<INavigationService, NavigationService>();    
        collection.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));
    }
}