using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public class UsersService : IUsersService
    {
        private UltimateMoviesDbContext db;

        public UsersService(UltimateMoviesDbContext db)
        {
            this.db = db;
        }

        public void AddMovieToWishList(string username, int movieId)
        {
            UMUser user = this.db.Users.FirstOrDefault(u => u.UserName == username);
            Movie movie = this.db.Movies.FirstOrDefault(m => m.Id == movieId);

            WishListMovie wishListMovie = new WishListMovie();

            wishListMovie.MovieId = movie.Id;
            wishListMovie.UserId = user.Id;

            if (user.WishList == null)
            {
                user.WishList = new HashSet<WishListMovie>();
            }

            user.WishList.Add(wishListMovie);
            this.db.WishListMovies.Add(wishListMovie);

            this.db.SaveChanges();
        }

        public List<Movie> GetMoviesFromWishList(string username)
        {
            UMUser user = this.db.Users.FirstOrDefault(u => u.UserName == username);
            List<Movie> result = new List<Movie>();

            foreach (WishListMovie wishListMovie in this.db.WishListMovies)
            {
                if (wishListMovie.UserId == user.Id)
                {
                    result.Add(this.db.Movies.FirstOrDefault(m => m.Id == wishListMovie.MovieId));
                }
            }

            return result;
        }
    }
}
