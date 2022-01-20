using System;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{

    public class NavigationStore
    {

        public event Action CurrentViewModelChanged;

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


        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
