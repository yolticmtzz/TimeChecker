using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using System.Linq;
using System.Collections.Generic;

namespace TimeChecker.DAL.Test
{
    [TestFixture]
    public class SelectTests
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
        public void GetAllTimeentries()
        {
            IEnumerable<Timeentry> Timeentries = _context.Timeentry.ToList();
            Assert.AreEqual(2, Timeentries.Count());
        }
    }
}
