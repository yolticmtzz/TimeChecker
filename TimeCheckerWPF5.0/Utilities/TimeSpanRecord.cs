using System;

namespace TimeCheckerWPF5._0.Utilities
{

    /// Summary:
    ///   two enums to distinguish between working time and break time spans for the TimeSpanRecords.
    public enum TimeSpanType
    {
        MainTime,
        BreakTime
    }

    /// Summary:
    ///    The TimeSpanRecord represent time periods between two events.
    ///    In this applications context, working times and break times are to be stored specifically in order
    ///    to visualize them in the UI without having to use the database. The goal is to show only measured time intervals
    ///    during runtime and not to fill the database with more data, because theoretically this data is already 
    ///    available in the database through the TimeEntries. Because the implementer wanted to learn how to store
    ///    and use time intervals without a database connection, this model and logic was implemented this way.
    public class TimeSpanRecord
    {

        /// Summary:
        ///     Initializes a new instance of a TimeSpanRecord
        ///
        /// Parameters:
        ///   type:
        ///     defines the TimeSpanRecord time as working or break time of enum type TimeSpanType
        ///   startTime:
        ///     every TimeSpanRecord must be intialized with a startTime of type DateTime
        ///   user:
        ///     each TimeSpanRecord is produced by a specific user of type string
        public TimeSpanRecord(TimeSpanType type, DateTime startTime, string user)
        {
            TimeSpanType = type;
            StartDateTime = startTime;
            User = user;
        }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public TimeSpan Duration => EndDateTime.Subtract(StartDateTime);

        public TimeSpanType TimeSpanType { get; set; }

        public string User { get; set; }
    }
}
