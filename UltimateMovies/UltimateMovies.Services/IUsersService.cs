using System;
using System.Collections.Generic;
using System.Text;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public interface IUsersService
    {
        void AddMovieToWishList(string username, int movieId);
        List<Movie> GetMoviesFromWishList(string username);
    }
}
