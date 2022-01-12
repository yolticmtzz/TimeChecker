using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChecker.DAL.Models
{
    public class Timeentry
    {
        public int ID { get; set; }

        public short Type { get; set; }

        public DateTime DateTime { get; set; }

        public string Comment { get; set; }

        public string User { get; set; }





    }
}
