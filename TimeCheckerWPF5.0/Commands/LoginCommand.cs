using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Commands
{
    public class LoginCommand : CommandBase
    {

        private readonly LoginViewModel _viewModel;
        private readonly NavigationService<TimeCheckerViewModel> _navigationService;

        public LoginCommand(LoginViewModel viewModel, NavigationService<TimeCheckerViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            MessageBox.Show($"Logging in {_viewModel.Firstname} {_viewModel.Lastname}...");

            _navigationService.Navigate();            
            //Navigate to the TimeChecker page
        }
    }
}
