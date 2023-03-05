using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fate.Models;

namespace Wheel_OF_Fate.DA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { 

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeWorkingHours> EmployeeWorkingHours { get; set; }
        public DbSet<Shift> shifts { get; set; }
    }
}
