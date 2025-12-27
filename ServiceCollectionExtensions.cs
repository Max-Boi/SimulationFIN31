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
        // ViewModels
        collection.AddSingleton<SettingsViewModel>();
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<SimulationViewModel>();
        collection.AddTransient<HomeViewModel>();

        // Navigation
        collection.AddSingleton<INavigationService, NavigationService>();

        // Weight Calculation Pipeline
        collection.AddSingleton<IFactorNormalizer, FactorNormalizer>();
        collection.AddSingleton<IInfluenceCalculator, InfluenceCalculator>();
        collection.AddSingleton<IEventWeightCalculator, EventWeightCalculator>();
        collection.AddSingleton<ICopingTriggerChecker, CopingTriggerChecker>();
        collection.AddSingleton<IWeightedRandomService, WeightedRandomService>();

        // Simulation Services
        collection.AddSingleton<IIllnessManagerService, IllnessManagerService>();
        collection.AddSingleton<ISimulationService, SimulationService>();

        collection.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));
    }
}