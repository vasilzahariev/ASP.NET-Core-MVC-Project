using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateMovies.Data;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public class CartsService : ICartsService
    {
        private UltimateMoviesDbContext db;

        public CartsService(UltimateMoviesDbContext db)
        {
            this.db = db;
        }

        public void AddMovieToCart(string username, int movieId)
        {
            this.db.CartMovies.Add(new CartMovie
            {
                MovieId = movieId,
                UserId = this.db.Users.FirstOrDefault(u => u.UserName == username).Id
            });

            this.db.SaveChanges();
        }

        public void AddToQuantity(string username, int movieId)
        {
            this.db.CartMovies.FirstOrDefault(c => c.MovieId == movieId
                                             && this.db.Users
                                             .FirstOrDefault(u => u.UserName == username)
                                             .Id == c.UserId)
                .Quantity++;

            this.db.SaveChanges();
        }

        public Dictionary<Movie, int> GetAllMoviesFromUserCart(string username)
        {
            Dictionary<Movie, int> result = new Dictionary<Movie, int>();

            foreach (var cart in this.db.CartMovies)
            {
                if (cart.UserId == this.db.Users.FirstOrDefault(u => u.UserName == username).Id)
                {
                    result.Add(this.db.Movies.FirstOrDefault(m => m.Id == cart.MovieId), cart.Quantity);
                }
            }

            return result;
        }

        public void RemoveFromQuantity(string username, int movieId)
        {
            if (this.db.CartMovies.FirstOrDefault(c => c.MovieId == movieId
                                             && this.db.Users
                                             .FirstOrDefault(u => u.UserName == username)
                                             .Id == c.UserId).Quantity == 1)
            {
                return;
            }

            this.db.CartMovies.FirstOrDefault(c => c.MovieId == movieId
                                             && this.db.Users
                                             .FirstOrDefault(u => u.UserName == username)
                                             .Id == c.UserId)
                .Quantity--;

            this.db.SaveChanges();
        }

        public void RemoveMovieFromCart(string username, int movieId)
        {
            this.db.CartMovies.Remove(this.db.CartMovies.FirstOrDefault(c => c.MovieId == movieId
                                             && this.db.Users
                                             .FirstOrDefault(u => u.UserName == username)
                                             .Id == c.UserId));

            this.db.SaveChanges();
        }
    }
}
