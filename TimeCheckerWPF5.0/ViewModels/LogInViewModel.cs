using System;
using System.Windows.Input;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public LoginViewModel(NavigationStore navigationStore, ElapsedTimeSpanListStoreService elapsedTimeSpanListStoreService)
        {

            LoginCommand = new LoginCommand(this, new Services.NavigationService<TimeCheckerViewModel>(
                navigationStore, () => new TimeCheckerViewModel(elapsedTimeSpanListStoreService)));
        }

        public ICommand LoginCommand { get; set; }

    }
}