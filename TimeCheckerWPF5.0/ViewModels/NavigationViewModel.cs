using System;
using System.Windows.Input;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Services;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// Summary:
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
    public class NavigationViewModel: ViewModelBase
    {
        private readonly INavigationService _loginNavigationService;
        private readonly INavigationService _timeCheckerNavigationService;
        private readonly INavigationService _elapsedTimesNavigationService;

        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigateTimeCheckerCommand { get; }
        public ICommand NavigateElapsedTimeSpansCommand { get; }
        public ICommand NavigateExitApplicationCommand { get; }

        /// Summary:
        ///     Initializes a new instance of a NavigationViewModel and initializes 
        ///     the INavigationServices and the ICommands
        /// 
        /// Parameters:
        ///   loginNavigationService:
        ///     injects the INavigationService to the LoginView/ViewModel
        ///   timeCheckerNavigationService:
        ///     injects the INavigationService to the TimeCheckerView/TimeCheckerViewModel
        ///   elapsedTimesNavigationService:
        ///     injects the INavigationService to the ElapsedTimesView/ElapsedTimesViewModel
        public NavigationViewModel(
            INavigationService loginNavigationService,
            INavigationService timeCheckerNavigationService,
            INavigationService elapsedTimesNavigationService
           )
        {
            _loginNavigationService = loginNavigationService;
            _timeCheckerNavigationService = timeCheckerNavigationService;
            _elapsedTimesNavigationService = elapsedTimesNavigationService;
            NavigateLoginCommand = new DelegateCommand(ExecuteNavigateToLoginCommand);
            NavigateTimeCheckerCommand = new DelegateCommand(ExecuteNavigateToTimeCheckerCommand);
            NavigateElapsedTimeSpansCommand = new DelegateCommand(ExecuteNavigateToElapsedTimesCommand);
            NavigateExitApplicationCommand = new DelegateCommand(ExecuteExitApplication);
            
        }

        /// Summary:
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Shutdown the Application.
        ///
        /// Parameters:
        ///     obj:
        ///       the "Exit" button clicked to run the command
        private void ExecuteExitApplication(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// Summary:
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Navigate to LoginViewModel
        ///
        /// Parameters:
        ///     obj:
        ///       the "Change User" button clicked to run the command
        public void ExecuteNavigateToLoginCommand(object obj)
        {
            _loginNavigationService.Navigate();
        }

        /// Summary:
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Navigate to TimeCheckerViewModel
        ///
        /// Parameters:
        ///     obj:
        ///       the "TimeChecker" button clicked to run the command
        public void ExecuteNavigateToTimeCheckerCommand(object obj)
        {
            _timeCheckerNavigationService.Navigate();
        }

        /// Summary:
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Navigate to ElapsedTimeViewModel
        ///
        /// Parameters:
        ///     obj:
        ///       the "Overview" button clicked to run the command
        public void ExecuteNavigateToElapsedTimesCommand(object obj)
        {
            _elapsedTimesNavigationService.Navigate();
        }

    }

}
