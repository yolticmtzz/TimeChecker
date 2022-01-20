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
    public class DataBaseService
    {
        private readonly ApplicationDbContext _dbContext;

        public DataBaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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



    }
}
