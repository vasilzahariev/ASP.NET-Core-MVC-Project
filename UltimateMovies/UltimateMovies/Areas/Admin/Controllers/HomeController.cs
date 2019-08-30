using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UltimateMovies.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        [HttpGet("/Admin/")]
        public IActionResult Index()
        {
            return Redirect("/Admin/Actors/");
        }
    }
}