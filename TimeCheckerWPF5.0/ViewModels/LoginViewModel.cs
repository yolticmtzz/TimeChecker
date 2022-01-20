using System;
using System.Windows;
using System.Windows.Input;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.DBOperations;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _prename;
        public string Prename
        {
            get => _prename;

            set
            {
                _prename = value;
                RaisePropertyChanged(nameof(Prename));
            }
        }

        private string _lastName;
        public string Lastname
        {
            get => _lastName;

            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(Lastname));
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(UserStore userStore, INavigationService loginNavigationService)
        {
            CheckUserDBList();
            LoginCommand = new LoginCommand(this, userStore, loginNavigationService);
        }

        private void CheckUserDBList()
        {
            var employeeDBList = new EmployeeGetDBOperation();

            if (employeeDBList.EmployeesDBList.Count == 0)
            {
                MessageBox.Show("There don't seem to be any users who could log in yet. \nPlease create at least one valid user to use the TimeChecker ");
                Application.Current.Shutdown();
            }
        }

    }
}