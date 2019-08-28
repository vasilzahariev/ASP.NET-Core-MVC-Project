using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Models.Enums;
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

        //[HttpGet("/{genre}")]
        [HttpGet("/")]
        public IActionResult Index(string search = "", MovieGenre genre = 0)
        {
            HomeListingModel model = new HomeListingModel
            {
                HomeMovies = this.homeServices.GetMovies().Select(x => new HomeMovieModelView
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.PosterUrl,
                    IMDbScore = x.IMDbScore,
                    Length = x.Length,
                    ReleaseDate = x.ReleaseDate,
                    RottenTomatoes = x.RottenTomatoes,
                    IsInUserWishList = this.User.Identity.IsAuthenticated ? this.homeServices.IsMovieInUserWishlist(this.User.Identity.Name, x.Id) : false,
                    Genre = x.Genre,
                    Genre2 = x.Genre2,
                    Genre3 = x.Genre3
                }),
                GenreFilter = genre,
                SearchString = search,
                MoviesCount = 0
            };

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Index(string search)
        {
            return this.Content(search);
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
