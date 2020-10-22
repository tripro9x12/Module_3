using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ThiLai_mondule3.Models;
using ThiLai_mondule3.Models.Entities;
using ThiLai_mondule3.Models.ModelViews;

namespace ThiLai_mondule3.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext context;

        public BookRepository(AppDbContext context)
        {
            this.context = context;
        }
        public int Create(Book book)
        {
            context.Books.Add(book);
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            var book = GetBook(id);
            context.Books.Remove(book);
            return context.SaveChanges();
        }

        public int Edit(Book book)
        {
            var editBook = context.Books.Attach(book);
            editBook.State = EntityState.Modified;
            return context.SaveChanges();
        }

        public BookViewModel Get(int id)
        {
            return Gets().FirstOrDefault(b => b.BookId == id);
        }

        public Book GetBook(int id)
        {
            return context.Books.FirstOrDefault(b => b.BookId == id);
        }

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public CategoryView GetCategoryViews(int categoryId)
        {
            var categories = new CategoryView();
            categories.CategoryId = categoryId;
            var books = (from b in context.Books
                                join c in context.Categories on b.CategoryId equals c.CategoryId
                                select new BookViewModelDetail()
                                {
                                    CategoryId = c.CategoryId,
                                    BookId = b.BookId,
                                    BookName = b.BookName,
                                    Author = b.Author,
                                    Amount = b.Amount,
                                    CategoryName = c.CategoryName,
                                    Content = b.Content,
                                    Time = b.Time
                                }).ToList();
           
            List<BookViewModelDetail> booksCate = new List<BookViewModelDetail>();
            foreach(var item in books.ToList())
            {
                if(item.CategoryId == categoryId)
                {
                    booksCate.Add(item);
                }
            }
            categories.books = booksCate;
            return categories;
        }

        public List<BookViewModel> Gets()
        {
            var books = (from b in context.Books
                         join c in context.Categories on b.CategoryId equals c.CategoryId
                         select new BookViewModel()
                         {
                             BookId = b.BookId,
                             BookName = b.BookName,
                             Author = b.Author,
                             Amount = b.Amount,
                             CategoryName = c.CategoryName,
                             Content = b.Content,
                             Time = b.Time
                         }).ToList();
            return books;
        }
    }
}
