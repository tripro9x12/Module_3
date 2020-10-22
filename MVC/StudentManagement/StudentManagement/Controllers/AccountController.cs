using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagement.Models.Identities;
using StudentManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //[AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }      

        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                var model = new ApplicationUser()
                {
                    Email = account.Email,
                    UserName = account.Email,
                    City = account.City,
                    Address = account.Address
                };
                //thêm tài khoản:
                var result = await userManager.CreateAsync(user: model, password: account.Password);
                if (result.Succeeded)
                {
                    //đăng nhập luôn:
                    await signInManager.SignInAsync(user: model, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(account);
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
