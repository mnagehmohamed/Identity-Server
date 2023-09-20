using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeLogix.Server.AppDbContext
{
    public class EmployeeContext: IdentityDbContext<ApplicationUser>
    {
       public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
