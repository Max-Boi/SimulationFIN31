using Microsoft.Extensions.DependencyInjection;
using SimulationFIN31.Services;
using SimulationFIN31.ViewModels;

namespace SimulationFIN31;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
            collection.AddTransient<MainWindowViewModel>();
            collection.AddSingleton<NavigationService>();

    }
}