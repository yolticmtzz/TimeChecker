using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace TimeChecker.Models
{
    [Serializable]
    public class SerializeData
    {

        private string Firstname { get; set; }

        public SerializeData(string firstname)
        {

            Firstname = firstname;
        }
    }
            
}
