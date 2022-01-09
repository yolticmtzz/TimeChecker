using System;

namespace TimeCheckerWPF5._0.Models
{
    public class TimeSpanRecord
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public TimeSpan Duration => EndDateTime.Subtract(StartDateTime);
    }
}
