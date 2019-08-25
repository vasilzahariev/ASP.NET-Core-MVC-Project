using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.Services
{
    public class MoviesService : IMoviesService
    {
        private UltimateMoviesDbContext db;

        public MoviesService(UltimateMoviesDbContext db)
        {
            this.db = db;
        }

        public void CreateMovie(string name, double onlinePrice, double bluRayPrice, double dvdPrice,
            string description, string directors, MovieGenre genre, MovieGenre? genre2, MovieGenre? genre3,
            DateTime releaseDate, int length, double imdbScore, int rottenTomatoes, string imdbUrl, string actors,
            string posterUrl, string trailerUrl)
        {
            Movie movie = new Movie
            {
                Name = name,
                OnlinePrice = onlinePrice,
                BluRayPrice = bluRayPrice,
                Description = description,
                Directors = directors,
                Genre = genre,
                ReleaseDate = releaseDate,
                Length = length,
                IMDbScore = imdbScore,
                RottenTomatoes = rottenTomatoes,
                IMDbUrl = imdbUrl,
                TrailerUrl = trailerUrl,
                PosterUrl = posterUrl
            };

            if (genre2 != null)
            {
                movie.Genre2 = (MovieGenre)genre2;
            }

            if (genre3 != null)
            {
                movie.Genre3 = (MovieGenre)genre3;
            }

            this.db.Movies.Add(movie);

            this.db.SaveChanges();

            this.ParseActors(actors, movie);
        }

        private void ParseActors(string actors, Movie movie)
        {
            string[] actorsNames = actors.Split("\r\n");
            List<Actor> result = new List<Actor>();
            List<ActorMovie> ams = new List<ActorMovie>();
            IActorsService actorsService = new ActorsService(db);

            foreach (var actorName in actorsNames)
            {
                Actor actor = null;
                bool isActorInDb = false;

                foreach (var AnActor in this.db.Actors)
                {
                    if (actorName == AnActor.Name)
                    {
                        isActorInDb = true;
                        actor = AnActor;

                        break;
                    }
                }

                if (!isActorInDb)
                {
                    actorsService.CreateActor(actorName);
                    actor = this.db.Actors.Last();
                }

                ActorMovie am = new ActorMovie
                {
                    ActorId = actor.Id,
                    Actor = actor,
                    Movie = movie,
                    MovieId = movie.Id
                };

                result.Add(actor);
                ams.Add(am);
            }

            this.db.ActorsMovies.AddRange(ams);

            this.db.SaveChanges();
        }

        private int MoviesCount()
        {
            int count = 0;

            foreach (var movie in this.db.Movies)
            {
                count++;
            }

            return count;
        }

        public Movie GetMovie(int id)
        {
            Movie resultMovie = null;

            foreach (Movie movie in this.db.Movies)
            {
                if (movie.Id == id)
                {
                    resultMovie = movie;

                    foreach (ActorMovie actorMovie in this.db.ActorsMovies)
                    {
                        if (actorMovie.MovieId == id)
                        {
                            resultMovie.Actors.Add(actorMovie);
                        }
                    }

                    break;
                }
            }

            if (resultMovie == null)
            {
                throw new Exception();
            }

            return resultMovie;
        }

        public Dictionary<string, int> GetActorsNames(int movieId)
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            foreach (var am in this.db.ActorsMovies)
            {
                if (am.MovieId == movieId)
                {
                    results[this.db.Actors.FirstOrDefault(x => x.Id == am.ActorId).Name] = am.ActorId;
                }
            }

            return results;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return this.db.Movies;
        }

        public bool IsMovieInUserWishList(string username, int movieId)
        {
            if (!this.db.Movies.Any(x => x.Id == movieId))
            {
                return false;
            }

            return this.db.WishListMovies.Any(x => x.MovieId == movieId && x.UserId == this.db.Users.FirstOrDefault(u => u.UserName == username).Id);
        }
    }
}
