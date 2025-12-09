using SimulationFIN31.Services;

namespace SimulationFIN31.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

        private readonly NavigationService _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainWindowViewModel(NavigationService navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.NavigateTo(new HomeViewModel());
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    
}
