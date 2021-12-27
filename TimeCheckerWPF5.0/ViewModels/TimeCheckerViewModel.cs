using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class TimeCheckerViewModel : INotifyPropertyChanged
    {

        public TimeWatch mainTimeWatch;
        public TimeWatch breakTimeWatch;
        public Employee user;

        public event PropertyChangedEventHandler PropertyChanged;

        public string User {get => user.Fullname;}
        public string Date { get; set; }
        public string Status { get; set; }

        public string CheckInButtonText {get; set;}
        public string BreakButtonText { get; set; }

        public string MainTimeWatchScreen {get => mainTimeWatch.CurrentTime;}
        public string BreakTimeWatchScreen{ get => breakTimeWatch.CurrentTime;}

        public TimeCheckerViewModel()
        {
            Date = DateTime.Now.ToLongDateString();
            Status = "Ready to Check In";
            CheckInButtonText = "Check In";
            BreakButtonText = "Start Break";

            mainTimeWatch = new TimeWatch();
            breakTimeWatch = new TimeWatch();
            user = new Employee();
            mainTimeWatch.TickEvent += mainTimeWatch.TimeWatchTrigger;
            breakTimeWatch.TickEvent += breakTimeWatch.TimeWatchTrigger;
        }




    }
}
