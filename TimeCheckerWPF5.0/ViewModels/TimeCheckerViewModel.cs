using System;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Views;
using Microsoft.EntityFrameworkCore;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.DBOperations;

namespace TimeCheckerWPF5._0.ViewModels
{

    public class TimeCheckerViewModel : ViewModelBase
    {    
        TimeSpanRecord MainTimeSpanRecord { get; set; }
        TimeSpanRecord BreakTimeSpanRecord { get; set; }
        private readonly ElapsedTimeSpanListStore _elapsedTimeSpanListService;
        
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
                    CheckInCommand.RaiseCanExecuteChanged();
                    BreakCommand.RaiseCanExecuteChanged();
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


        public TimeCheckerViewModel(UserStore userStore, ElapsedTimeSpanListStore elapsedTimeSpanListService)
        {
            _userStore = userStore;
            _elapsedTimeSpanListService = elapsedTimeSpanListService;

            InitiateCheckInCommand();
            InitiateBreakCommand();

            MainTimeWatch = new TimeWatch();
            BreakTimeWatch = new TimeWatch();
            
            //Subscribing the MainTimeWatch and the BreakTimeWatch to the TickEvent delegate
            MainTimeWatch.TickEvent += MainTimewatchTriggered;
            BreakTimeWatch.TickEvent += BreakTimewatchTriggered;

            Status = Status.CheckedOut;
     
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

        public DelegateCommand CheckInCommand { get; set; }
        private void InitiateCheckInCommand()
        {
            CheckInCommand = new DelegateCommand(
         (o) => Status != Status.BreakMode,
         (o) =>
             {
                 TimeCatch = DateTime.Now;

                 if (Status == Status.CheckedOut)
                 {
                         Status = Status.CheckedIn;
                         MainTimeWatch.StopwatchStart();
                         MainTimeSpanRecord = new TimeSpanRecord(TimeSpanType.MainTime, TimeCatch, _userStore.CurrentUser.Fullname);
                         new TimeEntryAddDBOperation(1, TimeCatch, _userStore.CurrentUser.Fullname);
                 }
                 else
                 {
                         MainTimeWatch.StopwatchStop();
                 
                         bool checkout = OpenCheckOutDialog();

                         if (checkout == true)
                         {
                             Status = Status.CheckedOut;
                             MainTimeWatchScreen = MainTimeWatch.StopwatchReset();
                             MainTimeSpanRecord.EndDateTime = TimeCatch;
                             _elapsedTimeSpanListService.AddTimeSpanRecord(MainTimeSpanRecord);
                         new TimeEntryAddDBOperation(2, TimeCatch, Comment, _userStore.CurrentUser.Fullname);
                         }
                          else
                         {
                             MainTimeWatch.StopwatchStart();
                         }

                }
             }
         );
        }

        public DelegateCommand BreakCommand { get; set; }
        private void InitiateBreakCommand()
        {
            BreakCommand = new DelegateCommand(
         (o) => Status != Status.CheckedOut,
         (o) =>
         {

             TimeCatch = DateTime.Now;

             if (Status == Status.CheckedIn)
             {
                 Status = Status.BreakMode;
                 MainTimeWatch.StopwatchStop();
                 MainTimeSpanRecord.EndDateTime = TimeCatch;
                 _elapsedTimeSpanListService.AddTimeSpanRecord(MainTimeSpanRecord);
                 _ = new TimeEntryAddDBOperation(3, TimeCatch, _userStore.CurrentUser.Fullname);

                 BreakTimeWatch.StopwatchStart();
                 BreakTimeSpanRecord = new TimeSpanRecord(TimeSpanType.BreakTime, TimeCatch, _userStore.CurrentUser.Fullname);
             }
             else
             {
                 Status = Status.CheckedIn;
                 BreakTimeWatchScreen = BreakTimeWatch.StopwatchReset();
                 BreakTimeSpanRecord.EndDateTime = TimeCatch;
                 _elapsedTimeSpanListService.AddTimeSpanRecord(BreakTimeSpanRecord);
                 
                 MainTimeSpanRecord = new TimeSpanRecord(TimeSpanType.MainTime, TimeCatch, _userStore.CurrentUser.Fullname);
                 MainTimeWatch.StopwatchStart();
                 _ = new TimeEntryAddDBOperation(4, TimeCatch, _userStore.CurrentUser.Fullname);
             }
         }

        );

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
        private void MainTimewatchTriggered(object? sender, TickEventArgs e)
            {
                var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    e.TimeSpan.Hours, e.TimeSpan.Minutes, e.TimeSpan.Seconds);
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