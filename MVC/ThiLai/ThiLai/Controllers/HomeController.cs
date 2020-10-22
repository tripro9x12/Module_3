using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using ThiLai.Models;
using ThiLai.Models.Entities;
using ThiLai.Models.ModelViews;
using ThiLai.Repository;

namespace ThiLai.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICakeRepository cakeRepository;

        public HomeController(ILogger<HomeController> logger, ICakeRepository cakeRepository)
        {
            _logger = logger;
            this.cakeRepository = cakeRepository;
        }

        public IActionResult Index()
        {
            var categories = cakeRepository.GetCategories();
            return View(categories);
        }
        public IActionResult IndexCake()
        {
            var cakes = cakeRepository.GetCakes();
            return View(cakes);
        }
        [Route("Home/CakeOfCategory/{CategoryId}")]
        public IActionResult CakeOfCategory(int categoryId)
        {
            var categories = cakeRepository.GetCakesOfCategory(categoryId);
            return View(categories);
        }
        [HttpGet]
        [Route("/Home/Create/{categoryId}")]
        public IActionResult Create(int categoryId)
        {
            var category = cakeRepository.GetCategory(categoryId);
            ViewBag.Category = category;
            return View();
        }
        [HttpPost]
        public IActionResult Create (CreateCake model)
        {
            if (ModelState.IsValid)
            {
                var cake = new Cake()
                {
                    TenBanh = model.TenBanh,
                    ThanhPhan = model.ThanhPhan,
                    GiaBan = model.GiaBan,
                    NSX = model.NSX,
                    HSD = model.HSD,
                    CategoryId = model.CategoryId
                };
                if (cakeRepository.CreateCake(cake) > 0)
                {
                    return RedirectToAction("IndexCake", "Home");
                }
            }
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            try
            {
                var detal = cakeRepository.GetCake(id);
                return View(detal);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var edit = cakeRepository.GetCake(id);
            var cake = new EditCake()
            {
                CakeId = edit.CakeId,
                CategoryId = edit.CategoryId,
                GiaBan = edit.GiaBan,
                HSD = edit.HSD,
                NSX = edit.NSX,
                TenBanh = edit.TenBanh,
                ThanhPhan = edit.ThanhPhan
            };
            return View(cake);
        }
        [HttpPost]
        public IActionResult Edit(EditCake model)
        {
            if (ModelState.IsValid)
            {
                var cake = new Cake()
                {
                    CakeId = model.CakeId,
                    CategoryId = model.CategoryId,
                    GiaBan = model.GiaBan,
                    HSD = model.HSD,
                    NSX = model.NSX,
                    TenBanh = model.TenBanh,
                    ThanhPhan = model.ThanhPhan
                };
                if (cakeRepository.EditCake(cake) > 0)
                {
                    return RedirectToAction("IndexCake", "Home");
                }
            }
            return View();
        }
        [Route("Home/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var delCake = cakeRepository.Delete(id);
            return Json(new { delCake });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
