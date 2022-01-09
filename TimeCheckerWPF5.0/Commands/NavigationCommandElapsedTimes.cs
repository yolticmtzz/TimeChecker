using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    public class NavigationCommandElapsedTimes : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ElapsedTimeSpanListStoreService _elapsedTimeSpanListService;
   

        public NavigationCommandElapsedTimes(NavigationStore navigationStore, ElapsedTimeSpanListStoreService elapsedTimeSpanListService)
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
