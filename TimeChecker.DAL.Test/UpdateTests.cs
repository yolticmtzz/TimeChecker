using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using System.Linq;
using System.Collections.Generic;

/**
 * Die Klasse UpdateTests testet das Updaten eines Timeentry Datensatz.
 * 
 * Methoden: 
 * 
 * UpdateTodoItem() - Updaten eines Timeentry Datensatz.
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */


namespace TimeChecker.DAL.Test
{
    [TestFixture]
    public class UpdateTests
    {
        private ApplicationDbContext _context;
        private int _TimeentryId;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options);


            var record = new Timeentry()
            {
                Type = 2,
                Comment = "Service",
                User = "Max Mustermann"

            };

            _context.Timeentry.Add(record);

            _context.SaveChanges();

            _TimeentryId = record.ID;

        }

        /// <summary>
        /// Updaten eines Timeentry Datensatz.
        /// </summary>
        [Test]
        public void UpdateTodoItem()
        {
            var Timeentry = _context.Timeentry.Single(x => x.ID == _TimeentryId);
            short Type = 2;
            string Comment = "Büro";
            string User = "Hans Keller";
            Timeentry.Type = Type;
            Timeentry.Comment = Comment;
            Timeentry.User = User;

            _context.SaveChanges();

            var updatedTimeentry = _context.Timeentry.Single(x => x.ID == _TimeentryId);
            Assert.AreEqual(2, updatedTimeentry.Type);
            Assert.AreEqual("Büro", updatedTimeentry.Comment);
            Assert.AreEqual("Hans Keller", updatedTimeentry.User);
        }

        [TearDown]
        public void TearDown()
        {
            var Timeentry = _context.Timeentry.Single(x => x.ID == _TimeentryId);
            _context.Timeentry.Remove(Timeentry);
            _context.SaveChanges();
        }
    }
}
