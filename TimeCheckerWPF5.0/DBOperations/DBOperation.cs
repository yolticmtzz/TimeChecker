using Microsoft.EntityFrameworkCore;
using System;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;

namespace TimeCheckerWPF5._0.DBOperations
{
    class TimeEntryAddDBOperation
    {
  
        readonly ApplicationDbContext _context = new(new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
       .Options);
        
        public TimeEntryAddDBOperation(short type, DateTime timeCatch, string user)
        {
            var record = new Timeentry()
            {
                Type = type,
                DateTime = timeCatch,
                User = user,
            };

            _context.Timeentry.Add(record);
            _context.SaveChanges();
        }

        public TimeEntryAddDBOperation(short type, DateTime timeCatch, string comment, string user)
        {
            var record = new Timeentry()
            {
                Type = type,
                DateTime = timeCatch,
                Comment = comment,
                User = user,
            };

            _context.Timeentry.Add(record);
            _context.SaveChanges();
        }



    }
}
