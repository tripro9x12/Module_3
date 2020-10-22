using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [Authorize(Roles ="System Admin, Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = userManager.Users;
            var model = new List<UserViewModel>();
            model = users.Select(u => new UserViewModel()
            {
                UserId = u.Id,
                Email = u.Email,
                City = u.City,
                Address = u.Address
            }).ToList();
            foreach(var user in model)
            {
                user.RolesName = GetRolesName(user.UserId);
            }
            return View(model);
        }
        public string GetRolesName(string UserId)
        {
            var user = Task.Run(async () => await userManager.FindByIdAsync(UserId)).Result;
            var roles = Task.Run(async () => await userManager.GetRolesAsync(user)).Result;
            return roles != null ? string.Join(", ", roles) : string.Empty;
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = roleManager.Roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    City = model.City,
                    Address = model.Address
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user: user, isPersistent: false);
                    if (!string.IsNullOrEmpty(model.RoleId))
                    {
                        var role = await roleManager.FindByIdAsync(model.RoleId);
                        var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                        if (addRoleResult.Succeeded)
                        {
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            foreach(var error in addRoleResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                   
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Roles = roleManager.Roles;
            var user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                var model = new UserEditViewModel()
                {
                    UserId = user.Id,
                    Email = user.UserName,
                    City = user.City,
                    Address = user.Address
                };
                var rolesName = await userManager.GetRolesAsync(user);
                if(rolesName != null && rolesName.Any())
                {
                    var role = await roleManager.FindByNameAsync(rolesName.FirstOrDefault());
                    model.RoleId = role.Id;
                }
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if(user != null)
                {
                    user.Id = model.UserId;
                    user.UserName = model.Email;
                    user.City = model.City;
                    user.Address = model.Address;
                    user.Email = model.Email;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        var delRole = await userManager.RemoveFromRolesAsync(user, roles);
                        if (!string.IsNullOrEmpty(model.RoleId))
                        {
                            var role = await roleManager.FindByIdAsync(model.RoleId);
                            var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                            if (addRoleResult.Succeeded)
                            {
                                return RedirectToAction("Index", "User");
                            }
                        }
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var role = await userManager.FindByIdAsync(id);
            if(role != null)
            {
                var result = await userManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
    }
}
