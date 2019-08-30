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

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [HttpGet("Movies/Details/{movieId}")]
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

            return this.View(movie);
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
    }
}