using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentManagement.Models;
using StudentManagement.Models.Entities;
using StudentManagement.Models.ViewModels;
using StudentManagement.Service;
using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentService studentService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private const int defaultProvinceId = 15;
        private const int defaultDistrictId = 194;

        public HomeController(ILogger<HomeController> logger, IStudentService studentService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.studentService = studentService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var Students = new List<StudentView>();
            Students = studentService.GetStudents().ToList();
            return View(Students);
        }

        private CreateStudentView CollectData()
        {
            var model = new CreateStudentView();
            model.Provinces = studentService.GetProvinces();
            model.ProvinceId = defaultProvinceId;
            model.Districts = studentService.GetDistricts(defaultProvinceId);
            model.DistrictId = defaultDistrictId;
            model.Wards = studentService.GetWards(defaultDistrictId, defaultProvinceId);
            return model;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(CollectData());
        }
        [HttpPost]
        public IActionResult Create(CreateStudentAdd model)
        {
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    Fullname = model.Fullname,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    ProvinceId = model.ProvinceId,
                    DistrictId = model.DistrictId,
                    WardId = model.WardId,
                    Address = model.Address
                };
                string fileName = string.Empty;
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
                student.Avatar = fileName;
                var result = studentService.CreateStudent(student);
                if(result > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "System error, please try again later!");

            }
            var createView = new CreateStudentView();
            return View(createView);
        }
        [Route("/Home/Districts/{provinceId}")]
        public IActionResult GetDistricts(int provinceId)
        {
            var districts = studentService.GetDistricts(provinceId).ToList();
            return Json(new { districts });
        }

        [Route("/Home/Wards/{districtId}/{provinceId}")]
        public IActionResult GetWards(int districtId, int provinceId)
        {
            var wards = studentService.GetWards(districtId, provinceId);
            return Json(new { wards });
        }

        public IActionResult Detail(int? id)
        {
            try
            {
                int.Parse(id.Value.ToString());
                var model = studentService.GetS(id.Value);
                if(model == null)
                {
                    return View();
                }
     
       
                return View(model);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = studentService.GetStudent(id);
            if (model == null)
            {
                return View();
            }
            var editStudent = new EditStudentView()
            {
                Id = model.Id,
                Address = model.Address,
                AvatarPath = model.Avatar,
                Email = model.Email,
                Fullname = model.Fullname,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                ProvinceId = model.ProvinceId,
                DistrictId = model.DistrictId,
                Provinces = studentService.GetProvinces(),
                Districts = studentService.GetDistricts(model.ProvinceId),
                WardId = model.WardId,
                Wards = studentService.GetWards(model.DistrictId, model.ProvinceId)

            };

            return View(editStudent);
        }
        [HttpPost]
        public IActionResult Edit(UpdateStudentView model)
        {
            if (ModelState.IsValid)
            { 
                var student = new Student()
                {
                    Id = model.Id,
                    Avatar = model.AvatarPath,
                    Gender = model.Gender,
                    Address = model.Address,
                    DistrictId = model.DistrictId,
                    Email = model.Email,
                    Fullname = model.Fullname,
                    ProvinceId = model.ProvinceId,
                    PhoneNumber = model.PhoneNumber,
                    WardId = model.WardId,
                    Password = model.Password
                };
                string fileName = string.Empty;
                if(model.Avatar != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{model.Avatar.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Avatar.CopyTo(fs);
                    }
                    student.Avatar = fileName;
                    if (!string.IsNullOrEmpty(model.AvatarPath))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", model.AvatarPath);
                        System.IO.File.Delete(delFile);
                    }
                }
               
                if (studentService.EditStudent(student)>0)
                {
                    return RedirectToAction("Detail", new { id = model.Id });
                }
                ModelState.AddModelError("","Chỉnh sửa thất bại");
            }
            var editStudent = new EditStudentView();
            return View(editStudent);

        }
        public IActionResult Delete(int id)
        {
            var del = studentService.GetStudent(id);
            if(del != null)
            {
                if(!string.IsNullOrEmpty(del.Avatar))
                {
                    string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", del.Avatar);
                    System.IO.File.Delete(delFile);
                }
                studentService.DeleteStudent(del.Id);
                
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [Route("/Home/Delete/{studentId}")]
        public IActionResult DeleteStaff(int studentId)
        {
            var deleteResult = studentService.DeleteStudent(studentId);
            return Json(new { deleteResult });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
