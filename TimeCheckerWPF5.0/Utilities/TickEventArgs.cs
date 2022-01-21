using System;

namespace TimeCheckerWPF5._0.Utilities
{
    /// Summary:
    ///    Specific EventArgs for the TimeWatch class since it needs it own Args of type TimeSpan
    public class TickEventArgs : EventArgs
    {
        public TimeSpan TimeSpan { get; set; }
    }

}
