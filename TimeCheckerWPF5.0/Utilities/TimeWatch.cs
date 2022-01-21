using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace TimeCheckerWPF5._0.Utilities
{
    /// Summary:
    ///    The class that implements the logic for the ticking time clock in the UI
    ///    It consists of a stopwatch, a DispatchTimer and the EventHandler TickEvent
    public class TimeWatch
    {
        internal EventHandler<TickEventArgs> TickEvent;
        internal DispatcherTimer dispatcherTimer = new();
        private readonly Stopwatch stopwatch = new();

        /// Summary:
        ///     Initializes a new instance of a TimeWatch
        ///     It is registered to the EventHandler that the method OnDispatchTimeTick is called for each Thick
        ///     The time interval of each tick is one second
        ///     With this the OnDispatchTimerTicket is called every second
        public TimeWatch()
        {
            dispatcherTimer.Tick += new EventHandler(OnDispatchTimerTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        /// Summary:
        ///     Starts the Stopwatch and the DispatcherTimer
        
        public void StopwatchStart()
        {
            stopwatch.Start();
            dispatcherTimer.Start();
        }

        /// Summary:
        ///     Stops the Stopwatch if it is running
        public void StopwatchStop()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
        }

        /// Summary:
        ///     Resets the Stopwatch
        /// Returns:
        ///     00:00:00 as string
        public string StopwatchReset()
        {
            stopwatch.Reset();
            return "00:00:00";
        }

        /// Summary:
        ///     During each DispatchTimer.tick it is checked if the stopwatch is running.
        ///     If it is running, a new TickEventArg is generated as TimeSpan 
        ///     and filled with the elapsed time (1 second) and passed to the method "OnWatchTickEvent" 
        ///     
        /// Parameters:
        ///     sender:
        ///         the DispatchTimer.Tick as object
        ///     e:
        ///         an eventArg from system
        void OnDispatchTimerTick(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                TickEventArgs tickEvent = new TickEventArgs()
                {
                    TimeSpan = stopwatch.Elapsed
                };
                OnWatchTickEvent(tickEvent);
            }
        }

        /// Summary:
        ///     Invokes the TickEvent of the instance of the Timewatch
        ///     
        /// Parameters:
        ///   timeSpan:
        ///     a TimeSpan as Arg from class TickEventArgs
        protected virtual void OnWatchTickEvent(TickEventArgs tickEvent)
        {
            TickEvent?.Invoke(this, tickEvent);
        }

    }
}
