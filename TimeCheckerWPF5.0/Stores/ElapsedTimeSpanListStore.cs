using System.Collections.ObjectModel;
using TimeCheckerWPF5._0.Utilities;

namespace TimeCheckerWPF5._0.Stores
{

    /// Summary:
    ///    A service that stores two ObservableCollections of TimeSpanRecords
    ///     - ElapsedMainTimeSpanList: Holds TimeSpanRecords of TimeSpanType "BreakTime"
    ///     - ElapsedBreakTimeSpanList: Holds TimeSpanRecords of TimeSpanType "BreakTime"
    ///    
    ///     Both lists are filled by interactions through the TimeCheckerViewModel and
    ///     presented via ElapsedTimeViewModel to the ElapsedTimeView     
    public class ElapsedTimeSpanListStore
    {
        public ObservableCollection<TimeSpanRecord> ElapsedBreakTimeSpanList;
        public ObservableCollection<TimeSpanRecord> ElapsedMainTimeSpanList;

        /// Summary:
        /// Initializes a new instance of the store and initalizes its both ObservableCollections
        public ElapsedTimeSpanListStore()
        {
            ElapsedMainTimeSpanList = new ObservableCollection<TimeSpanRecord>();
            ElapsedBreakTimeSpanList = new ObservableCollection<TimeSpanRecord>();
        }

        /// Summary:
        /// Implements adding TimeSpanRecords to the ObservableCollections. 
        /// Based on the type of the TimeSpanRecord the correct ObservableCollections is selected and filled.
        /// Differentiates between "MainTime" and "BreakTime", 
        /// i.e. depending on whether working time or break time is to be stored.
        ///         
        /// Parameters:
        ///   timeSpanRecord:
        ///     a timeSpanRecord that should be added to the list
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















