using System.Windows.Input;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {

        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigateTimeCheckerCommand { get; }
        public ICommand NavigateElapsedTimeSpansCommand { get; }
        public ICommand NavigateExitApplicationCommand { get; }

        public NavigationViewModel(NavigationService<TimeCheckerViewModel> timeCheckerNavigationService, NavigationService<ElapsedTimesViewModel> elapsedTimesNavigationService, NavigationService<LoginViewModel> loginNavigationService,
            UserStore userStore, NavigationStore navigationStore, ElapsedTimeSpanListStoreService elapsedTimeSpanListService)
        {

            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(loginNavigationService);
            NavigateTimeCheckerCommand = new NavigateCommand<TimeCheckerViewModel>(timeCheckerNavigationService);
            NavigateElapsedTimeSpansCommand = new NavigateCommand<ElapsedTimesViewModel>(elapsedTimesNavigationService);
            NavigateExitApplicationCommand = new ExitApplicationCommand();
        }

    }
     
}
