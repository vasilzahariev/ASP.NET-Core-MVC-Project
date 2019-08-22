using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Services;
using UltimateMovies.ViewModels;
using UltimateMovies.ViewModels.Home;

namespace UltimateMovies.Controllers
{
    public class HomeController : Controller
    {
        private IHomeServices homeServices;

        public HomeController(IHomeServices homeServices)
        {
            this.homeServices = homeServices;
        }

        public IActionResult Index()
        {
            HomeMoviesListingModel model = new HomeMoviesListingModel
            {
                HomeMovies = this.homeServices.GetMovies().Select(x => new HomeMovieModelView
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.PosterUrl,
                    IMDbScore = x.IMDbScore,
                    Length = x.Length,
                    ReleaseDate = x.ReleaseDate,
                    RottenTomatoes = x.RottenTomatoes
                })
            };

            return View("Index", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[HttpGet("/Home/Error/{statusErrorCode}")]
        //public IActionResult StatusCodeError(int statusErrorCode)
        //{
        //    return this.Content(statusErrorCode.ToString());
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
