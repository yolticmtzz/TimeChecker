using System.Windows;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.DBOperations;
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

            //Employee employee = new Employee(_viewModel.Firstname, _viewModel.Lastname);

            var EmployeeDB = new EmployeeGetDBOperation();
            bool isUserExist = false;
            var LogginPrename = _viewModel.Prename.Trim();
            var LoginLastname = _viewModel.Lastname.Trim();

            foreach (Employee employee in EmployeeDB.EmployeesDBList)
            {
                if (employee.Prename.Equals(LogginPrename) && employee.Lastname.Equals(LoginLastname))
                {
                    _userStore.CurrentUser = employee;
                    isUserExist = true;
                    break;
 
                }
            }

            if (isUserExist)
            {
                MessageBox.Show($"Logging in {LogginPrename} {LoginLastname}...");
                _navigationService.Navigate();
            }
            else
            {
                MessageBox.Show($"The user \"{LogginPrename} {LoginLastname}\" does not exist." +
                $"\nPlease try again or create a new user online.");
                _viewModel.Prename = "";
                _viewModel.Lastname = "";
            }

        }
    }
}


