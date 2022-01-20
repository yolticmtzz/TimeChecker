using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //        public void clearData()
        //        {
        //            Type_txt.Clear();
        //            Comment_txt.Clear();
        //            User_txt.Clear();
        //        }

        //        private void Button_Click(object sender, RoutedEventArgs e)
        //        {
        //            clearData();
        //        }


        //        private void Insert(object sender, RoutedEventArgs e)
        //        {
        //            short Type_int = Int16.Parse(Type_txt.Text);

        //            var record = new Timeentry()
        //            {
        //                Type = Type_int,
        //                DateTime = currentDate,
        //                Comment = Comment_txt.Text,
        //                User = User_txt.Text,
        //            };

        //            _context.Timeentry.Add(record);

        //            _context.SaveChanges();

        //           // _TimeentryId = record.ID;

        //            LoadDatagrid();

        //        }


        //        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        //        {
        //            int ID_int = Int32.Parse(ID_txt.Text);

        //            var existing = _context.Timeentry.Single(x => x.ID == ID_int);
        //            //var existing = _context.Timeentry.Single(x => x.Type == 2);

        //            _context.Timeentry.Remove(existing);

        //            _context.SaveChanges();

        //            LoadDatagrid();
        //        }


        //        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        //        {
        //            int ID_int = Int32.Parse(ID_txt.Text);
        //            short Type_int = Int16.Parse(Type_txt.Text);

        //            var existing = _context.Timeentry.Single(x => x.ID == ID_int);


        //            existing.Type = Type_int;
        //            existing.DateTime = currentDate;
        //            existing.Comment = Comment_txt.Text;
        //            existing.User = User_txt.Text;

        //            _context.SaveChanges();

        //            LoadDatagrid();

        //        }
        //    }
        //}


