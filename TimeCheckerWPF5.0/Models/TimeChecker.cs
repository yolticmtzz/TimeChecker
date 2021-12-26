using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCheckerWPF5._0.Models
{
    public class TimeChecker
    {
        public DateTime Date { get; set; }
        public int StatusType { get; set; }

        public string StatusText { get; set; }       

        public TimeWatch MainTime { get; set; }

        public TimeWatch BreakTime { get; set; }

    }
}
