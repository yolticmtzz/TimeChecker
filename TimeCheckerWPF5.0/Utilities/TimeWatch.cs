using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace TimeCheckerWPF5._0.Utilities
{
    /// <summary>
    ///    The class that implements the logic for the ticking time clock in the UI
    ///    It consists of a stopwatch, a DispatchTimer and the EventHandler TickEvent
    /// </summary>
    public class TimeWatch
    {
        internal EventHandler<TickEventArgs> TickEvent;
        internal DispatcherTimer dispatcherTimer = new();
        private readonly Stopwatch stopwatch = new();

        /// <summary>
        ///     Initializes a new instance of a TimeWatch
        ///     It is registered to the EventHandler that the method OnDispatchTimeTick is called for each Thick
        ///     The time interval of each tick is one second
        ///     With this the OnDispatchTimerTicket is called every second
        /// </summary>
        public TimeWatch()
        {
            dispatcherTimer.Tick += new EventHandler(OnDispatchTimerTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        /// <summary>
        ///     Starts the Stopwatch and the DispatcherTimer
        /// </summary>
        public void StopwatchStart()
        {
            stopwatch.Start();
            dispatcherTimer.Start();
        }

        /// <summary>
        ///     Stops the Stopwatch if it is running
        /// </summary>
        public void StopwatchStop()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
        }

        /// <summary>
        ///     Resets the Stopwatch
        /// <returns>
        ///  00:00:00 as string
        /// </returns>:
        ///     
        /// </summary>
        public string StopwatchReset()
        {
            stopwatch.Reset();
            return "00:00:00";
        }

        /// <summary>
        ///     During each DispatchTimer.tick it is checked if the stopwatch is running.
        ///     If it is running, a new TickEventArg is generated as TimeSpan 
        ///     and filled with the elapsed time (1 second) and passed to the method "OnWatchTickEvent" 
        ///     
        /// <paramref name="sender">
        /// the DispatchTimer.Tick as object
        /// </paramref>
        /// <paramref name="e">
        /// an eventArg from system
        /// </paramref>
        /// 
        /// </summary>
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

        /// <summary>
        ///     Invokes the TickEvent of the instance of the Timewatch
        ///     
        /// <paramref name="tickEvent"/>
        ///   timeSpan:
        ///     a TimeSpan as Arg from class TickEventArgs
        ///     
        /// </summary>
        protected virtual void OnWatchTickEvent(TickEventArgs tickEvent)
        {
            TickEvent?.Invoke(this, tickEvent);
        }

    }
}
