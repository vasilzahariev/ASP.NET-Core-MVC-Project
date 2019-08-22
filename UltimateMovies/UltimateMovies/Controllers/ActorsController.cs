using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Models;
using UltimateMovies.Services;
using UltimateMovies.ViewModels.Actors;

namespace UltimateMovies.Controllers
{
    public class ActorsController : Controller
    {
        private IActorsService actorService;

        public ActorsController(IActorsService actorService)
        {
            this.actorService = actorService;
        }

        [HttpGet("/Actors/Details/{actorId}")]
        public IActionResult Details(int actorId)
        {
            Actor a = this.actorService.GetActor(actorId);

            ActorViewModel actor = new ActorViewModel();

            actor.Id = actorId;
            actor.Name = a.Name;
            actor.ImdbUrl = a.ImdbUrl;
            actor.PictureUrl = a.PictureUrl;
            actor.MoviesPosters = this.actorService.GetActorsMoviesAndPosters(actorId);

            return View(actor);
        }

        [HttpGet("/Actors/")]
        public IActionResult Actors()
        {
            ActorsListingModel actors = new ActorsListingModel
            {
                Actors = this.actorService.GetAllActors()
                                          .Select(a => new ActorsListModelView()
                                          {
                                              Id = a.Id,
                                              Name = a.Name
                                          })
            };

            return this.View(actors);
        }
    }

    //[HttpGet("/Actors/Edit/{actorId}")]
    //public IActionResult Edit(int actorId)
    //{
    //    return this.View();
    //}
}