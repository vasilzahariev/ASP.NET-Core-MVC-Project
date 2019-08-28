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

            if (user == null || movie == null)
            {
                return;
            }

            if (this.db.WishListMovies.Any(x => x.UserId == user.Id && x.MovieId == movie.Id))
            {
                this.RemoveMovieFromWishList(username, movieId);
                return;
            }

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

        public void AddPhoneToUser(string username, string phone)
        {
            this.db.Users.FirstOrDefault(u => u.UserName == username).PhoneNumber = phone;

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

        public UMUser GetUser(string username)
        {
            return this.db.Users.FirstOrDefault(u => u.UserName == username);
        }

        public void RemoveMovieFromWishList(string username, int movieId)
        {
            UMUser user = this.db.Users.FirstOrDefault(u => u.UserName == username);
            Movie movie = this.db.Movies.FirstOrDefault(m => m.Id == movieId);

            if (user == null || movie == null)
            {
                return;
            }

            if (!this.db.WishListMovies.Any(x => x.UserId == user.Id && x.MovieId == movie.Id))
            {
                return;
            }

            if (user.WishList != null)
            {
                user.WishList.Remove(user.WishList.FirstOrDefault(x => x.MovieId == movie.Id && x.UserId == user.Id));
            }

            this.db.WishListMovies.Remove(this.db.WishListMovies.FirstOrDefault(x => x.MovieId == movie.Id && x.UserId == user.Id));

            this.db.SaveChanges();
        }
    }
}
