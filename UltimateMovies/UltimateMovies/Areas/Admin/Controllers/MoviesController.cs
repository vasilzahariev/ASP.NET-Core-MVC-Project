using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Areas.Admin.ViewModels;
using UltimateMovies.Models;
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

        [HttpGet("/Admin/Movies/Create")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("/Admin/Movies/Create")]
        public IActionResult Create(MoviesInputModel movie)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.moviesService.CreateMovie(movie.Name, movie.OnlinePrice, movie.BluRayPrice, movie.DvdPrice,
                movie.Description, movie.Directors, movie.Genre, movie.Genre2, movie.Genre3, movie.ReleaseDate,
                movie.Length, movie.IMDbScore, movie.RottenTomatoes, movie.IMDbUrl, movie.Actors, movie.PosterUrl, movie.TrailerUrl);

            if (movie.IsSuggested)
            {
                this.moviesService.RemoveSuggestedMovie(this.moviesService.GetMovieId(movie.Name));
            }

            return this.Redirect("/Admin/Movies/");
        }

        [HttpGet("/Admin/Movies/Edit/{movieId}")]
        public IActionResult Edit(int movieId)
        {
            Movie movie = this.moviesService.GetMovie(movieId);

            MoviesEditModel model = new MoviesEditModel
            {
                Id = movie.Id,
                Actors = this.moviesService.GetActorsNames(movie.Id).ToList(),
                ActorWANFTM = this.moviesService.GetActorsWhoAreNotInThisMovie(movie.Id).ToList(),
                BluRayPrice = movie.BluRayPrice,
                Description = movie.Description,
                Directors = movie.Directors,
                DvdPrice = movie.DvdPrice,
                Genre = movie.Genre,
                Genre2 = movie.Genre2,
                Genre3 = movie.Genre3,
                IMDbScore = movie.IMDbScore,
                IMDbUrl = movie.IMDbUrl,
                Length = movie.Length,
                Name = movie.Name,
                OnlinePrice = movie.OnlinePrice,
                PosterUrl = movie.PosterUrl,
                ReleaseDate = movie.ReleaseDate,
                RottenTomatoes = movie.RottenTomatoes,
                TrailerUrl = movie.TrailerUrl
            };

            return this.View(model);
        }

        [HttpPost("/Admin/Movies/Edit/{movieId}")]
        public IActionResult Edit(MoviesEditModel edit, string actorId = "", string adding = "")
        {
            if (!this.ModelState.IsValid)
            {
                edit.Actors = this.moviesService.GetActorsNames(edit.Id).ToList();
                edit.ActorWANFTM = this.moviesService.GetActorsWhoAreNotInThisMovie(edit.Id).ToList();

                return this.View(edit);
            }

            this.moviesService.EditAMovie(edit.Id, edit.Name, edit.OnlinePrice, edit.BluRayPrice, edit.DvdPrice,
                                            edit.Description, edit.Directors, edit.Genre, edit.Genre2, edit.Genre3,
                                            edit.ReleaseDate, edit.Length, edit.IMDbScore, edit.RottenTomatoes,
                                            edit.IMDbUrl, edit.PosterUrl, edit.TrailerUrl);
            if (actorId != "")
            {
                if (actorId != "No")
                {
                    int id = int.Parse(actorId);

                    this.moviesService.RemoveActorFromMovie(id, edit.Id);

                    edit.Actors = this.moviesService.GetActorsNames(edit.Id).ToList();
                    edit.ActorWANFTM = this.moviesService.GetActorsWhoAreNotInThisMovie(edit.Id).ToList();

                    return this.View(edit);
                }
                else
                {
                    return this.Redirect("/Admin/Movies/");
                }
            }
            else
            {
                int id = int.Parse(adding);

                this.moviesService.AddActorToMovie(id, edit.Id);

                edit.Actors = this.moviesService.GetActorsNames(edit.Id).ToList();
                edit.ActorWANFTM = this.moviesService.GetActorsWhoAreNotInThisMovie(edit.Id).ToList();

                return this.View(edit);
            }
        }

        [HttpGet("/Admin/Movies/RemoveActor/{actorId}")]
        public IActionResult RemoveActor(int actorId)
        {
            return this.Redirect(this.Request.Headers["Referer"].ToString());
        }

        [HttpGet("/Admin/Movies/Suggested")]
        public IActionResult Suggested()
        {
            return this.View(this.moviesService.GetAllSuggestedMovies());
        }

        [HttpPost("/Admin/Movies/Suggested")]
        public IActionResult Suggested(int Id, string Name, string IMDbUrl)
        {
            MoviesInputModel model = new MoviesInputModel
            {
                Name = Name,
                IMDbUrl = IMDbUrl,
                IsSuggested = true
            };

            return this.View("Create", model);
        }

        [HttpGet("/Admin/Movies/Suggested/Remove/{id}")]
        public IActionResult RemoveSuggested(int id)
        {
            this.moviesService.RemoveSuggestedMovie(id);

            return this.Redirect("/Admin/Movies/Suggested");
        }
    }
}