using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThiLai.Models.Entities;

namespace ThiLai.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                    new Category()
                    {
                        CategoryId = 1,
                        CategoryName = "Bánh Gạo"
                    },
                    new Category()
                    {
                        CategoryId = 2,
                        CategoryName = "Bánh Tráng"
                    },
                    new Category()
                    {
                        CategoryId = 3,
                        CategoryName = "Bánh ép"
                    },
                    new Category()
                    {
                        CategoryId = 4,
                        CategoryName = "Bánh Canh"
                    },
                    new Category()
                    {
                        CategoryId = 5,
                        CategoryName = "Bánh Tráng Cuộn"
                    }
            );
        }
    }
}
