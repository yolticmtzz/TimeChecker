using System;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Views;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.Utilities;
using TimeCheckerWPF5._0.Services;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// <summary>
    ///   3 enums to distinguish between the 3 possible status the TimeChecker ever can be
    ///  </summary>
    public enum Status
    {
        CheckedIn,
        CheckedOut,
        BreakMode,
    }

    /// <summary>
    ///     This ViewModel is the core of the application.
    ///     It represents a check-in system that can assume three different states.
    ///     Either one is checked in, checked out or in break mode.
    ///     While checked in, the main time is running and stored, which can, for example, represent the working time.
    ///     While checked out, no time is measured
    ///     While in break mode, only the BreakTime is running and measured, which represents the break time.
    ///     
    ///     Based on the state, the design changes the entire UI of the TimeChecker. For this reason it has many properties
    ///     Furthermore the whole TimeChecker is controlled with ButtonClickCommands
    ///     
    ///     This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged.
    ///        
    ///     The ViewModel relies on:
    ///         - ElapsedTimeSpanListService: 
    ///             because it manages the add of TimeSpanRecord to the lists thru interactions on the ui
    ///         - DataBaseService:
    ///             because it uses DataBaseServices to interact with the database thru interactions on the ui
    ///         - UserStore:
    ///             because it has to know which user is currently interacting on the ui
    /// </summary>
    public class TimeCheckerViewModel : ViewModelBase
    {
        ///Injections as services to use
        private readonly ElapsedTimeSpanListStore _elapsedTimeSpanListStore;
        private readonly DataBaseService _dataBaseService;
        private readonly UserStore _userStore;

        ///The TimeSpanRecords used and 
        ///send to the ElapsedTimeView for the presentation to the UI
        ///as an Overview of "elapsed times" during runtime
        TimeSpanRecord MainTimeSpanRecord { get; set; }
        TimeSpanRecord BreakTimeSpanRecord { get; set; }         
        
        DateTime TimeCatch { get; set; }
        
        public string Comment { get; set; }

        /// <summary>
        /// The status is the most important property in this class. 
        /// Depending on the status the whole implementation and the presentation in the UI reacts.
        /// Therefore the following must be done after each set call:
        ///       - call RaisPropertyChanged to inform the UI that the status has changed.
        ///       - call UpdateGUIProperty to adjust the UI according to the status.
        /// </summary>
        private Status _status;
        public Status Status
        {
            get => _status;

            set
            {
                _status = value;
                RaisePropertyChanged();
                CheckInCommand.RaiseCanExecuteChanged();
                BreakCommand.RaiseCanExecuteChanged();
                UpdateGUIProperties();
            }
        }

        ///UI Properties
        ///The all have to use RaisePropertyChanged()
        ///in their set to notify the UI about the change
        private string _statusScreenText;
        public string StatusScreenText
        {
            get => _statusScreenText;
            set
            {
                _statusScreenText = value;
                RaisePropertyChanged();
            }
        }

        private string _mainTimeButtonText;
        public string MainTimeButtonText
        {
            get => _mainTimeButtonText;
            set
            {
                _mainTimeButtonText = value;
                RaisePropertyChanged();
            }
        }

        private string _mainTimeButtonColor;
        public string MainTimeButtonColor
        {
            get => _mainTimeButtonColor;
            set
            {
                _mainTimeButtonColor = value;
                RaisePropertyChanged();
            }
        }

        private string _mainTimeButtonBorderColor;
        public string MainTimeButtonBorderColor
        {
            get => _mainTimeButtonBorderColor;
            set
            {
                _mainTimeButtonBorderColor = value;
                RaisePropertyChanged();
            }
        }

        private string _mainTimeWatchScreen;
        public string MainTimeWatchScreen
        {
            get => _mainTimeWatchScreen;

            set
            {
                _mainTimeWatchScreen = value;
                RaisePropertyChanged();
            }

        }

        private string _breakTimeButtonText;
        public string BreakTimeButtonText
        {
            get => _breakTimeButtonText;
            set
            {
                _breakTimeButtonText = value;
                RaisePropertyChanged();
            }
        }

        private string _breakTimeButtonColor;
        public string BreakTimeButtonColor
        {
            get => _breakTimeButtonColor;
            set
            {
                _breakTimeButtonColor = value;
                RaisePropertyChanged();
            }
        }

        private string _breakTimeButtonBorderColor;
        public string BreakTimeButtonBorderColor
        {
            get => _breakTimeButtonBorderColor;
            set
            {
                _breakTimeButtonBorderColor = value;
                RaisePropertyChanged();
            }
        }
        
        private string _breakTimeWatchScreen;
        public string BreakTimeWatchScreen
        {
            get => _breakTimeWatchScreen;

            set
            {
                _breakTimeWatchScreen = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand CheckInCommand { get; set; }
        public DelegateCommand BreakCommand { get; set; }

        /// <summary>
        ///     Initializes a new instance of a TimeCheckerViewModel and initializes 
        ///     the UserStore, the ElapsedTimeSpanListStore and the DataBaseService
        ///     
        ///     Initializes the CheckInCommand and the BreakCommand
        ///     
        ///     Initliazses two TimeWatches (MainTIme and BreakTime) and 
        ///     registers the TickEvents to the TimeWatchTriggers
        /// 
        /// <paramref name="userStore">
        /// injects the UserStore
        /// </paramref>
        /// 
        /// <paramref name="elapsedTimeSpanListStore">
        /// injects the ElapsedTimeSpanListSTore
        /// </paramref>
        /// 
        /// <paramref name="dataBaseService">
        /// injects the DataBaseService
        /// </paramref>
        ///     
        ///  </summary>
        public TimeCheckerViewModel(UserStore userStore, 
                                    ElapsedTimeSpanListStore elapsedTimeSpanListStore,
                                    DataBaseService dataBaseService)
        {
            _userStore = userStore;
            _elapsedTimeSpanListStore = elapsedTimeSpanListStore;
            _dataBaseService = dataBaseService;
            
            CheckInCommand = new DelegateCommand(CanExecuteCheckinCommand, ExecuteCheckinCommand);
            BreakCommand = new DelegateCommand(CanExecuteBreakCommand, ExecuteBreakCommand);

            MainTimeWatch = new TimeWatch();
            MainTimeWatch.TickEvent += MainTimewatchTriggered;

            BreakTimeWatch = new TimeWatch();
            BreakTimeWatch.TickEvent += BreakTimewatchTriggered;

            Status = Status.CheckedOut;
        }


        /// <summary>
        ///     Delivers the CanExecute criterias for the ICommand Predicate
        ///
        /// <paramref name="obj">
        /// the "CheckInCommand" button clicked to run the command
        /// </paramref>
        ///       
        /// </summary>
        private bool CanExecuteCheckinCommand(object obj)
        {
            return Status != Status.BreakMode;
        }

        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Captures the DateTime.Now from System. 
        ///     Based on the current Status it either checks in or out
        ///     and performs the dependent tasks/methods
        ///
        /// <paramref name="obj">:
        /// the "CheckInCommand" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        private void ExecuteCheckinCommand(object obj)
        {
            {
                TimeCatch = DateTime.Now;

                if (Status == Status.CheckedOut)
                {
                    SetCheckedInStatus(false);
                    return;
                }

                if  (IsCheckOutCommentSet())
                {
                    SetCheckedOutStatus();
                    return;
                }

                MainTimeWatch.StopwatchStart();

            }
        }

        /// <summary>
        ///     Sets the CheckIn status and performs all dependent tasks:
        ///         - Starts the MainTime stopwatch
        ///         - Creates a new MainTimeSpanRecord 
        ///         - Writes a "CheckIn" time entry into the database
        ///         
        /// <paramref name="isEndingBreak">
        ///     - defines whether a initial checkin or an after-break-checkin (re-checkin) is conducted
        /// </paramref>
        /// </summary>
        private void SetCheckedInStatus(bool isEndingBreak)
        {
            Status = Status.CheckedIn;
            MainTimeWatch.StopwatchStart();
            MainTimeSpanRecord = new TimeSpanRecord(TimeSpanType.MainTime, TimeCatch, _userStore.CurrentUser.Fullname);

            if (isEndingBreak == false) _dataBaseService.AddTimeEntry(1, TimeCatch, _userStore.CurrentUser.Fullname);
            
        }

        /// <summary>
        ///     Stops the MainTime stopwatch and opens and waits for the response of the
        ///     "CheckOutComment Dialog".
        ///     This asks if the user really wants to check out and if so,
        ///     whether a comment should be added to the time entry.
        ///     
        /// <returns>
        /// From ShowCheckOutDialog() a boolean True or False for "CheckOut"
        /// </returns>
        /// 
        /// </summary>
        private bool IsCheckOutCommentSet()
        {
            MainTimeWatch.StopwatchStop();
            return ShowCheckOutDialog();
        }

        /// <summary>
        ///     Shows the "CheckOutComment Dialog" as a pop-up window.
        ///     
        /// <returns>
        ///  True If CheckOut nutton clicked, false if Cancel button clicked
        /// </returns>
        /// 
        /// </summary>
        private bool ShowCheckOutDialog()
        {
            CheckOutDialogWindow dialog = new();
            if (dialog.ShowDialog() == true)
            {
                var checkOutDialogViewModel = ((CheckOutDialogViewModel)dialog.DataContext);
                Comment = checkOutDialogViewModel.DialogComment;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Sets the CheckOut status and performs all dependent tasks:
        ///         - Resets the MainTimeWatch and the Screen
        ///         - Closes the MainTimeSpanRecord with an EndDate
        ///         - Adds the MainTimeSpanRecord to the ElapsedTimeSpanStore
        ///         - Writes a "CheckOut" time entry into the database
        ///         - Resets the Comment string to empty;
        /// </summary>
        private void SetCheckedOutStatus()
        {
                Status = Status.CheckedOut;
                MainTimeWatchScreen = MainTimeWatch.StopwatchReset();
                MainTimeSpanRecord.EndDateTime = TimeCatch;
                _elapsedTimeSpanListStore.AddTimeSpanRecord(MainTimeSpanRecord);
                _dataBaseService.AddTimeEntry(2, TimeCatch, _userStore.CurrentUser.Fullname, Comment);
                Comment = string.Empty;
        }

        /// <summary>
        ///     Delivers the CanExecute criterias for the ICommand Predicate
        ///
        /// <paramref name="obj">:
        /// the "BreakCommand" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        private bool CanExecuteBreakCommand(object obj)
        {
            return Status != Status.CheckedOut;
        }

        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     Captures the DateTime.Now from System. 
        ///     Based on the current Status it either activates the BreakMode or checks back in
        ///     and performs the dependent tasks/methods
        ///
        /// <paramref name="obj">
        /// the "BreakCommand" button clicked to run the command
        /// </paramref>
        ///       
        /// </summary>
        private void ExecuteBreakCommand(object obj)
        {
            TimeCatch = DateTime.Now;

            if (Status == Status.CheckedIn)
            {
                EndCheckInMode();
                SetBreakModeStatus();
                return;
            }

            EndBreakMode();
            SetCheckedInStatus(true);
        }

        /// <summary>
        ///     Ends the CheckIn activites and performs its dependent tasks:
        ///         - Stops the MainTimeWatch and the Screen
        ///         - Closes the MainTimeSpanRecord with an EndDate
        ///         - Adds the MainTimeSpanRecord to the ElapsedTimeSpanStore
        /// </summary>
        private void EndCheckInMode()
        {
            MainTimeWatch.StopwatchStop();
            MainTimeSpanRecord.EndDateTime = TimeCatch;
            _elapsedTimeSpanListStore.AddTimeSpanRecord(MainTimeSpanRecord);
        }

        /// <summary>
        ///     Ends the BreakMode activites and performs its dependent tasks:
        ///         - Resets the BreakTimeWatch and the Screen
        ///         - Closes the BreakTimeSpanRecord with an EndDate
        ///         - Adds the BreakTimeSpanRecord to the ElapsedTimeSpanStore
        ///         - Writes a "BreakEnd" time entry into the database
        /// </summary>
        private void EndBreakMode()
        {
            BreakTimeWatchScreen = BreakTimeWatch.StopwatchReset();
            BreakTimeSpanRecord.EndDateTime = TimeCatch;
            _elapsedTimeSpanListStore.AddTimeSpanRecord(BreakTimeSpanRecord);
            _dataBaseService.AddTimeEntry(4, TimeCatch, _userStore.CurrentUser.Fullname);
        }


        /// <summary>
        /// Sets the BreakMode status and performs all dependent tasks:
        ///         - Starts the BreakTime stopwatch
        ///         - Creates a new BreakTimeSpanRecord 
        ///         - Writes a "BreakStart" time entry into the database
        /// </summary>
        private void SetBreakModeStatus()
        {
            Status = Status.BreakMode;
            BreakTimeWatch.StopwatchStart();
            BreakTimeSpanRecord = new TimeSpanRecord(TimeSpanType.BreakTime, TimeCatch, _userStore.CurrentUser.Fullname);
            _dataBaseService.AddTimeEntry(3, TimeCatch, _userStore.CurrentUser.Fullname);
        }

        /// <summary>
        ///     Updates the GUI Properties based on the Status
        ///     Executed whenever the status changes (in the property setter).
        /// </summary>
        private void UpdateGUIProperties()
        {
            switch (Status)
            {
                case Status.CheckedIn:
                    StatusScreenText = "Checked In!";
                    MainTimeButtonText = "Check Out";
                    BreakTimeButtonText = "Start Break";
                    BreakTimeButtonColor = "Blue";
                    BreakTimeButtonBorderColor = "Navy";
                    MainTimeButtonColor = "Red";
                    MainTimeButtonBorderColor = "Firebrick";
                    break;
                case Status.CheckedOut:
                    StatusScreenText = "Ready To Check In!";
                    MainTimeButtonText = "Check In";
                    MainTimeButtonColor = "LightGreen";
                    MainTimeButtonBorderColor = "Green";
                    BreakTimeButtonColor = "LightGray";
                    BreakTimeButtonBorderColor = "DarkGray";
                    BreakTimeButtonText = "Breakmode";
                    break;
                case Status.BreakMode:
                    StatusScreenText = "Break Mode On!";
                    BreakTimeButtonColor = "DarkBlue";
                    BreakTimeButtonBorderColor = "Midnightblue";
                    BreakTimeButtonText = "Stop Break";
                    MainTimeButtonColor = "LightGray";
                    MainTimeButtonBorderColor = "DarkGray";
                    break;
            }
        }

 
        public TimeWatch MainTimeWatch { get; set; }
        /// <summary>
        ///     uses the argument "tickEvent" from the parameter to write
        ///     the TimeSpan Arg value formatted to the MainTimeWatchSceen in the UI.
        ///     thus a ticking digital clock is displayed
        ///     
        /// <paramref name="sender"> The registered TimeWatch </paramref>
        /// <paramref name="tickEvent"> the stopwatch.elapsed TimeSpan Arg </paramref>
           ///     
        /// </summary>
        private void MainTimewatchTriggered(object? sender, TickEventArgs tickEvent)
        {
            var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}", tickEvent.TimeSpan.Hours, tickEvent.TimeSpan.Minutes, tickEvent.TimeSpan.Seconds);
            MainTimeWatchScreen = CurrentTime;
        }

        public TimeWatch BreakTimeWatch { get; set; }
        /// <summary>
        ///     uses the argument "tickEvent" from the parameter to write
        ///     the TimeSpan Arg value formatted to the BreakTimeWatchSceen in the UI.
        ///     thus a ticking digital clock is displayed
        ///     
        /// <paramref name="sender"> The registered TimeWatch </paramref>
        /// <paramref name="tickEvent"> the stopwatch.elapsed TimeSpan Arg </paramref>
        ///     
        /// </summary>
        private void BreakTimewatchTriggered(object? sender, TickEventArgs tickEvent)
        {
            var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}",
                tickEvent.TimeSpan.Hours, tickEvent.TimeSpan.Minutes, tickEvent.TimeSpan.Seconds);
            BreakTimeWatchScreen = CurrentTime;
        }

    }

}