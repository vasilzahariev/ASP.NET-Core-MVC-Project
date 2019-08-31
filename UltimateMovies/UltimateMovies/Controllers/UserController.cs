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
        private readonly IMoviesService moviesService;

        public UserController(IUsersService usersService, IMoviesService moviesService)
        {
            this.usersService = usersService;
            this.moviesService = moviesService;
        }
        [HttpGet("/User/AddToWishList/{movieId}")]
        public IActionResult AddToWishList(int movieId)
        {
            this.usersService.AddMovieToWishList(this.User.Identity.Name, movieId);

            return this.Redirect(this.Request.Headers["Referer"].ToString());
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
                    PosterUrl = m.PosterUrl,
                    IsMovieInUserLibrary = this.moviesService.IsMovieInUserLibrary(this.User.Identity.Name, m.Id)
                }).ToList()
            };

            return this.View(wishList);
        }

        [HttpGet("/User/RemoveMovieFromWishList/{movieId}")]
        public IActionResult RemoveMovieFromWishList(int movieId)
        {
            this.usersService.RemoveMovieFromWishList(this.User.Identity.Name, movieId);

            return this.Redirect(this.Request.Headers["Referer"].ToString());
        }

        [HttpGet("/User/Library")]
        public IActionResult Library()
        {
            LibraryListingModel model = new LibraryListingModel
            {
                Movies = this.usersService.GetMoviesFromLibrary(this.User.Identity.Name).Select(x => new LibraryMovieViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PosterUrl = x.PosterUrl
                }).ToList()
            };

            return this.View(model);
        }
    }
}