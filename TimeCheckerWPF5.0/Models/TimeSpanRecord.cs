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
        public TimeSpanRecord(TimeSpanType type)
        {
            TimeSpanType = type;

        }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public TimeSpan Duration => EndDateTime.Subtract(StartDateTime);

        public TimeSpanType TimeSpanType { get; set; }
    }
}
