using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TimeCheckerWPF5._0.Models
{
    public class TimeWatch
    {
        //Declare a handler for when the watchtick is triggered to run of type of own arguments (timeSpan)
        internal EventHandler<TickEventArgs> TickEvent;
        internal DispatcherTimer dispatcherTimer = new DispatcherTimer();
        internal Stopwatch stopwatch = new Stopwatch();




        public TimeWatch()
        {
            //Subscribing the dispatcherTimer on tick to call the OnDispatchTimerTick event and define the Time interval of the TimeSpan to tick.
            dispatcherTimer.Tick += new EventHandler(OnDispatchTimerTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        //Stopwatch Start, Stop and Reset functions
        internal void StopwatchStart()
        {
            stopwatch.Start();
            dispatcherTimer.Start();
        }

        internal void StopwatchStop()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
            //etil.AddItemToTimeItemList(currentTime);
        }

        internal string StopwatchReset()
        {
            stopwatch.Reset();
            return "00:00:00";
        }

        //Triggering the EventHandler
        protected virtual void OnWatchTickEvent(TickEventArgs e)
        {
            EventHandler<TickEventArgs> tickEvent = TickEvent;
            if (tickEvent != null)
            {
                tickEvent(this, e);
            }
        }

        //Behavior if StopwatchTick Event was triggered
        void OnDispatchTimerTick(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                TickEventArgs tickEvent = new TickEventArgs();
                tickEvent.TimeSpan = stopwatch.Elapsed;
                OnWatchTickEvent(tickEvent);
            }
        }
    }
}
