using System;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Services
{


    /// Summary:
    ///     A navigations service specifically for the LayoutViewModel that pulls the data of 3 different views
    ///     in a viewmodel:
    ///         HeaderView - HeaderViewModel (always present)
    ///         NavigationView - NavigationViewModel (always present)
    ///         Whatever is currently shown as the View - CurrentViewModel 
    ///            - LoginViewModel/LoginView,
    ///            - TimeCheckerView/ViewModel,
    ///            - ElapsedTimeViewModel/ElapsedTimeView
    public class LayoutNavigationService<TViewModel> : INavigationService 
        where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Func<HeaderViewModel> _createHeaderViewModel;
        private readonly Func<NavigationViewModel> _createNavigationViewModel;

        public LayoutNavigationService(NavigationStore navigationStore,
            Func<TViewModel> createViewModel,
            Func<HeaderViewModel> createHeaderViewModel,
            Func<NavigationViewModel> createNavigationViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createHeaderViewModel = createHeaderViewModel;
            _createNavigationViewModel = createNavigationViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new LayoutViewModel(_createNavigationViewModel(), _createHeaderViewModel(), _createViewModel());
        }
    }
}
