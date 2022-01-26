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
    ///     This ViewModel reprents the NavigationView. It manages everything related to navigate around the application
    ///     
    ///     This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged.
    ///     
    ///     It implements 3 INavigationServices to navigate around the application
    ///     It implements 4 ICommands by ButtonClicks, 3 use the INavigationServices to navigate around the application
    ///     and one exits the app.
    ///     
    ///     The ViewModel relies on:
    ///         - loginNavigationService
    ///         - timeCheckerNavigationService
    ///         - elapsedTimesNavigationService
    ///         
    /// </summary>
    public class NavigationViewModel: ViewModelBase
    {
        private readonly INavigationService _loginNavigationService;
        private readonly INavigationService _timeCheckerNavigationService;
        private readonly INavigationService _elapsedTimesNavigationService;
        private readonly UserStore _userStore;

        public DelegateCommand NavigateLoginCommand { get; }
        public DelegateCommand NavigateTimeCheckerCommand { get; }
        public DelegateCommand NavigateElapsedTimeSpansCommand { get; }
        public DelegateCommand NavigateExitApplicationCommand { get; }

        /// <summary>
        ///     Initializes a new instance of a NavigationViewModel and initializes 
        ///     the INavigationServices and the ICommands
        /// 
        /// <paramref name="loginNavigationService">
        /// injects the INavigationService to the LoginView/ViewModel
        /// </paramref>
        /// <paramref name="timeCheckerNavigationService">
        /// injects the INavigationService to the TimeCheckerView/TimeCheckerViewModel
        /// </paramref>
        /// <paramref name="elapsedTimesNavigationService">
        /// injects the INavigationService to the ElapsedTimesView/ElapsedTimesViewModel
        /// </paramref>
        ///     
        ///</summary>
        public NavigationViewModel(
            INavigationService loginNavigationService,
            INavigationService timeCheckerNavigationService,
            INavigationService elapsedTimesNavigationService,
            UserStore userStore
           )
        {
            _loginNavigationService = loginNavigationService;
            _timeCheckerNavigationService = timeCheckerNavigationService;
            _elapsedTimesNavigationService = elapsedTimesNavigationService;
            _userStore = userStore;
            NavigateLoginCommand = new DelegateCommand(CanExecuteNavigateToCommand, ExecuteNavigateToLoginCommand);
            NavigateTimeCheckerCommand = new DelegateCommand(CanExecuteNavigateToCommand, ExecuteNavigateToTimeCheckerCommand);
            NavigateElapsedTimeSpansCommand = new DelegateCommand(CanExecuteNavigateToCommand, ExecuteNavigateToElapsedTimesCommand);
            NavigateExitApplicationCommand = new DelegateCommand(ExecuteExitApplication);
            _userStore.CurrentUserChanged = CurrentUserChanged;

        }

        /// <summary>
        ///     Delivers the CanExecute criterias for the ICommand Predicate
        ///
        /// <paramref name="obj">
        /// the "NavigateTo" buttons clicked to run the command
        /// </paramref>
        ///       
        /// </summary>
        private bool CanExecuteNavigateToCommand(object obj)
        {
            return _userStore.CurrentUser != null; 
        }

        /// <summary>
        ///     A help method to run RaiseCanExecuteChanged on the NavigateToCommands
        ///     so they get accessible after the CurrentUser isn't null anymore
        ///     (ICommand predicate changed)
        ///       
        /// </summary>
        private void CurrentUserChanged()
        {
            NavigateTimeCheckerCommand.RaiseCanExecuteChanged();
            NavigateElapsedTimeSpansCommand.RaiseCanExecuteChanged();
            NavigateLoginCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Shutdown the Application.
        ///
        /// <paramref name="obj">
        /// the "Exit" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        private void ExecuteExitApplication(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Navigate to LoginViewModel
        ///
        /// <paramref name="obj">
        /// the "Change User" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        public void ExecuteNavigateToLoginCommand(object obj)
        {
            _loginNavigationService.NavigateToType(typeof(LoginViewModel));
        }

        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Navigate to TimeCheckerViewModel
        ///
        /// <paramref name="obj">
        /// the "TimeChecker" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        public void ExecuteNavigateToTimeCheckerCommand(object obj)
        {
         _timeCheckerNavigationService.NavigateToType(typeof(TimeCheckerViewModel));

        }


        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Navigate to ElapsedTimeViewModel
        ///
        /// <paramref name="obj">
        /// the "Overview" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        public void ExecuteNavigateToElapsedTimesCommand(object obj)
        {
            _elapsedTimesNavigationService.NavigateToType(typeof(ElapsedTimesViewModel));
        }

    }



}
