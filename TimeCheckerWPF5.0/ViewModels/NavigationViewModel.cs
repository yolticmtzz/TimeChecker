using System.Windows.Input;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {
        private readonly NavigationStore navigationStore;

        public ICommand ShowTimeCheckerCommand { get; }
        public ICommand ShowElapsedTimesCommand { get; }
        public ICommand ExitCommand { get; }

        public NavigationViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            ShowTimeCheckerCommand = new NavigationCommandTimeChecker(navigationStore);
            ShowElapsedTimesCommand = new NavigationCommandElapsedTimes(navigationStore);
            ExitCommand = new ExitCommand();

        }

    }
     
}
