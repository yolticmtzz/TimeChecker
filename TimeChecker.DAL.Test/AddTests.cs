using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using System.Linq;

/**
 * Die Klasse AddTests testet das Hinzufügen eines Timeentry Datensatz.
 * 
 * Methoden: 
 * 
 * InsertTimeentry() - Hinzufügen Timeentry Datensatz
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */

namespace TimeChecker.DAL.Test
{
    public class AddTests
    {
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options);
        }


        [Test]
        public void InsertTimeentry()
        {
            var record = new Timeentry()
            {
                Type = 2,
                Comment = "Service",
                User = "Max Mustermann"

            };

            _context.Timeentry.Add(record);

            _context.SaveChanges();

            var addedTimeentry = _context.Timeentry.Single(x => x.Type == 2);

            Assert.Greater(addedTimeentry.ID, 0);
            Assert.AreEqual(record.Type, addedTimeentry.Type);
            Assert.AreEqual(record.User, addedTimeentry.User);
            Assert.AreEqual(record.Comment, addedTimeentry.Comment);
        }

        [TearDown]
        public void TearDown()
        {
            var Timeentry = _context.Timeentry.Single(x => x.Comment == "Service");
            _context.Timeentry.Remove(Timeentry);
            _context.SaveChanges();
        }
    }
}