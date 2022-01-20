using System;

namespace TimeCheckerWPF5._0.Utilities
{
    public class TickEventArgs : EventArgs
    {
        //Data carrier of the Timespans and it must inherit the EventArgs. To be used with the EventHandler
        public TimeSpan TimeSpan { get; set; }
        
    }

}
