using BangazonEFTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonEFTest.Data
{
    // Think of this class as a representation of our database
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Think of each of these DbSets as a table in our database
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Computer> Computer { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
