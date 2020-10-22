using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThiLai.Models.Entities;
using ThiLai.Models.ModelViews;

namespace ThiLai.Repository
{
    public interface ICakeRepository
    {
        List<Category> GetCategories();
        List<CakeViewModel> GetCakes();
        Cake GetCake(int id);
        CategoryOfCake GetCakesOfCategory(int categoryId);
        int CreateCake(Cake cake);
        int EditCake(Cake cake);
        int Delete(int id);
        Category GetCategory(int id);
    }
}
