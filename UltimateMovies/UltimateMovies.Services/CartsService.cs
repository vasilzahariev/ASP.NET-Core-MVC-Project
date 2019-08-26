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
    }
}
