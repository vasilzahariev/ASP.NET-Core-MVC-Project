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

        void SuggestAMovie(string name, string imdburl);

        void EditAMovie(int id, string name, double onlinePrice, double bluRayPrice, double dvdPrice, string description, string directors, MovieGenre genre, MovieGenre? genre2, MovieGenre? genre3, DateTime releaseDate, int length, double imdbScore, int rottenTomatoes, string imdbUrl, string posterUrl, string trailerUrl);

        void RemoveActorFromMovie(int actorId, int movieId);

        ICollection<Actor> GetActorsWhoAreNotInThisMovie(int movieId);

        void AddActorToMovie(int actorId, int movieId);

        ICollection<SuggestedMovie> GetAllSuggestedMovies();

        void RemoveSuggestedMovie(int id);

        int GetMovieId(string name);

        bool Exists(int id);

        void BuyDigital(int id, string username);

        bool IsMovieInUserLibrary(string username, int movieId);

        ICollection<Review> GetMovieReviews(int movieId);

        void AddComment(int movieId, string username, double score, string comment);
    }
}
