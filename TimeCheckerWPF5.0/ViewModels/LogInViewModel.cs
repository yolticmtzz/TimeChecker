using System;
using System.Windows;
using System.Windows.Input;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{

    /// <summary>
    ///     This ViewModel processes the LoginView, manipulates its data and implements the necessary functionalities
    ///     
    ///     This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged.
    ///     
    ///     The ViewModel relies on:
    ///         - The UserStore to check the user during the login process
    ///           and if successful to set the user as current user,
    ///         - A navigation service to navigate after the login process
    ///         - A DataBaseService to check the possible users during login.
    ///         - A LoginCommand for the ButtonClick.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private readonly UserStore _userStore;
        private readonly INavigationService _navigationService;
        private readonly DataBaseService _dataBaseService;

        public DelegateCommand LoginCommand { get; set; }

        private string _prename;
        public string Prename
        {
            get => _prename;    

            set
            {
                _prename = value;
                RaisePropertyChanged(nameof(Prename));
                LoginCommand.RaiseCanExecuteChanged();
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
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of a LoginViewModel and initializes 
        /// the LoginCommand, the userStore, the navigationService and the dataBaseService
        ///     
        /// With CheckUserDBList, the ViewModel directly checks if there are any Emplyees stored in the database. 
        /// Because without any, the application can not be used (it doesn't run user-less) and therefore is shut down. 
        /// 
        /// <paramref name="userStore">
        /// injects the UserStore
        /// </paramref>
        /// 
        /// <paramref name="loginNavigationService">
        /// injects a INavigationService
        /// </paramref>
        /// 
        /// <paramref name="dataBaseService">
        /// injects the DataBaseService
        /// </paramref>
        /// 
        ///     
        /// </summary>
        public LoginViewModel(UserStore userStore, 
                                INavigationService loginNavigationService,
                                DataBaseService dataBaseService)
        {
            
            LoginCommand = new DelegateCommand(CanExecuteLoginCommand, ExecuteLoginCommand);
            _userStore = userStore;
            _navigationService = loginNavigationService;
            _dataBaseService = dataBaseService;
            CheckUserDBList();
        }

        /// <summary>
        ///     Delivers the CanExecute criterias for the ICommand Predicate
        ///
        /// <paramref name="obj">:
        ///   the "Start" button clicked to run the command
        ///   </paramref>
        ///   
        /// </summary>     
        private bool CanExecuteLoginCommand(object obj)
        {
            var check = !string.IsNullOrEmpty(Prename) && !string.IsNullOrEmpty(Lastname);
            return check;
        }

        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     After the button is clicked, Firstname and Lastname are compared
        ///     to a equal Employees in the Database. If a Employee with the Same Pre- and Lastname exists,
        ///     login is success, the employee is set as the current user and then 
        ///     the application navigates to the TimeCheckerView. 
        ///     If no Employee was found, an error message is shown in a message box and the login-procedure stopped.
        ///
        /// <paramref name="obj">
        /// the "Start" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        private void ExecuteLoginCommand(object obj)
        {
            var EmployeeDBList = _dataBaseService.GetEmployees();
            bool isUserExist = false;
            var LogginPrename = Prename.Trim();
            var LoginLastname = Lastname.Trim();

            foreach (Employee employee in EmployeeDBList)
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
                _navigationService.NavigateToType(typeof(TimeCheckerViewModel));
                return;
            }
  
                MessageBox.Show($"The user \"{LogginPrename} {LoginLastname}\" does not exist." +
                $"\nPlease try again or create a new user online.");
                Prename = "";
                Lastname = "";
        }

        /// <summary>
        ///     Checks if there are any Employees stored in the database.
        ///     If not, the user is informed about it and the Application is shot down.
        ///     Otherwise it continues
        ///     
        ///     </summary>
        private void CheckUserDBList()
        {
            var EmployeeDBList = _dataBaseService.GetEmployees();

            if (EmployeeDBList.Count == 0)
            {
                MessageBox.Show("There don't seem to be any users who could log in yet. \nPlease create at least one valid user to use the TimeChecker ");
                Application.Current.Shutdown();
            }
        }

    }
}