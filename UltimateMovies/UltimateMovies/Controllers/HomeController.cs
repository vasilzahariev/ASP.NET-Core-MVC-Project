using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
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
        public IActionResult Index(string search = "", MovieGenre genre = 0, string sortBy = "")
        {
            if (sortBy == "DAN" || sortBy == "")
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
                    }).ToList().OrderByDescending(m => m.Id).ToList(),
                    GenreFilter = genre,
                    SearchString = search,
                    MoviesCount = 0,
                    OrderBy = sortBy,
                    OrderByOptions = new List<string>
                {
                    "Date Added (Newest)",
                    "Date Added (Oldest)",
                    "Release Date (Newest)",
                    "Release Date (Oldest)"
                },
                    OrderByOptionsValues = new List<string>
                {
                    "DAN",
                    "DAO",
                    "RDN",
                    "RDO"
                }
                };

                return View("Index", model);
            }
            else if (sortBy == "DAO")
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
                    }).ToList().OrderBy(m => m.Id).ToList(),
                    GenreFilter = genre,
                    SearchString = search,
                    MoviesCount = 0,
                    OrderBy = sortBy,
                    OrderByOptions = new List<string>
                {
                    "Date Added (Newest)",
                    "Date Added (Oldest)",
                    "Release Date (Newest)",
                    "Release Date (Oldest)"
                },
                    OrderByOptionsValues = new List<string>
                {
                    "DAN",
                    "DAO",
                    "RDN",
                    "RDO"
                }
                };

                return View("Index", model);
            }
            else if (sortBy == "RDN")
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
                    }).ToList().OrderByDescending(m => m.ReleaseDate).ToList(),
                    GenreFilter = genre,
                    SearchString = search,
                    MoviesCount = 0,
                    OrderBy = sortBy,
                    OrderByOptions = new List<string>
                {
                    "Date Added (Newest)",
                    "Date Added (Oldest)",
                    "Release Date (Newest)",
                    "Release Date (Oldest)"
                },
                    OrderByOptionsValues = new List<string>
                {
                    "DAN",
                    "DAO",
                    "RDN",
                    "RDO"
                }
                };

                return View("Index", model);
            }

            HomeListingModel lastModel = new HomeListingModel
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
                }).ToList().OrderBy(m => m.ReleaseDate).ToList(),
                GenreFilter = genre,
                SearchString = search,
                MoviesCount = 0,
                OrderBy = sortBy,
                OrderByOptions = new List<string>
                {
                    "Date Added (Newest)",
                    "Date Added (Oldest)",
                    "Release Date (Newest)",
                    "Release Date (Oldest)"
                },
                OrderByOptionsValues = new List<string>
                {
                    "DAN",
                    "DAO",
                    "RDN",
                    "RDO"
                }
            };

            return View("Index", lastModel);
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
