using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Models;
using System.Windows.Input;
using TimeCheckerWPF5._0.Views;
using TimeCheckerWPF5._0.Stores;
using Microsoft.EntityFrameworkCore;

namespace TimeCheckerWPF5._0.ViewModels
{

    public class TimeCheckerViewModel : ViewModelBase
    {

        //DBContext
                ApplicationDbContext _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options);

        //General Data
        private readonly Employee _user;
        public string UserFullName { get; set; }
        public string Date { get; set; }
        ElapsedTimesView _elapsedTimesView;
        ElapsedTimesViewModel elapsedTimesViewModel;



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

        public int EntryType { get; set; }
        public DateTime EntryDate { get; set; }

        //UI Data
        //Status Dialog
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

        //MainTime Button and Watch
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

        //BreakTime Button and Watch
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



        public TimeCheckerViewModel()
        {

            InitiateCheckInCommand();
            InitiateBreakCommand();

            MainTimeWatch = new TimeWatch();
            BreakTimeWatch = new TimeWatch();
         

            //Subscribing the MainTimeWatch and the BreakTimeWatch to the TickEvent delegate
            MainTimeWatch.TickEvent += MainTimewatchTriggered;
            BreakTimeWatch.TickEvent += BreakTimewatchTriggered;

            Date = DateTime.Now.ToLongDateString();
            Status = Status.CheckedOut;
            _user = new Employee("Dummy", "User 77");
            UserFullName = _user.Fullname;
            _elapsedTimesView = new ElapsedTimesView();
            elapsedTimesViewModel = ((ElapsedTimesViewModel)_elapsedTimesView.DataContext);
        }


        private bool OpenCheckOutDialog()
        {
            CheckOutDialogWindow dialog = new CheckOutDialogWindow();
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
         if (Status == Status.CheckedOut)
         {
             Status = Status.CheckedIn;
             MainTimeWatch.StopwatchStart();
             elapsedTimesViewModel.CurrentTimeSpan = new ElapsedTimeSpan(DateTime.Now, "MainTime");
             Insert(1);



             }
             else
             {
                 MainTimeWatch.StopwatchStop();
                 elapsedTimesViewModel.CurrentTimeSpan.EndDateTime = DateTime.Now;

                 bool checkout = OpenCheckOutDialog();

                 if (checkout == true)
                 {
                     Status = Status.CheckedOut;
                     MainTimeWatchScreen = MainTimeWatch.StopwatchReset();
                     elapsedTimesViewModel.AddTimeSpan(elapsedTimesViewModel.ElapsedMainTimeSpans);
                     Insert(2);

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
             if (Status == Status.CheckedIn)
             {
                 Status = Status.BreakMode;
                 MainTimeWatch.StopwatchStop();
                 elapsedTimesViewModel.CurrentTimeSpan.EndDateTime = DateTime.Now;
                 elapsedTimesViewModel.AddTimeSpan(elapsedTimesViewModel.ElapsedMainTimeSpans);
                 Insert(3);

                 BreakTimeWatch.StopwatchStart();
                 elapsedTimesViewModel.CurrentTimeSpan = new ElapsedTimeSpan(DateTime.Now, "BreakTime");
             }
             else
             {
                 Status = Status.CheckedIn;
                 BreakTimeWatchScreen = BreakTimeWatch.StopwatchReset();
                 elapsedTimesViewModel.CurrentTimeSpan.EndDateTime = DateTime.Now;
                 elapsedTimesViewModel.AddTimeSpan(elapsedTimesViewModel.ElapsedBreakTimeSpans);
                 MainTimeWatch.StopwatchStart();
                 elapsedTimesViewModel.CurrentTimeSpan = new ElapsedTimeSpan(DateTime.Now, "MainTime");
                 Insert(4);

             }
         }
        );
        }

        private void Insert(short type)
        { 
            var record = new Timeentry()
            {
                Type = type,
                DateTime = DateTime.Now,
                Comment = Comment,
                User = _user.Fullname,
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