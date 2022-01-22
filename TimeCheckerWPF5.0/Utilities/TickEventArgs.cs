using System;

namespace TimeCheckerWPF5._0.Utilities
{
    /// <summary>
    ///    Specific EventArgs for the TimeWatch class since it needs it own Args of type TimeSpan
    /// </summary>
    public class TickEventArgs : EventArgs
    {
        public TimeSpan TimeSpan { get; set; }
    }

}
