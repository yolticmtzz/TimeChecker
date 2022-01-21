using System;
using TimeCheckerWPF5._0.Commands;
using TimeCheckerWPF5._0.Views;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.Utilities;
using TimeCheckerWPF5._0.Services;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.ViewModels
{

    public enum Status
    {
        CheckedIn,
        CheckedOut,
        BreakMode,
    }

    public class TimeCheckerViewModel : ViewModelBase
    {
        TimeSpanRecord MainTimeSpanRecord { get; set; }
        TimeSpanRecord BreakTimeSpanRecord { get; set; }

        private readonly ElapsedTimeSpanListStore _elapsedTimeSpanListService;

        private readonly DataBaseService _dataBaseService;
        private readonly UserStore _userStore;
        private DateTime TimeCatch { get; set; }

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
        public TimeWatch MainTimeWatch { get; set; }
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
        public TimeWatch BreakTimeWatch { get; set; }
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


        public TimeCheckerViewModel(UserStore userStore, ElapsedTimeSpanListStore elapsedTimeSpanListService, DataBaseService dataBaseService)
        {
            _userStore = userStore;
            _elapsedTimeSpanListService = elapsedTimeSpanListService;
            _dataBaseService = dataBaseService;
            Status = Status.CheckedOut;

            CheckInCommand = new DelegateCommand(CanExecuteCheckinCommand, ExecuteCheckinCommand);
            BreakCommand = new DelegateCommand(CanExecuteBreakCommand, ExecuteBreakCommand);

            MainTimeWatch = new TimeWatch();
            BreakTimeWatch = new TimeWatch();

            //Subscribing the MainTimeWatch and the BreakTimeWatch to the TickEvent delegate
            MainTimeWatch.TickEvent += MainTimewatchTriggered;
            BreakTimeWatch.TickEvent += BreakTimewatchTriggered;
        }

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

                if  (GetCheckOutComment())
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

        private bool GetCheckOutComment()
        {
            MainTimeWatch.StopwatchStop();
            return OpenCheckOutDialog();
        }

        private bool OpenCheckOutDialog()
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

            SetEndBreakStatus();

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

        private void SetEndBreakStatus()
        {
            Status = Status.CheckedIn;
            BreakTimeWatchScreen = BreakTimeWatch.StopwatchReset();
            BreakTimeSpanRecord.EndDateTime = TimeCatch;
            _elapsedTimeSpanListService.AddTimeSpanRecord(BreakTimeSpanRecord);

            MainTimeSpanRecord = new TimeSpanRecord(TimeSpanType.MainTime, TimeCatch, _userStore.CurrentUser.Fullname);
            MainTimeWatch.StopwatchStart();
            _dataBaseService.AddTimeEntry(4, TimeCatch, _userStore.CurrentUser.Fullname);
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

        //Access the Timewatch Events to trigger, since its subscribed to the delegate
        // -> The MainTimeWatch Textbox is to be updated with as a running timewatch in the defined DispatchTimers interval
        private void MainTimewatchTriggered(object? sender, TickEventArgs tickEvent)
        {
            var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}", tickEvent.TimeSpan.Hours, tickEvent.TimeSpan.Minutes, tickEvent.TimeSpan.Seconds);
            MainTimeWatchScreen = CurrentTime;
        }

        //Access the Timewatch Events to trigger, since its subscribed to the delegate
        // -> The BreakTimeWatch Textbox is to be updated with as a running timewatch in the defined DispatchTimers interval
        private void BreakTimewatchTriggered(object? sender, TickEventArgs e)
        {
            var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}",
                e.TimeSpan.Hours, e.TimeSpan.Minutes, e.TimeSpan.Seconds);
            BreakTimeWatchScreen = CurrentTime;
        }

    }

}