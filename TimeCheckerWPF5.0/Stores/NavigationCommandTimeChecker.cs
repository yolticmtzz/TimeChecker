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
        private NavigationStore navigationStore;

        // private readonly NavigationStore _navigationStore;

        //public NavigationCommandTimeChecker()
        //{
        //   // _navigationStore = navigationStore;
        //}

        public NavigationCommandTimeChecker(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
           navigationStore.CurrentViewModel = new TimeCheckerViewModel();
        }
        
    }
}
