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

namespace TimeCheckerWPF5._0.ViewModels
{

    public class TimeCheckerViewModel : INotifyPropertyChanged
    {


        //General Data
        private readonly Employee user;
        public string UserFullName { get; set; }
        public string Date { get; set; }
        public string comment;
        public string Comment
        {
            get => comment;

            set
            {
                if (value != comment)
                {
                    comment = value;
                    RaisePropertyChanged();
                }
            }

        }

        private Status status;
        public Status Status
        {
            get => status;

            set
            {
                if (value != status)
                {
                    status = value;
                    RaisePropertyChanged();
                    CheckInCommand.RaiseCanExecuteChanged();
                    BreakCommand.RaiseCanExecuteChanged();
                    UpdateGUIProperties();
                }
            }
        }

        public int EntryType { get; set; }
        public DateTime EntryDate { get; set; }

        //UI Data
        //Status Dialog
        private string statusScreenText;
        public string StatusScreenText
        {
            get => statusScreenText;
            set
            {
                if (statusScreenText != value)
                {
                    statusScreenText = value;
                    RaisePropertyChanged();
                }
            }
        }

        //MainTime Button and Watch
        private string mainTimeButtonText;
        public string MainTimeButtonText
        {
            get => mainTimeButtonText;
            set
            {
                if (mainTimeButtonText != value)
                {
                    mainTimeButtonText = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string mainTimeButtonColor;
        public string MainTimeButtonColor
        {
            get => mainTimeButtonColor;
            set
            {
                if (mainTimeButtonColor != value)
                {
                    mainTimeButtonColor = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TimeWatch MainTimeWatch { get; set; }

        private string mainTimeWatchScreen = "00:00:00";
        public string MainTimeWatchScreen
        {
            get => mainTimeWatchScreen;

            set
            {
                if (value != mainTimeWatchScreen)
                {
                    mainTimeWatchScreen = value;
                    RaisePropertyChanged();
                }
            }

        }

        //BreakTime Button and Watch
        private string breakButtonText;
        public string BreakButtonText
        {
            get => breakButtonText;
            set
            {
                if (breakButtonText != value)
                {
                    breakButtonText = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string breakButtonColor;
        public string BreakButtonColor
        {
            get => breakButtonColor;
            set
            {
                if (breakButtonColor != value)
                {
                    breakButtonColor = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TimeWatch BreakTimeWatch { get; set; }

        private string breakTimeWatchScreen = "00:00:00";
        public string BreakTimeWatchScreen
        {
            get => breakTimeWatchScreen;

            set
            {
                if (value != breakTimeWatchScreen)
                {
                    breakTimeWatchScreen = value;
                    RaisePropertyChanged();
                }
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
            user = new Employee("Dummy", "User 77");
            UserFullName = user.Fullname;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
             }
             else
             {
                 MainTimeWatch.StopwatchStop();
                 bool checkout = OpenCheckOutDialog();

                 if (checkout == true)
                 {
                     Status = Status.CheckedOut;
                     MainTimeWatchScreen = MainTimeWatch.StopwatchReset();

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
                 BreakTimeWatch.StopwatchStart();
             }
             else
             {
                 Status = Status.CheckedIn;
                 BreakTimeWatchScreen = BreakTimeWatch.StopwatchReset();
                 MainTimeWatch.StopwatchStart();
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