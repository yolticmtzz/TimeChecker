using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;

namespace TimeCheckerWPF5._0.Services
{

    /// Summary:
    ///     This class implements database manipulation services:
    ///       - adding timeentries to the database
    ///       - getting all employees stored in the database
    ///     registered to the database
    public class DataBaseService
    {
        private readonly ApplicationDbContext _dbContext;

        /// Summary:
        /// Initializes a new instance database manipulation services:
        /// Logic based on EntityFramworkCore
        /// 
        /// Parameters:
        ///   dbContext:
        ///     Refers to the ApplicationDBContext related to the database
        public DataBaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// Summary:
        /// Implements adding TimeEntries to the Database by Add & SaveChanges (OR-Mapper)
        ///         
        /// Parameters:
        ///   type:
        ///     defines the TimeEntryType as short:
        ///         1: CheckIn
        ///         2: CheckOut
        ///         3: BreakModeOn
        ///         4: BreakModeOff -> CheckIn
        ///   timeCatch:
        ///    system time as DateTime to be stored during TimeEntry catch.
        ///   user:
        ///     the user stored in the userstore (current logged user) as string.
        ///   comment:
        ///     a comment as string. Can be null, since only and optionally used during CheckOut (type 2)
        ///     
        /// Returns:
        ///     a boolean wether the db entry could be made or not (measured based on if any changes were saved or not)
        public bool AddTimeEntry(short type, DateTime timeCatch, string user, string? comment = "")
        {
            var record = new Timeentry()
            {
                Type = type,
                DateTime = timeCatch,
                User = user,
                Comment = comment,
            };

            _dbContext.Timeentry.Add(record);
            int result = _dbContext.SaveChanges();
            if (result > 0) return true;

            return false;
        }

        /// Summary:
        /// Implements getting the Employees from the database by Get (OR-Mapper)
        ///         
        /// Returns:
        ///     a list of Employees
        public List<Employee> GetEmployees()
        { 
            List<Employee> EmployeesDB = new List<Employee>();
            EmployeesDB = _dbContext.Employees.ToList();
            return EmployeesDB;
        }

    }
}
