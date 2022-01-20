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


        public bool AddTimeEntry(short type, DateTime timeCatch, string user, string? comment)
        {

            using (var db = new ApplicationDbContext())
            {
                var record = new Timeentry()
                {
                    Type = type,
                    DateTime = timeCatch,
                    User = user,
                    Comment = comment,
                };

                db.Timeentry.Add(record);
                int result = db.SaveChanges();
                if (result > 0) return true;
            }

            return false;
        }



    }
}
