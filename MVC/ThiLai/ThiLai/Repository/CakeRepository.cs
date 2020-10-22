using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThiLai.Models;
using ThiLai.Models.Entities;
using ThiLai.Models.ModelViews;

namespace ThiLai.Repository
{
    public class CakeRepository : ICakeRepository
    {
        private readonly AppDbContext context;

        public CakeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public int CreateCake(Cake cake)
        {
            context.Cakes.Add(cake);
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            var del = GetCake(id);
            del.DaXoa = true;
            return context.SaveChanges();
        }

        public int EditCake(Cake cake)
        {
            var editCake = context.Cakes.Attach(cake);
            editCake.State = EntityState.Modified;
            return context.SaveChanges();
        }

        public Cake GetCake(int id)
        {
            return context.Cakes.FirstOrDefault(c => c.CakeId == id);
        }

        public List<CakeViewModel> GetCakes()
        {
            var cakes = (from c in context.Cakes
                         join ct in context.Categories on c.CategoryId equals ct.CategoryId
                         select new CakeViewModel()
                         {
                             CakeId = c.CakeId,
                             TenBanh = c.TenBanh,
                             GiaBan = c.GiaBan,
                             NSX = c.NSX,
                             DaXoa = c.DaXoa,
                             HSD = c.HSD,
                             CategoryName = ct.CategoryName,
                             ThanhPhan = c.ThanhPhan,
                             CategoryId = ct.CategoryId
                         }).ToList();
            return cakes;
        }

        public CategoryOfCake GetCakesOfCategory(int categoryId)
        {
            var model = new CategoryOfCake();
            model.CategoryId = categoryId;
            var cakes = GetCakes();
            var cakeofcategory = new List<CakeViewModel>();
            foreach(var item in cakes.ToList())
            {
                if(item.CategoryId == categoryId)
                {
                    cakeofcategory.Add(item);
                }
            }
            model.cakes = cakeofcategory;
            return model;
        }

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
