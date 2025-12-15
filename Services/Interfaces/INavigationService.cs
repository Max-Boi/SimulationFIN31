using System.ComponentModel;
using SimulationFIN31.ViewModels;

namespace SimulationFIN31.Services.Interfaces;

public interface INavigationService : INotifyPropertyChanged
{
    ViewModelBase CurrentViewModel { get; }
    void NavigateTo<T>() where T : ViewModelBase;
}