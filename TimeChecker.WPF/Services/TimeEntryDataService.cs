using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using TimeChecker.Domain.Models;
using TimeChecker.EntityFramework;

namespace TimeChecker.WPF.Services
{
    class TimeEntryDataService : ITimeEntryDataService
    {
        private readonly TimeCheckerDbContextFactory _context;
        
        public TimeEntryDataService(TimeCheckerDbContextFactory context, int type)
        {
            _context = context;
            CreateTimeEntry(type);
        }

        public void CreateTimeEntry(int type)
        {
            
            var record = new TimeEntry()
            {
                User = "DummyUser",
                Type = type,
                DateTimNow = DateTime.Now,
                Comment = ""
            };

            var _contextr = _context.CreateDbContext();

            //How now write this to de Database?
            _contextr.TimeEntries.Add(record);
            _contextr.SaveChanges();


            var timeentry = new Dictionary<string, string>();
            timeentry.Add("user", "DummyUser");
            timeentry.Add("type", type.ToString());
            timeentry.Add("datetimenow", DateTime.Now.ToString());
            timeentry.Add("comment", "");

            string dictset = "";
            foreach (var element in timeentry)
            {
                dictset = dictset + $" {element},";
            }
            
            MessageBox.Show(dictset);
        }
    }
}




  

