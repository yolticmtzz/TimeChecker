using System.Windows.Input;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ElapsedTimeSpanListStoreService _elapsedTimeListService;

        public ICommand ShowTimeCheckerCommand { get; }
        public ICommand ShowElapsedTimesCommand { get; }
        public ICommand ExitCommand { get; }

        public NavigationViewModel(NavigationStore navigationStore, ElapsedTimeSpanListStoreService elapsedTimeSpanListService)
        {
            _navigationStore = navigationStore;
            _elapsedTimeListService = elapsedTimeSpanListService;

            ShowTimeCheckerCommand = new NavigationCommandTimeChecker(_navigationStore, _elapsedTimeListService);
            ShowElapsedTimesCommand = new NavigationCommandElapsedTimes(_navigationStore, _elapsedTimeListService);
            ExitCommand = new ExitCommand();

        }

    }
     
}
