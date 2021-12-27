using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChecker.DAL.Models
{
    public interface IEmployee
    {
        public int Id { get; set; }
        public string Prename { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get => $"{Prename} {Lastname }"; }

        public string setUser()
        {
            return Fullname;
        }
    }
}
