using System.Windows.Input;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _firstname;
        public string Firstname
        {
            get => _firstname;

            set
            {
                _firstname = value;
                RaisePropertyChanged(nameof(Firstname));
            }
        }

        private string _lastName;
        public string Lastname
        {
            get => _lastName;

            set
            {
                _firstname = value;
                RaisePropertyChanged(nameof(Lastname));
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(UserStore userStore, INavigationService loginNavigationService)
        {
            LoginCommand = new LoginCommand(this, userStore, loginNavigationService);
        }



    }
}