using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThiLai_mondule3.Models;
using ThiLai_mondule3.Models.Entities;
using ThiLai_mondule3.Models.ModelViews;
using ThiLai_mondule3.Repositories;

namespace ThiLai_mondule3.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository bookRepository;

        public HomeController(ILogger<HomeController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            this.bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var categories = bookRepository.GetCategories();
            return View(categories);
        }
        
        public IActionResult ListBook()
        {
            var books = bookRepository.Gets();
            return View(books);
        }
        [Route("Home/IndexBook/{CategoryId}")]
        public IActionResult IndexBook(int CategoryId)
        {
            var books = bookRepository.GetCategoryViews(CategoryId);
            return View(books);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBook model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    BookName = model.BookName,
                    Amount = model.Amount,
                    Author = model.Author,
                    CategoryId = model.CategoryId,
                    Content = model.Content,
                    Time = model.Time
                };
                if(bookRepository.Create(book) > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            
            }
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            try
            {
                var book = bookRepository.Get(id);
                return View(book);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = bookRepository.GetBook(id);
            var newbook = new EditBook()
            {
                BookId = book.BookId,
                Content = book.Content,
                Time = book.Time,
                Amount = book.Amount,
                Author = book.Author,
                BookName = book.BookName,
                CategoryId = book.CategoryId
            };
            return View(newbook);
        }
        [HttpPost]
        public IActionResult Edit(EditBook model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    BookId = model.BookId,
                    Amount = model.Amount,
                    Author = model.Author,
                    BookName = model.BookName,
                    CategoryId = model.CategoryId,
                    Content = model.Content,
                    Time = model.Time
                };
                if (bookRepository.Edit(book) > 0)
                {
                    return RedirectToAction("ListBook", "Home");
                }
            }
            return View();
        }
        [Route("Home/Delete/{bookId}")]
        public IActionResult Delete(int bookId)
        {
            var delBook = bookRepository.Delete(bookId);
            return Json(new { delBook });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
