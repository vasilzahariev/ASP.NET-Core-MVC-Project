using System;
using System.Collections.Generic;
using System.Text;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.Services
{
    public interface IMoviesService
    {
        void CreateMovie(string name, double onlinePrice, double bluRayPrice, double dvdPrice, string description, string directors, MovieGenre genre, MovieGenre? genre2, MovieGenre? genre3, DateTime releaseDate, int length, double imdbScore, int rottenTomatoes, string imdbUrl, string actors, string posterUrl, string trailerUrl);

        Movie GetMovie(int id);

        IEnumerable<Movie> GetAllMovies();

        ICollection<Actor> GetActorsNames(int movieId);

        bool IsMovieInUserWishList(string username, int movieId);
    }
}
