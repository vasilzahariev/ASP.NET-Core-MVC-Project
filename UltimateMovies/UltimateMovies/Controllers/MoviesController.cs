using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Services;
using UltimateMovies.ViewModels.Home;

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
            return this.View();
        }

        [HttpGet]
        public IActionResult CreateMovie()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateMovie(MoviesInputModel movie)
        {
            this.moviesService.CreateMovie(movie.Name, movie.OnlinePrice, movie.BluRayPrice, movie.DvdPrice,
                movie.Description, movie.Directors, movie.Genre, movie.Genre2, movie.Genre3, movie.ReleaseDate,
                movie.Length, movie.IMDbScore, movie.RottenTomatoes, movie.IMDbUrl, movie.Actors, movie.PosterUrl);

            return this.Redirect("~/");
        }
    }
}