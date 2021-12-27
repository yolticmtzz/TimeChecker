using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class TimeCheckerViewModel : INotifyPropertyChanged
    {
        //Utilities
        private readonly TimeWatch mainTimeWatch;
        private readonly TimeWatch breakTimeWatch;
        private readonly Employee user;


        //General Data
        private string userFullName;
        public string UserFullName
        {
            get => userFullName;
            set
            {
                _ = user.Fullname;
                RaisePropertyChanged();
            }
        }
        public string Date { get; set; }

        private Status status;
        public Status Status
        {
            get => status;

            set
            {
                status = value;
                this.RaisePropertyChanged();
            }
        }
    

        //UI Data
        private string statusScreenText;
        public string StatusScreenText
        {
            get => statusScreenText;
            set
            {
                switch (Status)
                {
                    case Status.CheckedIn:
                        value = "Checked In!";
                        RaisePropertyChanged();
                        break;
                    case Status.CheckedOut:
                        value = "Ready To Check In!";
                        RaisePropertyChanged();
                        break;
                    case Status.BreakMode:
                        value = "Break Mode On!";
                        RaisePropertyChanged();
                        break;
                }

            }
        }
        private string checkInButtonText;
        public string CheckInButtonText
        { get => checkInButtonText;
            set
            {
                switch (Status)
                {
                    case Status.CheckedIn:
                        value = "Check Out!";
                        RaisePropertyChanged();
                        break;
                    case Status.CheckedOut:
                        value = "Check In!";
                        RaisePropertyChanged();
                        break;
                    case Status.BreakMode:
                        value = "Break Mode";
                        RaisePropertyChanged();
                        break;
                }

            }
        }
        private string breakButtonText;
        public string BreakButtonText
        {
            get => breakButtonText;
            set
            {
                switch (Status)
                {
                    case Status.BreakMode:
                        value = "Stop Break";
                        RaisePropertyChanged();
                        break;
                    case Status.CheckedIn:
                        value = "Take a Break!";
                        RaisePropertyChanged();
                        break;
                    default:
                        value = "";
                        RaisePropertyChanged();
                        break;
                }
            }
        }
        public string MainTimeWatchScreen { get => mainTimeWatch.CurrentTime; }
        public string BreakTimeWatchScreen { get => breakTimeWatch.CurrentTime; }

        public TimeCheckerViewModel()
        {
            Date = DateTime.Now.ToLongDateString();
            Status = Status.CheckedOut;
            user = new Employee();
            mainTimeWatch = new TimeWatch();
            breakTimeWatch = new TimeWatch();

            mainTimeWatch.TickEvent += mainTimeWatch.TimeWatchTrigger;
            breakTimeWatch.TickEvent += breakTimeWatch.TimeWatchTrigger;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
            {
            if (!string.IsNullOrEmpty(propertyName))
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
        }


    }
}
