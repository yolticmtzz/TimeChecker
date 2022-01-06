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
        //Employees der Datenbank hinzufügen
        public DbSet<Employee> Employees { get; set; }

        //Timeentry der Datenbank hinzufügen
        public DbSet<Timeentry> Timeentry { get; set; }


        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {


        }
    }
}
