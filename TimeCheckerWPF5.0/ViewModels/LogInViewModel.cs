using System;
using System.Windows.Input;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public LoginViewModel(UserStore userStore, NavigationStore navigationStore, ElapsedTimeSpanListStoreService elapsedTimeSpanListStoreService)
        {

            NavigationService<TimeCheckerViewModel> navigationService = new NavigationService<TimeCheckerViewModel>(
                navigationStore,
                () => new TimeCheckerViewModel(userStore, elapsedTimeSpanListStoreService));
            
            
            LoginCommand = new LoginCommand(this, userStore, navigationService);
        }

        public ICommand LoginCommand { get; set; }

    }
}