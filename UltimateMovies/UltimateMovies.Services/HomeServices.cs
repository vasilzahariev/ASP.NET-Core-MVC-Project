using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public class HomeServices : IHomeServices
    {
        private UltimateMoviesDbContext db;

        public HomeServices(UltimateMoviesDbContext db)
        {
            this.db = db;
        }

        public List<Movie> GetMovies()
        {
            return this.db.Movies.ToList();
        }

        public bool IsMovieInUserWishlist(string username, int movieId)
        {
            //throw new Exception(this.db.WishListMovies.Any(x => x.UserId == this.db.Users.FirstOrDefault(u => u.UserName == username).Id && x.MovieId == movieId).ToString());
            return this.db.WishListMovies.Any(x => x.UserId == this.db.Users.FirstOrDefault(u => u.UserName == username).Id && x.MovieId == movieId);
        }
    }
}
