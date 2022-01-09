using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    public class NavigationCommandTimeChecker : CommandBase
    {
        private NavigationStore _navigationStore;
        private TimeCheckerViewModel _timeCheckerViewModel;
        private readonly ElapsedTimeSpanListService _elapsedTimeSpanListService;


        public NavigationCommandTimeChecker(NavigationStore navigationStore, ElapsedTimeSpanListService elapsedTimeSpanListService)
        {
            _navigationStore = navigationStore;
            _elapsedTimeSpanListService = elapsedTimeSpanListService;
        }

        public override void Execute(object parameter)
        {
            if (_timeCheckerViewModel is null)
            {
                var newViewModel = new TimeCheckerViewModel(_elapsedTimeSpanListService);
                _navigationStore.CurrentViewModel = newViewModel;
                _timeCheckerViewModel = newViewModel;

            }

            else
            {
                _navigationStore.CurrentViewModel = _timeCheckerViewModel;
            }

        }
    }

}
