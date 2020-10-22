using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThiLai_mondule3.Models.Entities;

namespace ThiLai_mondule3.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = 1,
                    CategoryName = "Truyện Tranh"
                },
                new Category()
                {
                    CategoryId = 2,
                    CategoryName = "Văn Học"
                },
                 new Category()
                 {
                     CategoryId = 3,
                     CategoryName = "Toán Học"
                 },
                  new Category()
                  {
                      CategoryId = 4,
                      CategoryName = "Khoa Học"
                  },
                  new Category()
                  {
                      CategoryId = 5,
                      CategoryName = "Viễn Tưởng"
                  }
            );
        }
    }
}
