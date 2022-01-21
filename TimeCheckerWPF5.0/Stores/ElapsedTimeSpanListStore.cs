using System.Collections.ObjectModel;
using TimeCheckerWPF5._0.Utilities;

namespace TimeCheckerWPF5._0.Stores
{
    public class ElapsedTimeSpanListStore
    {
        public ObservableCollection<TimeSpanRecord> ElapsedBreakTimeSpanList;

        public ObservableCollection<TimeSpanRecord> ElapsedMainTimeSpanList;

        public ElapsedTimeSpanListStore()
        {
            ElapsedMainTimeSpanList = new ObservableCollection<TimeSpanRecord>();
            ElapsedBreakTimeSpanList = new ObservableCollection<TimeSpanRecord>();

        }

        public void AddTimeSpanRecord(TimeSpanRecord timeSpanRecord)
        {
            if (timeSpanRecord.TimeSpanType == TimeSpanType.MainTime)
            {
                ElapsedMainTimeSpanList.Add(timeSpanRecord);
                return;
            }
            ElapsedBreakTimeSpanList.Add(timeSpanRecord);
        }

    }

}















