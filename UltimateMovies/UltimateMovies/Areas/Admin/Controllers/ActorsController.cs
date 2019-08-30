using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Areas.Admin.ViewModels;
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
                                              Name = a.Name
                                          })
            };

            return this.View(actors);
        }
    }
}