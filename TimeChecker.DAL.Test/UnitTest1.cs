using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;


namespace TimeChecker.DAL.Test
{
    public class Tests
    {
    //    private ApplicationDbContext _context;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
    //           .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TodoListManager;Trusted_Connection=True;MultipleActiveResultSets=true")
    //           .Options);
    //    }
    //    [Test]
    //    //public void InsertTodoItem()
    //    //{
    //    //    var record = new Timeentry()
    //    //    {
    //    //        Comment = "Organize meeting",

    //    //    };

    //    //    _context.Timeentry.Add(record);

    //    //    _context.SaveChanges();

    //    //    var addedTodoItem = _context.Timeentry.Single(x => x.Comment == "Organize meeting");

    //    //    Assert.Greater(addedTodoItem.Id, 0);

    //    //    Assert.AreEqual(record.Comment, addedTodoItem.Completed);
    //    //}

    //    //[TearDown]
    //    //public void TearDown()
    //    //{
    //    //    var todoItem = _context.Timeentry.Single(x => x.Comment == "Organize meeting");
    //    //    _context.Timeentry.Remove(todoItem);
    //    //    _context.SaveChanges();
    //    //}
    }
}