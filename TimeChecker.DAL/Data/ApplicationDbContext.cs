using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TimeChecker.DAL.Models;

namespace TimeChecker.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Timeentry> Timeentry { get; set; }

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {


        }
    }
}
