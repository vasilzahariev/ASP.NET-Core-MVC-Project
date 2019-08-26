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
    }
}
