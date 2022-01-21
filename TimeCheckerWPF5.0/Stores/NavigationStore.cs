﻿using System;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    /// Summary:
    ///    A service that stores the current ViewModel assigned to the content in the presented view
    ///    since this application uses UserControls as presenter.
    ///    
    ///    Because current ViewModel can change, it has an event "CurrentViewModelChanged"
    ///    that implements and invokes the OnCurrentViewModelChanged.
    ///    This method updates the view shown according to the current viewmodel change.
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

        /// Summary:
        ///    This method updates the view shown according to the current viewmodel change.
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
