using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UltimateMovies.Controllers
{
    public class MoviesController : Controller
    {
        [HttpGet("Movies/Details/{id}")]
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}