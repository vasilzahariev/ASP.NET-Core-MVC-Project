using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Services;

namespace UltimateMovies.Areas.Admin.Controllers
{
    public class MoviesController : AdminController
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [HttpGet("/Admin/Movies/")]
        public IActionResult Index()
        {
            return View(this.moviesService.GetAllMovies());
        }
    }
}