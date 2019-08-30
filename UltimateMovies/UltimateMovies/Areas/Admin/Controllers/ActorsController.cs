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
    public class ActorsController : AdminController
    {
        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        [HttpGet("/Admin/Actors/")]
        public IActionResult Index()
        {
            ActorsListingModel actors = new ActorsListingModel
            {
                Actors = this.actorsService.GetAllActors()
                                          .Select(a => new ActorsListModelView()
                                          {
                                              Id = a.Id,
                                              Name = a.Name,
                                              PictureUrl = a.PictureUrl
                                          })
            };

            return this.View(actors);
        }

        [HttpGet("/Admin/Actors/Create")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("/Admin/Actors/Create")]
        public IActionResult Create(ActorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.actorsService.CreateActor(input.Name, input.BirthDate, input.IMDbUrl, input.PictureUrl);

            return this.Redirect("/Admin/Actors");
        }

        [HttpGet("/Admin/Actors/Edit/{actorId}")]
        public IActionResult Edit(int actorId)
        {
            Actor actor = this.actorsService.GetActor(actorId);

            ActorsEditModel model = new ActorsEditModel
            {
                Id = actor.Id,
                BirthDate = actor.BirthDate,
                IMDbUrl = actor.ImdbUrl,
                Name = actor.Name,
                PictureUrl = actor.PictureUrl
            };

            return this.View(model);
        }

        [HttpPost("/Admin/Actors/Edit/{actorId}")]
        public IActionResult Edit(ActorsEditModel edit)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.actorsService.Edit(edit.Id, edit.Name, edit.BirthDate, edit.IMDbUrl, edit.PictureUrl);

            return this.Redirect("/Admin/Actors/");
        }

        [HttpGet("/Admin/Actors/Remove/{actorId}")]
        public IActionResult Remove(int actorId)
        {
            this.actorsService.Remove(actorId);

            return this.Redirect("/Admin/Actors/");
        }
    }
}