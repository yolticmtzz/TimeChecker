using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{

    public enum ViewType
    {
        TimeChecker,
        ElapsedTimes,
        Help,

    }

    public class NavigationStore
    {



        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
