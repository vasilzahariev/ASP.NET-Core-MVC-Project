using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Areas.Admin.ViewModels;
using UltimateMovies.Models;
using UltimateMovies.Services;

namespace UltimateMovies.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly UserManager<UMUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUsersService usersService;

        public UsersController(UserManager<UMUser> userManager, RoleManager<IdentityRole> roleManager, IUsersService usersService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.usersService = usersService;
        }

        [HttpGet("/Admin/Users/")]
        public IActionResult Index()
        {
            UserListingModel model = new UserListingModel
            {
                Users = this.usersService.GetAllUsers().Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FirstName + " " + x.LastName,
                    Role = this.usersService.GetUserRole(x.Id),
                    Username = x.UserName
                })
            };

            return View(model);
        }

        [HttpGet("/Admin/Users/AddAdmin")]
        public async Task<IActionResult> AddAdmin(string id)
        {
            if (this.usersService.CheckIfReal(id))
            {
                await this.userManager.AddToRoleAsync(this.usersService.GetUserById(id), "Admin");
            }

            return this.Redirect("/Admin/Users/");
        }

        [HttpGet("Admin/Users/RemoveAdmin")]
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            if (this.usersService.CheckIfReal(id))
            {
                await this.userManager.RemoveFromRoleAsync(this.usersService.GetUserById(id), "Admin");
            }

            return this.Redirect("/Admin/Users");
        }
    }
}