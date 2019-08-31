using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;
using UltimateMovies.Services;
using UltimateMovies.ViewModels.Home;
using UltimateMovies.ViewModels.Movies;

namespace UltimateMovies.Controllers
{
    public class MoviesController : Controller
    {
        private IMoviesService moviesService;
        private readonly IUsersService usersService;

        public MoviesController(IMoviesService moviesService, IUsersService usersService)
        {
            this.moviesService = moviesService;
            this.usersService = usersService;
        }

        [HttpGet("/Movies/Details/{movieId}")]
        public IActionResult Details(int movieId)
        {
            Movie m = this.moviesService.GetMovie(movieId);

            MoviesViewModel movie = new MoviesViewModel();
            movie.Id = m.Id;
            movie.BlueRayPrice = m.BluRayPrice;
            movie.OnlinePrice = m.OnlinePrice;
            movie.PosterUrl = m.PosterUrl;
            movie.Genre = m.Genre;
            movie.Genre2 = m.Genre2;
            movie.Genre3 = m.Genre3;
            movie.Description = m.Description;
            movie.DvdPrice = m.DvdPrice;
            movie.Directors = m.Directors;
            movie.IMDbScore = m.IMDbScore;
            movie.IMDbUrl = m.IMDbUrl;
            movie.ReleaseDate = m.ReleaseDate;
            movie.Length = m.Length;
            movie.RottenTomatoes = m.RottenTomatoes;
            movie.Name = m.Name;
            movie.Actors = this.moviesService.GetActorsNames(m.Id);
            movie.TrailerUrl = m.TrailerUrl;
            movie.IsInUserWishList = this.User.Identity.IsAuthenticated ? this.moviesService.IsMovieInUserWishList(this.User.Identity.Name, m.Id) : false;
            movie.IsInUserLibrary = this.User.Identity.IsAuthenticated ? this.moviesService.IsMovieInUserLibrary(this.User.Identity.Name, m.Id) : false;
            movie.Reviews = this.moviesService.GetMovieReviews(m.Id).Select(x => new ReviewViewModel
            {
                Comment = x.Comment,
                MovieId = x.MovieId,
                Id = x.Id,
                Score = x.Score,
                UserId = x.UserId,
                Username = this.usersService.GetUserById(x.UserId).UserName
            }).ToList();

            return this.View(movie);
        }

        [HttpPost("/Movies/Details/{movieId}")]
        public IActionResult Details(int movieId, double score, string comment)
        {
            if (string.IsNullOrEmpty(comment) || (score > 0 && score <= 10) == false)
            {
                return this.Redirect($"/Movies/Details/{movieId}");
            }

            this.moviesService.AddComment(movieId, this.User.Identity.Name, score, comment);

            return this.Redirect($"/Movies/Details/{movieId}");
        }


        [HttpGet("/Movies/Suggest/")]
        public IActionResult Suggest(string movieName)
        {
            SuggestMovieInputModel model = new SuggestMovieInputModel { Name = movieName };

            return this.View(model);
        }

        // TODO: Make it so you get message of success
        [HttpPost("/Movies/Suggest/")]
        public IActionResult Suggest(SuggestMovieInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Suggest");
            }

            this.moviesService.SuggestAMovie(input.Name, input.IMDbUrl);

            ViewData["Message"] = "Thank you for the suggestion, we are going to look at it and try to get the movie up as soon as possible!";

            return this.Redirect("/");
        }

        [Authorize]
        [HttpGet("/Movies/BuyDigital/{id}")]
        public IActionResult BuyDigital(int id)
        {
            if (this.moviesService.Exists(id) && !this.moviesService.IsMovieInUserLibrary(this.User.Identity.Name, id))
            {
                Movie movie = this.moviesService.GetMovie(id);

                DigitalMoviePayModel model = new DigitalMoviePayModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    PosterUrl = movie.PosterUrl,
                    Price = movie.OnlinePrice
                };

                return this.View(model);
            }

            return this.Redirect("/");
        }

        [Authorize]
        [HttpPost("/Movies/BuyDigital/{id}")]
        public IActionResult BuyDigital(int Id, double Price)
        {
            this.moviesService.BuyDigital(Id, this.User.Identity.Name);

            return this.Redirect("/User/Library");
        }

        [Authorize]
        [HttpGet("/Movies/Watch/{id}")]
        public IActionResult Watch(int id)
        {
            if (this.moviesService.IsMovieInUserLibrary(this.User.Identity.Name, id))
            {
                Movie movie = this.moviesService.GetMovie(id);

                WatchMovieViewModel model = new WatchMovieViewModel
                {
                    Id = movie.Id,
                    MovieUrl = movie.TrailerUrl,
                    Name = movie.Name
                };

                return this.View(model);
            }

            return this.Redirect("/");
        }
    }
}