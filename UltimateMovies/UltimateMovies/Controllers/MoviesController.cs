using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Models;
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

            return this.View(movie);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateMovie()
        {

            return this.View();
        }

        [HttpPost]
        public IActionResult CreateMovie(MoviesInputModel movie)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["ValidationError"] = this.ModelState.ValidationState.ToString();
                return this.Redirect("/");
            }

            this.moviesService.CreateMovie(movie.Name, movie.OnlinePrice, movie.BluRayPrice, movie.DvdPrice,
                movie.Description, movie.Directors, movie.Genre, movie.Genre2, movie.Genre3, movie.ReleaseDate,
                movie.Length, movie.IMDbScore, movie.RottenTomatoes, movie.IMDbUrl, movie.Actors, movie.PosterUrl, movie.TrailerUrl);

            return this.Redirect("~/");
        }
    }
}