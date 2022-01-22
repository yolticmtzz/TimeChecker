using System;
using System.Collections.ObjectModel;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.Utilities;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// <summary>
    ///     Represents and handles the data presented to the UI by the ElapsedTimeView.
    ///     This is mainly about time measurements of working and break times in a list view by
    ///     ObservableCollections of TimeSpanRecords.
    ///     
    ///     This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged.
    ///     
    ///     The ViewModel uses the service ElapsedTimeSpanListStore for the purpose to retrieve the
    ///     stored data of the ElapsedTimeSpanList that stores everything during runtime.
    ///     If the ViewModel would not use this service, the ObservableCollections would be empty after each
    ///     View Navigation (Every navigation create a new instance of the ViewModels).
    ///     
    /// </summary>
    public class ElapsedTimesViewModel : ViewModelBase
    {

        private readonly ElapsedTimeSpanListStore _elapsedTimeSpanListStore;
        public TimeSpan TotalMainTimeSpans { get; set; }
        public TimeSpan TotalBreakTimeSpans { get; set; }
        public ObservableCollection<TimeSpanRecord> ElapsedMainTimeSpanList { get; private set; }
        public ObservableCollection<TimeSpanRecord> ElapsedBreakTimeSpanList { get; private set; }


        /// <summary>
        ///     Initializes a new instance of a ElapsedTimesViewModel and initializes two properties of
        ///     ObservableCollection.
        ///         ElapsedMainTimeSpanList = A list for all working times measured
        ///         ElapsedBreakTimeSpanList = A list for all break times measured
        ///     Then it directly calculatios the total of those timespans inside the lists and assign the value
        ///     to the properties that are bound to the UI.
        ///
        /// <paramref name="elapsedTimeSpanStore">
        ///     injects the ElapsedTimeSpanListStore
        ///     </paramref>
        ///     
        /// </summary>
        public ElapsedTimesViewModel(ElapsedTimeSpanListStore elapsedTimeSpanStore)
        {
            _elapsedTimeSpanListStore = elapsedTimeSpanStore;
            ElapsedMainTimeSpanList = _elapsedTimeSpanListStore.ElapsedMainTimeSpanList;
            ElapsedBreakTimeSpanList = _elapsedTimeSpanListStore.ElapsedBreakTimeSpanList;
            TotalMainTimeSpans = CalculateTotalTimeSpans(ElapsedMainTimeSpanList);
            TotalBreakTimeSpans = CalculateTotalTimeSpans(ElapsedBreakTimeSpanList);
        }

        /// <summary>
        ///     Calculates the total of all TimeSpans a list.
        ///     Again distinguishes between work and break times based on ten TimeSpanType 
        ///     of the TimeSpans in the list to distinguish between the two lists
        ///     and add them to the correct property
        ///
        /// <paramref name="elapsedTimeSpanList">:
        ///     the ObservableCollection of timeSpans to be used for the calculation of the total duration
        ///    </paramref>
        ///    
        /// <returns>
        ///     a TimeSpan total value of all TimeSpan durations in the ObservableCollection used.
        ///     </returns>
        ///
        /// </summary>
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
