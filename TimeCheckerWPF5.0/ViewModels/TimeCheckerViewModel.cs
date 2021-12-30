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

namespace TimeCheckerWPF5._0.ViewModels
{
    public class TimeCheckerViewModel : INotifyPropertyChanged
    {

        //ApplicationDbContext _context = 

        //General Data
        private Employee user;
        public string UserFullName { get; set; }
        public string Date { get; set; }

        private Status status;
        public Status Status
        {
            get => status;

            set
            {
                status = value;
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

        public string MainTimeWatchScreen
        {
            get => "00:00:00";

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

        public string BreakTimeWatchScreen
        {
            get => "00:00:00";
        }



        public TimeCheckerViewModel()
        {
            initiateCheckInCommand();
            initiateBreakCommand();
            
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

        public DelegateCommand CheckInCommand { get; set; }
        private void initiateCheckInCommand()
        {
            CheckInCommand = new DelegateCommand(
         (o) => Status != Status.BreakMode,
         (o) =>
         {
             if (Status == Status.CheckedOut)
             {
                 Status = Status.CheckedIn;
             }
             else
             {
                 Status = Status.CheckedOut;
             }
         }
        );
        }
        public DelegateCommand BreakCommand { get; set; }
        private void initiateBreakCommand()
        {
            BreakCommand = new DelegateCommand(
         (o) => Status != Status.CheckedOut,
         (o) =>
         {
             if (Status == Status.CheckedIn)
             {
                 Status = Status.BreakMode;
             }
             else
             {
                 Status = Status.CheckedIn;
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

    }
}