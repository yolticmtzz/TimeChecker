using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TimeChecker.Models;

namespace TimeChecker.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Employee> Employees { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
    }
}
