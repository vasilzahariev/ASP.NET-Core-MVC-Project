using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public interface ICartsService
    {
        Dictionary<Movie, int> GetAllMoviesFromUserCart(string username);

        void AddMovieToCart(string username, int movieId);

        void AddToQuantity(string username, int movieId);

        void RemoveFromQuantity(string username, int movieId);

        void RemoveMovieFromCart(string username, int movieId);
    }
}
