using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    public class NavigationCommand : CommandBase
    {
        private readonly ElapsedTimeSpanListStoreService _elapsedTimeSpanListService;
        private readonly NavigationStore _navigationStore;

        public NavigationCommand(NavigationStore navigationStore, ElapsedTimeSpanListStoreService elapsedTimeSpanListService)
        {
            _navigationStore = navigationStore;
            _elapsedTimeSpanListService = elapsedTimeSpanListService;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new ElapsedTimesViewModel(_elapsedTimeSpanListService.ElapsedTimeSpanList);
        }
        
    }
}
