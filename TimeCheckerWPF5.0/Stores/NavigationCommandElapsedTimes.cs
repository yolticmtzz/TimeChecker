using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    public class NavigationCommandElapsedTimes : CommandBase
    {
        //Singleton Pattern.
        // ElapsedTimeViewModel viewModel = *viewModel ablegen
        private ElapsedTimesViewModel _elapsedTimesViewModel;
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
                var newViewModel = new ElapsedTimesViewModel();
                _navigationStore.CurrentViewModel = newViewModel;
                _elapsedTimesViewModel = newViewModel;
            }

            else
            {
                _navigationStore.CurrentViewModel = _elapsedTimesViewModel;
            }
        }

    }
}
