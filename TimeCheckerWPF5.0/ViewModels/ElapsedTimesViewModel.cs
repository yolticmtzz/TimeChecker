using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class ElapsedTimesViewModel : INotifyPropertyChanged
    {

        public ElapsedTimeSpan CurrentTimeSpan { get; set; }

        private List<ElapsedTimeSpan> _elapsedMainTimeSpans;
        public List<ElapsedTimeSpan> ElapsedMainTimeSpans
        { 
            get => _elapsedMainTimeSpans;

            set
            {
                if (value != _elapsedMainTimeSpans)
                {
                    _elapsedMainTimeSpans = value;
                    RaisePropertyChanged();
                }
            }
        }

        private List<ElapsedTimeSpan> _elapsedBreakTimeSpans;

        public List<ElapsedTimeSpan> ElapsedBreakTimeSpans
        {
            get => _elapsedBreakTimeSpans;

            set
            {
                if (value != _elapsedBreakTimeSpans)
                {
                    _elapsedBreakTimeSpans = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ElapsedTimesViewModel()
        {
            ElapsedMainTimeSpans = new List<ElapsedTimeSpan>();
            ElapsedBreakTimeSpans = new List<ElapsedTimeSpan>();

        }
        


        public void AddTimeSpan(List<ElapsedTimeSpan> elapsedTimeSpanList)
        {
            elapsedTimeSpanList.Add(CurrentTimeSpan);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
