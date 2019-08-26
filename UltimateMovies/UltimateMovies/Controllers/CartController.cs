using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Services;

namespace UltimateMovies.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartsService cartsService;

        public CartController(ICartsService cartsService)
        {
            this.cartsService = cartsService;
        }

        [HttpGet("/Cart/Add/{movieId}")]
        public IActionResult Add(int movieId)
        {
            this.cartsService.AddMovieToCart(this.User.Identity.Name, movieId);

            return this.Redirect(this.Request.Headers["Referer"].ToString());
        }

        [HttpGet("/Cart/Plus/{movieId}")]
        public IActionResult Plus(int movieId)
        {
            this.cartsService.AddToQuantity(this.User.Identity.Name, movieId);

            return this.Redirect(this.Request.Headers["Referer"].ToString());
        }

        [HttpGet("/Cart/Minus/{movieId}")]
        public IActionResult Minus(int movieId)
        {
            this.cartsService.RemoveFromQuantity(this.User.Identity.Name, movieId);

            return this.Redirect(this.Request.Headers["Referer"].ToString());
        }

        [HttpGet("/Cart/Remove/{movieId}")]
        public IActionResult Remove(int movieId)
        {
            this.cartsService.RemoveMovieFromCart(this.User.Identity.Name, movieId);

            return this.Redirect(this.Request.Headers["Referer"].ToString());
        }
    }
}