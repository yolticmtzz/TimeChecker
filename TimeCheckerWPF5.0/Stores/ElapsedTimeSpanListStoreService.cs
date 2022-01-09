using System.Collections.ObjectModel;

namespace TimeCheckerWPF5._0.Models
{
    public class ElapsedTimeSpanListStoreService
    {
        private readonly ObservableCollection<TimeSpanRecord> _elapsedTimeSpanList;
        public ObservableCollection<TimeSpanRecord> ElapsedTimeSpanList => _elapsedTimeSpanList;

        public ElapsedTimeSpanListStoreService()
        {
            _elapsedTimeSpanList = new ObservableCollection<TimeSpanRecord>();

        }


        public void AddTimeSpanRecord(TimeSpanRecord timeSpanRecord)
        {
            ElapsedTimeSpanList.Add(timeSpanRecord);
        }

    }

}















