using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeChecker.DAL.Data;

namespace TimeCheckerWPF5._0.DBOperations
{
    public class DBOperationsBase
    {
        readonly ApplicationDbContext _context = new(new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
        .Options);
    }
}
