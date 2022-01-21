using System.Windows;
using System.Windows.Input;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private readonly DataBaseService _dataBaseService;

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

        public LoginViewModel(UserStore userStore, INavigationService loginNavigationService, DataBaseService dataBaseService)
        {
            CheckUserDBList();
            LoginCommand = new LoginCommand(this, userStore, loginNavigationService, dataBaseService);
            _dataBaseService = dataBaseService;
        }

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