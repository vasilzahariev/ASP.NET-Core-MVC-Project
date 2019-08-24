using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Services;
using UltimateMovies.ViewModels.Users;

namespace UltimateMovies.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUsersService usersService;

        public UserController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        [HttpGet("/User/AddToWishList/{movieId}")]
        public IActionResult AddToWishList(int movieId)
        {
            this.usersService.AddMovieToWishList(this.User.Identity.Name, movieId);

            return this.Redirect("/");
        }

        [HttpGet("/User/WishList")]
        public IActionResult WishList()
        {
            WishListListingModel wishList = new WishListListingModel
            {
                WishList = this.usersService.GetMoviesFromWishList(this.User.Identity.Name).Select(m => new WishListModelView
                {
                    MovieId = m.Id,
                    MovieName = m.Name,
                    PosterUrl = m.PosterUrl
                }).ToList()
            };

            return this.View(wishList);
        }
    }
}