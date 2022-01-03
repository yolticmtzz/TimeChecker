using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    public class NavigationCommandElapsedTimes : CommandBase
    {

        public NavigationCommandElapsedTimes()
        {
        }

        public override void Execute(object parameter)
        {
           //NavigationStore.CurrentViewModel = new ElapsedTimesViewModel();
        }

    }
}
