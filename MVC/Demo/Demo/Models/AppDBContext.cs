using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(
                 new Employee()
                 {
                     Id = 1,
                     Fullname = "An Nguyễn",
                     Department = Dept.IT,
                     Email = "anpro@gmail.com",
                     AvatarPath = "~/images/2.png"
                 },
                new Employee()
                {
                    Id = 2,
                    Fullname = "Long Nguyễn",
                    Department = Dept.HR,
                    Email = "long9x@gmail.com",
                    AvatarPath = "~/images/3.png"
                }
                );
        }
    }
}
