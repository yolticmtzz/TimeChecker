using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{

    /// Summary:
    ///     This ViewModel reprents the MainView and what ultimately is shown. It manages the data contxt binding to the MainView
    ///     and thus what is shown in the UI.
    ///     
    ///     This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged.
    ///     Also it registers to the OnCurrentViewModelChanged Event to update the UI if the currentViewModel was changed,
    ///     what means a navigation from one view to another took place.
    ///     
    ///     The ViewModel relies on the NavigationStore
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        /// Summary:
        ///     Initializes a new instance of a MainViewModel and initializes 
        ///     the navigationStore also registers to the OnCurrentViewModelChanged Event if the 
        ///     current ViewModel in the NavigationStore changed.
        /// 
        /// Parameters:
        ///   navigationStore:
        ///     injects the NavigationStore
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        /// Summary:
        ///     Raises the RaisePropertyChanged with the CurrentViewModel so the UI updates its presentation to
        ///     the new View.
        private void OnCurrentViewModelChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
