using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * 
 * Die Klasse Timeentry besitzt folgende Attribute:
 * - ID
 * - Type
 * - DateTime
 * - Comment
 * - User
 * 
 * In der SQL-Datenbank wird ein Table "Timeentry" angelegt welche als Ressource für die Web Applikation 
 * und der WPF Applikation dient.
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */

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
