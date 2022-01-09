using System.Windows.Input;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ElapsedTimeSpanListStoreService _elapsedTimeListService;

        public ICommand NavigateTimeCheckerCommand { get; }
        public ICommand NavigateElapsedTimeSpansCommand { get; }
        public ICommand NavigateExitApplicationCommand { get; }

        public NavigationViewModel(NavigationStore navigationStore, ElapsedTimeSpanListStoreService elapsedTimeSpanListService)
        {
            _navigationStore = navigationStore;
            _elapsedTimeListService = elapsedTimeSpanListService;

            NavigateTimeCheckerCommand = new NavigateCommand<TimeCheckerViewModel>(_navigationStore, () => new TimeCheckerViewModel(_elapsedTimeListService));
            NavigateElapsedTimeSpansCommand = new NavigateCommand<ElapsedTimesViewModel>(_navigationStore, () => new ElapsedTimesViewModel(_elapsedTimeListService.ElapsedTimeSpanList));
            NavigateExitApplicationCommand = new NavigateExitApplicationCommand();

        }

    }
     
}
