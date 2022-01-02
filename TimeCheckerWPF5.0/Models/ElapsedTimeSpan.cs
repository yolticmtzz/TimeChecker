using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCheckerWPF5._0.Models
{
    public class ElapsedTimeSpan
    {
   

        public string TimeSpanType { get; set; }
        
     
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Duration { get; set; }

        public ElapsedTimeSpan(DateTime startDateTime, string timeSpanType)
        {
            StartDateTime = startDateTime;
            TimeSpanType = timeSpanType;
        }

        public void SetElapsedTime()
        {
            var _elapsedTime = (EndDateTime - StartDateTime);
            Duration =  _elapsedTime.ToString();
            
                 
             
        }




    }
}
