using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCheckerWPF.Classes
{
    class BusinessLogic
    {


        public string Time()
        {
            string currentTime = DateTime.Now.ToLongTimeString();

            return currentTime;
        }

        public string Date()
        {
           // string currentDate = DateTime.Now.ToLongDateString();

            string currentDate = DateTime.Now.ToShortDateString();

            return currentDate;
        }
    }
}
