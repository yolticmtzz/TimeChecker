using System.Collections.ObjectModel;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class ElapsedTimesViewModel : ViewModelBase
    {

        //private readonly ObservableCollection<ElapsedTimeSpanList> _elapsedMainTimeSpans;
        //public ObservableCollection<ElapsedTimeSpanList> ElapsedMainTimeSpans => _elapsedMainTimeSpans;
        private readonly ElapsedTimeSpanListStore _elapsedTimeSpanListStore;

        private readonly ObservableCollection<TimeSpanRecord> _elapsedTimeList;
        public ObservableCollection<TimeSpanRecord> ElapsedTimeSpanList => _elapsedTimeList;


        public ElapsedTimesViewModel(ElapsedTimeSpanListStore elapsedTimeSpan)
        {
            _elapsedTimeSpanListStore = elapsedTimeSpan;
            _elapsedTimeList = _elapsedTimeSpanListStore.ElapsedTimeSpanList;

        }

    }
}
