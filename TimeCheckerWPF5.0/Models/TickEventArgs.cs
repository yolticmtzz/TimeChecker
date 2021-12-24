using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCheckerWPF5._0.Models
{
    class TickEventArgs
    {
        //Data carrier of the Timespans and it must inherit the EventArgs. To be used with the EventHandler
        public class TickEventArgs : EventArgs

        {
            public TimeSpan TimeSpan { get; set; }
        }
    }

}
