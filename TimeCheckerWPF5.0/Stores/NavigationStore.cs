using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    public class NavigationStore
    {

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
            }
        }


    }
}
