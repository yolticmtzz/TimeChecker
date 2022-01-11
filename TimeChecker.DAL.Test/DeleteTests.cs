using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using System.Linq;


/**
 * Die Klasse DeleteTests testet das Löschen eines Timeentry Datensatz.
 * 
 * Methoden: 
 * 
 * DeleteTodoItem() - Löschen Timeentry Datensatz
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */

namespace TimeChecker.DAL.Test
{
    public class DeleteTests
    {
        private ApplicationDbContext _context;

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

        }



        [Test]
        public void DeleteTodoItem()
        {
            var existing = _context.Timeentry.Single(x => x.Type == 2);

            var TimeentryId = existing.ID;
            _context.Timeentry.Remove(existing);
            _context.SaveChanges();

            var found = _context.Timeentry.SingleOrDefault(x => x.Type == 2);
            Assert.IsNull(found);
        }
    }
}