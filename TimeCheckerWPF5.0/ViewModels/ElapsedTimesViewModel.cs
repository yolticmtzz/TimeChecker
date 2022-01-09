using System.Collections.ObjectModel;
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class ElapsedTimesViewModel : ViewModelBase
    {

        //private readonly ObservableCollection<ElapsedTimeSpanList> _elapsedMainTimeSpans;
        //public ObservableCollection<ElapsedTimeSpanList> ElapsedMainTimeSpans => _elapsedMainTimeSpans;
       
        private readonly ObservableCollection<TimeSpanRecord> _elapsedTimeList;
        public ObservableCollection<TimeSpanRecord> ElapsedTimeSpanList => _elapsedTimeList;


        public ElapsedTimesViewModel(ObservableCollection<TimeSpanRecord> elapsedTimeSpan)
        {
            _elapsedTimeList = elapsedTimeSpan;

        }

    }
}
