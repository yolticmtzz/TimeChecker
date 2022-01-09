using System.Windows.Input;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {

        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigateTimeCheckerCommand { get; }
        public ICommand NavigateElapsedTimeSpansCommand { get; }
        public ICommand NavigateExitApplicationCommand { get; }

        public NavigationViewModel(NavigationStore navigationStore, ElapsedTimeSpanListStoreService elapsedTimeSpanListService)
        {

            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(new Services.NavigationService<LoginViewModel>(
                navigationStore, () => new LoginViewModel(navigationStore, elapsedTimeSpanListService)));

            NavigateTimeCheckerCommand = new NavigateCommand<TimeCheckerViewModel>(new Services.NavigationService<TimeCheckerViewModel>(
                navigationStore, () => new TimeCheckerViewModel(elapsedTimeSpanListService)));

            NavigateElapsedTimeSpansCommand = new NavigateCommand<ElapsedTimesViewModel>(new Services.NavigationService<ElapsedTimesViewModel>(
                navigationStore, () => new ElapsedTimesViewModel(elapsedTimeSpanListService.ElapsedTimeSpanList)));

            NavigateExitApplicationCommand = new ExitApplicationCommand();

        }

    }
     
}
