using System;

namespace TimeCheckerWPF5._0.Models
{
    
    public enum TimeSpanType
    {
        MainTime,
        BreakTime

    }
    
    public class TimeSpanRecord
    {
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
