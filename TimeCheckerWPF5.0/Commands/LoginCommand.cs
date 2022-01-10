using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Commands
{
    public class LoginCommand : CommandBase
    {

        private readonly LoginViewModel _viewModel;
        private readonly INavigationService _navigationService;
        private readonly UserStore _userStore;

        public LoginCommand(LoginViewModel viewModel, UserStore userStore, INavigationService navigationService)
        {
            _viewModel = viewModel;
            _userStore = userStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {

            Employee employee = new Employee("", "")
            {
                Prename = _viewModel.Firstname,
                Lastname = _viewModel.Lastname,
            };

            MessageBox.Show($"Logging in {_viewModel.Firstname} {_viewModel.Lastname}...");

            _userStore.CurrentUser = employee;
            _navigationService.Navigate();
            
        }
    }
}
