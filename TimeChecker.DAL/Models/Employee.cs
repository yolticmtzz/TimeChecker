using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeChecker.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Prename { get; set; }

        public string Lastname { get; set; }

        public string Fullname { get => $"{Prename} {Lastname }"; }

        public Employee ()
        {

        }

        public Employee(string prename, string lastname)
        {
            Prename = prename;
            Lastname = lastname;
        }

    }
}
