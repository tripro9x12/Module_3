using Demo.Models;
using Demo.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRespository employeeRespository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IEmployeeRespository employeeRespository, IWebHostEnvironment webHostEnvironment)
        {
            this.employeeRespository = employeeRespository;
            this.webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            //ViewData["Employee"] = employeeRespository.Gets();
            ViewBag.Employee = employeeRespository.Gets();
            //TempData["emp"] = employeeRespository.Gets();
            
            return View();
        }
        public ViewResult Details(int? id)
        {
            //ViewBag.Employee = employeeRespository.Get(id);
            //var employee = employeeRespository.Get(id);

            try
            {
                int.Parse(id.Value.ToString());
                var employee = employeeRespository.Get(id.Value);
                if (employee == null)
                {
                    //ViewBag.Id = id.Value;
                    return View("~/Views/Error/EmployeeNotFound.cshtml", id.Value);
                }
                var detailviewmodel = new HomeDetailsViewModel()
                {
                    employee = employeeRespository.Get(id ?? 1),
                    TitleName = "Employee Detail"
                };
                return View(detailviewmodel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
  
        [HttpPost]
        public IActionResult Create(HomeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Fullname = model.Fullname,
                    Email = model.Email,
                    Department = model.Department
                };
                var fileName = string.Empty;
                if(model.Avatar != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{model.Avatar.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using(var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Avatar.CopyTo(fs);
                    }
                }
                employee.AvatarPath = fileName;
                var newEmp = employeeRespository.Create(employee);
                return RedirectToAction("Details", new { id = newEmp.Id });
            }
            return View();
        }
     
        public ViewResult Edit(int id)
        {
            var employee = employeeRespository.Get(id);
            if (employee == null)
            {
                return View("~/Views/Error/EmployeeNotFound.cshtml", id);
            }
            var empEdit = new HomeEditViewModel()
            {
                Id = employee.Id,
                Fullname = employee.Fullname,
                Email = employee.Email,
                AvatarPath = employee.AvatarPath,
                Department = employee.Department
            };
            return View(empEdit);
        }

        
        [HttpPost]
        public IActionResult Edit(HomeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = model.Id,
                    Fullname = model.Fullname,
                    Email = model.Email,
                    Department = model.Department,
                    AvatarPath = model.AvatarPath
                };
                var fileName = string.Empty;
                if (model.Avatar != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{model.Avatar.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Avatar.CopyTo(fs);
                    }
                    employee.AvatarPath = fileName;
                    if (!string.IsNullOrEmpty(model.AvatarPath))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images",model.AvatarPath);
                        System.IO.File.Delete(delFile);
                    }
                }
                
                var editEmp = employeeRespository.Edit(employee);
                if (editEmp != null)
                {
                    return RedirectToAction("Details",new { id = editEmp.Id});
                }
            }
            
            return View();
        }

        public IActionResult Delete(int id)
        {
            if (employeeRespository.Delete(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
