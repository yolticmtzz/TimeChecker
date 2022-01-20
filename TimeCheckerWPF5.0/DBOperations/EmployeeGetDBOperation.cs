using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;

namespace TimeCheckerWPF5._0.DBOperations
{
    public class EmployeeGetDBOperation
    {

        readonly ApplicationDbContext _context = new(new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
         .Options);

        public List<Employee> EmployeesDBList { get; set; }

        public EmployeeGetDBOperation()
        {

            EmployeesDBList = _context.Employees.ToList();

        }
    }
}
