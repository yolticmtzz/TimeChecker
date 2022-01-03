using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class ElapsedTimesViewModel : ViewModelBase
    {

        public ElapsedTimeSpan CurrentTimeSpan { get; set; }

        private readonly ObservableCollection<ElapsedTimeSpan> _elapsedMainTimeSpans;
        public ObservableCollection<ElapsedTimeSpan> ElapsedMainTimeSpans => _elapsedMainTimeSpans;
       
        private readonly ObservableCollection<ElapsedTimeSpan> _elapsedBreakTimeSpans;

        public ObservableCollection<ElapsedTimeSpan> ElapsedBreakTimeSpans => _elapsedBreakTimeSpans;


        public ElapsedTimesViewModel()
        {
            _elapsedMainTimeSpans = new ObservableCollection<ElapsedTimeSpan>();
            _elapsedBreakTimeSpans = new ObservableCollection<ElapsedTimeSpan>();

        }



        public void AddTimeSpan(ObservableCollection<ElapsedTimeSpan> elapsedTimeSpanList)
        {
            elapsedTimeSpanList.Add(CurrentTimeSpan);
        }

    }
}
