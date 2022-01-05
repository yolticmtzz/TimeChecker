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
        private ViewModelBase _timeCheckerViewModel;


        public NavigationCommandTimeChecker(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            if (_timeCheckerViewModel is null)
            {
                _navigationStore.CurrentViewModel = new TimeCheckerViewModel();
                _timeCheckerViewModel = _navigationStore.CurrentViewModel;


            }

            else
            {
                _navigationStore.CurrentViewModel = _timeCheckerViewModel;
            }

        }
    }

}
