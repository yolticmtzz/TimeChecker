using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * 
 * Die Klasse Employee besitzt folgende Attribute:
 * - ID
 * - Prename
 * - Lastname
 * - Fullname
 * 
 * In der SQL-Datenbank wird ein Table "Employees" angelegt welche als Ressource für die Web Applikation 
 * und der WPF Applikation dient.
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */


namespace TimeChecker.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Prename { get; set; }

        public string Lastname { get; set; }

        public string Fullname { get => $"{Prename} {Lastname }"; }

        public Employee() { }
 

        public Employee(string prename, string lastname)
        {
            Prename = prename;
            Lastname = lastname;
        }

    }
}
