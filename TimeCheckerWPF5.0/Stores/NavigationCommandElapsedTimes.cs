using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    public class NavigationCommandElapsedTimes : CommandBase
    {
        //Singleton Pattern.
        // ElapsedTimeViewModel viewModel = *viewModel ablegen
        private ViewModelBase _elapsedTimesViewModel;
        private NavigationStore _navigationStore;

        //public NavigationCommandElapsedTimes()
        //{
        //}

        public NavigationCommandElapsedTimes(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {

            if (_elapsedTimesViewModel is null)
            {
                _elapsedTimesViewModel = new ElapsedTimesViewModel();
                _navigationStore.CurrentViewModel = _elapsedTimesViewModel;
            }

            else
            {
                _navigationStore.CurrentViewModel = _elapsedTimesViewModel;
            }
        }

    }
}
