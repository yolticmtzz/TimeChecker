using System;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Views;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.Utilities;
using TimeCheckerWPF5._0.Services;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// Summary:
    ///   3 enums to distinguish between the 3 possible status the TimeChecker ever can be
    public enum Status
    {
        CheckedIn,
        CheckedOut,
        BreakMode,
    }

    /// Summary:
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
    public class TimeCheckerViewModel : ViewModelBase
    {
        ///Injections as services to use
        private readonly ElapsedTimeSpanListStore _elapsedTimeSpanListService;
        private readonly DataBaseService _dataBaseService;
        private readonly UserStore _userStore;

        ///The TimeSpanRecords used and 
        ///send to the ElapsedTimeView for the presentation to the UI
        ///as an Overview of "elapsed times" during runtime
        TimeSpanRecord MainTimeSpanRecord { get; set; }
        TimeSpanRecord BreakTimeSpanRecord { get; set; }         
        
        DateTime TimeCatch { get; set; }
        
        public string Comment { get; set; }

        private Status _status;
        public Status Status
        {
            get => _status;

            set
            {
                _status = value;
                RaisePropertyChanged();
                //CheckInCommand.RaiseCanExecuteChanged();
                //BreakCommand.RaiseCanExecuteChanged();
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

        /// Summary:
        ///     Initializes a new instance of a TimeCheckerViewModel and initializes 
        ///     the UserStore, the ElapsedTimeSpanListStore and the DataBaseService
        ///     
        ///     Initializes the CheckInCommand and the BreakCommand
        ///     
        ///     Initliazses two TimeWatches (MainTIme and BreakTime) and 
        ///     registers the TickEvents to the TimeWatchTriggers
        /// 
        /// Parameters:
        ///   userStore:
        ///     injects the UserStore
        ///   elapsedTimeSpanListStore:
        ///     injects the ElapsedTimeSpanListSTore
        ///   dataBaseService:
        ///     injects the DataBaseService
        public TimeCheckerViewModel(UserStore userStore, 
                                    ElapsedTimeSpanListStore elapsedTimeSpanListStore,
                                    DataBaseService dataBaseService)
        {
            _userStore = userStore;
            _elapsedTimeSpanListService = elapsedTimeSpanListStore;
            _dataBaseService = dataBaseService;
            
            Status = Status.CheckedOut;

            CheckInCommand = new DelegateCommand(CanExecuteCheckinCommand, ExecuteCheckinCommand);
            BreakCommand = new DelegateCommand(CanExecuteBreakCommand, ExecuteBreakCommand);

            MainTimeWatch = new TimeWatch();
            MainTimeWatch.TickEvent += MainTimewatchTriggered;

            BreakTimeWatch = new TimeWatch();
            BreakTimeWatch.TickEvent += BreakTimewatchTriggered;
        }


        /// Summary:
        ///     Delivers the CanExecute criterias for the ICommand Predicate
        ///
        /// Parameters:
        ///     obj:
        ///       the "CheckInCommand" button clicked to run the command
        private bool CanExecuteCheckinCommand(object obj)
        {
            return Status != Status.BreakMode;
        }

        private void ExecuteCheckinCommand(object obj)
        {
            {
                TimeCatch = DateTime.Now;

                if (Status == Status.CheckedOut)
                {
                    SetCheckedInStatus();
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

        private void SetCheckedInStatus()
        {
            Status = Status.CheckedIn;
            MainTimeWatch.StopwatchStart();
            MainTimeSpanRecord = new TimeSpanRecord(TimeSpanType.MainTime, TimeCatch, _userStore.CurrentUser.Fullname);
            _dataBaseService.AddTimeEntry(1, TimeCatch, _userStore.CurrentUser.Fullname);
        }

        private bool IsCheckOutCommentSet()
        {
            MainTimeWatch.StopwatchStop();
            return ShowCheckOutDialog();
        }

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

        private void SetCheckedOutStatus()
        {
                Status = Status.CheckedOut;
                MainTimeWatchScreen = MainTimeWatch.StopwatchReset();
                MainTimeSpanRecord.EndDateTime = TimeCatch;
                _elapsedTimeSpanListService.AddTimeSpanRecord(MainTimeSpanRecord);
                _dataBaseService.AddTimeEntry(2, TimeCatch, _userStore.CurrentUser.Fullname, Comment);
                Comment = string.Empty;
        }

        /// Summary:
        ///     Delivers the CanExecute criterias for the ICommand Predicate
        ///
        /// Parameters:
        ///     obj:
        ///       the "BreakCommand" button clicked to run the command
        private bool CanExecuteBreakCommand(object obj)
        {
            return Status != Status.CheckedOut;
        }

        private void ExecuteBreakCommand(object obj)
        {
            TimeCatch = DateTime.Now;

            if (Status == Status.CheckedIn)
            {
                SetBreakModeStatus();
                return;
            }

            EndBreakMode();
            SetCheckedInStatus();
        }

        private void EndBreakMode()
        {
            BreakTimeWatchScreen = BreakTimeWatch.StopwatchReset();
            BreakTimeSpanRecord.EndDateTime = TimeCatch;
            _elapsedTimeSpanListService.AddTimeSpanRecord(BreakTimeSpanRecord);
            _dataBaseService.AddTimeEntry(4, TimeCatch, _userStore.CurrentUser.Fullname);
        }

        private void SetBreakModeStatus()
        {
            Status = Status.BreakMode;
            MainTimeWatch.StopwatchStop();
            MainTimeSpanRecord.EndDateTime = TimeCatch;
            _elapsedTimeSpanListService.AddTimeSpanRecord(MainTimeSpanRecord);
            _dataBaseService.AddTimeEntry(3, TimeCatch, _userStore.CurrentUser.Fullname);

            BreakTimeWatch.StopwatchStart();
            BreakTimeSpanRecord = new TimeSpanRecord(TimeSpanType.BreakTime, TimeCatch, _userStore.CurrentUser.Fullname);
        }

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


        //The thicking timewatch
        public TimeWatch MainTimeWatch { get; set; }
        //Access the Timewatch Events to trigger, since its subscribed to the delegate
        // -> The MainTimeWatch Textbox is to be updated with as a running timewatch in the defined DispatchTimers interval
        private void MainTimewatchTriggered(object? sender, TickEventArgs tickEvent)
        {
            var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}", tickEvent.TimeSpan.Hours, tickEvent.TimeSpan.Minutes, tickEvent.TimeSpan.Seconds);
            MainTimeWatchScreen = CurrentTime;
        }

        public TimeWatch BreakTimeWatch { get; set; }
        //Access the Timewatch Events to trigger, since its subscribed to the delegate
        // -> The BreakTimeWatch Textbox is to be updated with as a running timewatch in the defined DispatchTimers interval
        private void BreakTimewatchTriggered(object? sender, TickEventArgs tickEvent)
        {
            var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}",
                tickEvent.TimeSpan.Hours, tickEvent.TimeSpan.Minutes, tickEvent.TimeSpan.Seconds);
            BreakTimeWatchScreen = CurrentTime;
        }

    }

}