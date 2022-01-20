using System;
using System.Collections.ObjectModel;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.Utilities;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class ElapsedTimesViewModel : ViewModelBase
    {

        private readonly ElapsedTimeSpanListStore _elapsedTimeSpanListStore;

        public TimeSpan TotalMainTimeSpans { get; set; }
        public TimeSpan TotalBreakTimeSpans { get; set; }

        public ObservableCollection<TimeSpanRecord> ElapsedMainTimeSpanList { get; private set; }

        public ObservableCollection<TimeSpanRecord> ElapsedBreakTimeSpanList { get; private set; }

        public ElapsedTimesViewModel(ElapsedTimeSpanListStore elapsedTimeSpan)
        {
            _elapsedTimeSpanListStore = elapsedTimeSpan;
            ElapsedMainTimeSpanList = _elapsedTimeSpanListStore.ElapsedMainTimeSpanList;
            ElapsedBreakTimeSpanList = _elapsedTimeSpanListStore.ElapsedBreakTimeSpanList;
            TotalMainTimeSpans = CalculateTotalTimeSpans(ElapsedMainTimeSpanList);
            TotalBreakTimeSpans = CalculateTotalTimeSpans(ElapsedBreakTimeSpanList);
        }

        public static TimeSpan CalculateTotalTimeSpans(ObservableCollection<TimeSpanRecord> elapsedTimeSpanList)
        {
            TimeSpan Total = TimeSpan.Zero;
            foreach (TimeSpanRecord item in elapsedTimeSpanList)
            {
                Total = Total + item.Duration;
            }

            return Total;
        }

        
    }
}
