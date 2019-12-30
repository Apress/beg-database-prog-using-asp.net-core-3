using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;



namespace EmployeeManager.Mvc.Models
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<Country> Countries { get; set; }

    }
}
