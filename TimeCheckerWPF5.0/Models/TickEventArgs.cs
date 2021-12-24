using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCheckerWPF5._0.Models
{
    public class TickEventArgs : EventArgs
    {
        //Data carrier of the Timespans and it must inherit the EventArgs. To be used with the EventHandler
        public TimeSpan TimeSpan { get; set; }
        
    }

}
