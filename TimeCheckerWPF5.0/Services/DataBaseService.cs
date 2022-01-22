﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;

namespace TimeCheckerWPF5._0.Services
{

    /// <summary>
    ///     This class implements database manipulation services:
    ///       - adding timeentries to the database
    ///       - getting all employees stored in the database
    ///     registered to the database
    /// </summary>
    public class DataBaseService
    {
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance database manipulation services:
        /// Logic based on EntityFramworkCore
        /// 
        /// <paramref name="dbContext">:
        ///   dbContext:
        ///     Refers to the ApplicationDBContext related to the database
        ///   </paramref>
        ///   
        /// </summary>
        public DataBaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Implements adding TimeEntries to the Database by Add & SaveChanges (OR-Mapper)
        /// 
        /// <paramref name="type">
        /// defines the TimeEntryType as short:
        ///         1 = CheckIn
        ///         2 = CheckOut
        ///         3 = BreakModeOn
        ///         4 = BreakModeOff -> CheckIn
        /// </paramref>
        /// <paramref name="timeCatch"> 
        /// system time as DateTime to be stored during TimeEntry catch.
        /// </paramref>
        /// <paramref name="user"> 
        /// the user stored in the userstore (current logged user) as string.
        /// </paramref>
        /// <paramref name="comment">
        ///  a comment as string. Can be null, since only and optionally used during CheckOut (type 2)
        /// </paramref>
        /// 
        /// <returns>
        /// a boolean wether the db entry could be made or not (measured based on if any changes were saved or not
        /// </returns>
        /// 
        /// </summary>
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

        /// <summary>
        /// Implements getting the Employees from the database by Get (OR-Mapper)
        ///         
        /// <returns>
        ///  a list of Employees
        /// </returns>:
        ///    
        /// </summary>
        public List<Employee> GetEmployees()
        { 
            List<Employee> EmployeesDB = new List<Employee>();
            EmployeesDB = _dbContext.Employees.ToList();
            return EmployeesDB;
        }

    }
}
