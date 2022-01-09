using System;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Stores
{
    public class NavigateCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }

    }
}