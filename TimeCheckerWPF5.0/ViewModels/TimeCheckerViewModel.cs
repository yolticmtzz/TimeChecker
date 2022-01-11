using System;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Views;
using Microsoft.EntityFrameworkCore;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{

    public class TimeCheckerViewModel : ViewModelBase
    {
        readonly ApplicationDbContext _context = new(new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
       .Options);

        TimeSpanRecord TimeSpanRecord { get; set; }
        private readonly ElapsedTimeSpanListStore _elapsedTimeSpanListService;
        private DateTime TimeCatch { get; set; }
        
        public string _comment;
        public string Comment
        {
            get => _comment;

            set
            {
                    _comment = value;
                    RaisePropertyChanged();
            }

        }

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

        public TimeWatch MainTimeWatch { get; set; }

        private string _mainTimeWatchScreen = "00:00:00";
        public string MainTimeWatchScreen
        {
            get => _mainTimeWatchScreen;

            set
            {
                    _mainTimeWatchScreen = value;
                    RaisePropertyChanged();
            }

        }

        private string _breakButtonText;
        public string BreakButtonText
        {
            get => _breakButtonText;
            set
            {
                    _breakButtonText = value;
                    RaisePropertyChanged();
            }
        }
        private string _breakButtonColor;
        public string BreakButtonColor
        {
            get => _breakButtonColor;
            set
            {
                    _breakButtonColor = value;
                    RaisePropertyChanged();
            }
        }

        public TimeWatch BreakTimeWatch { get; set; }

        private string _breakTimeWatchScreen = "00:00:00";
        
        public string BreakTimeWatchScreen
        {
            get => _breakTimeWatchScreen;

            set
            {
                    _breakTimeWatchScreen = value;
                    RaisePropertyChanged();
            }
        }

        public TimeCheckerViewModel(ElapsedTimeSpanListStore elapsedTimeSpanListService)
        {
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
                 TimeSpanRecord = new TimeSpanRecord();
                 TimeSpanRecord.StartDateTime = TimeCatch;
             Insert(1, TimeCatch);

             }
             else
             {
                 MainTimeWatch.StopwatchStop();
                 
                 bool checkout = OpenCheckOutDialog();

                 if (checkout == true)
                 {
                     Status = Status.CheckedOut;
                     MainTimeWatchScreen = MainTimeWatch.StopwatchReset();
                     TimeSpanRecord.EndDateTime = TimeCatch;
                     _elapsedTimeSpanListService.AddTimeSpanRecord(TimeSpanRecord);
                     Insert(2, TimeCatch);
                     Comment = String.Empty;
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
                 
                 Insert(3, TimeCatch);

                 BreakTimeWatch.StopwatchStart();

             }
             else
             {
                 Status = Status.CheckedIn;
                 BreakTimeWatchScreen = BreakTimeWatch.StopwatchReset();
                 MainTimeWatch.StopwatchStart();

                 Insert(4, TimeCatch);
             }
         }

        );

        }

        private void Insert(short type, DateTime timeCatch)
        { 
            var record = new Timeentry()
            {
                Type = type,
                DateTime = timeCatch,
                Comment = Comment,
                User = "",
            };

            _context.Timeentry.Add(record);
            _context.SaveChanges();
        }

        private void UpdateGUIProperties()
        {
            switch (Status)
            {
                case Status.CheckedIn:
                    StatusScreenText = "Checked In!";
                    MainTimeButtonText = "Check Out";
                    BreakButtonText = "Start Break";
                    BreakButtonColor = "Blue";
                    MainTimeButtonColor = "Red";
                    break;
                case Status.CheckedOut:
                    StatusScreenText = "Ready To Check In!";
                    MainTimeButtonText = "Check In";
                    MainTimeButtonColor = "Green";
                    BreakButtonColor = "LightGray";
                    BreakButtonText = "Breakmode";
                    break;
                case Status.BreakMode:
                    StatusScreenText = "Break Mode On!";
                    BreakButtonColor = "DarkBlue";
                    BreakButtonText = "Stop Break";
                    MainTimeButtonColor = "LightGray";
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