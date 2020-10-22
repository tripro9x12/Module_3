using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThiLai_mondule3.Models.Entities;
using ThiLai_mondule3.Models.ModelViews;

namespace ThiLai_mondule3.Repositories
{
    public interface IBookRepository
    {
        List<BookViewModel> Gets();
        BookViewModel Get(int id);
        Book GetBook(int id);
        int Create(Book book);
        int Edit(Book book);
        int Delete(int id);
        List<Category> GetCategories();
        CategoryView GetCategoryViews( int categoryId);
        Category GetCategory(int id);
    }
}
