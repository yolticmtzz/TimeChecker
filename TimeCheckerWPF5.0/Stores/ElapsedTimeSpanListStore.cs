using System.Collections.ObjectModel;
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.Stores
{
    public class ElapsedTimeSpanListStore
    {
        private readonly ObservableCollection<TimeSpanRecord> _elapsedTimeSpanList;
        public ObservableCollection<TimeSpanRecord> ElapsedTimeSpanList => _elapsedTimeSpanList;

        public ElapsedTimeSpanListStore()
        {
            _elapsedTimeSpanList = new ObservableCollection<TimeSpanRecord>();

        }


        public void AddTimeSpanRecord(TimeSpanRecord timeSpanRecord)
        {
            ElapsedTimeSpanList.Add(timeSpanRecord);
        }

    }

}















