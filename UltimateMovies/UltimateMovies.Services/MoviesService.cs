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

        public ICollection<Actor> GetActorsNames(int movieId)
        {
            List<Actor> results = new List<Actor>();

            foreach (var am in this.db.ActorsMovies)
            {
                if (am.MovieId == movieId)
                {
                    results.Add(this.db.Actors.FirstOrDefault(a => a.Id == am.ActorId));
                }
            }

            return results;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return this.db.Movies.ToList();
        }

        public bool IsMovieInUserWishList(string username, int movieId)
        {
            if (!this.db.Movies.Any(x => x.Id == movieId))
            {
                return false;
            }

            return this.db.WishListMovies.Any(x => x.MovieId == movieId && x.UserId == this.db.Users.FirstOrDefault(u => u.UserName == username).Id);
        }

        public void SuggestAMovie(string name, string imdburl)
        {
            if (!this.db.SuggestedMovies.Any(s => s.Name.ToLower() == name.ToLower() && s.IMDbUrl.ToLower() == imdburl.ToLower()))
            {
                SuggestedMovie suggestedMovie = new SuggestedMovie
                {
                    Name = name,
                    IMDbUrl = imdburl
                };

                this.db.SuggestedMovies.Add(suggestedMovie);

                this.db.SaveChanges();
            }
        }

        public void EditAMovie(int id, string name, double onlinePrice, double bluRayPrice, double dvdPrice,
                                string description, string directors, MovieGenre genre, MovieGenre? genre2,
                                MovieGenre? genre3, DateTime releaseDate, int length, double imdbScore,
                                int rottenTomatoes, string imdbUrl, string posterUrl,
                                string trailerUrl)
        {
            this.db.Movies.FirstOrDefault(m => m.Id == id).Name = name;
            this.db.Movies.FirstOrDefault(m => m.Id == id).OnlinePrice = onlinePrice;
            this.db.Movies.FirstOrDefault(m => m.Id == id).BluRayPrice = bluRayPrice;
            this.db.Movies.FirstOrDefault(m => m.Id == id).DvdPrice = dvdPrice;
            this.db.Movies.FirstOrDefault(m => m.Id == id).Description = description;
            this.db.Movies.FirstOrDefault(m => m.Id == id).Directors = directors;
            this.db.Movies.FirstOrDefault(m => m.Id == id).Genre = genre;
            this.db.Movies.FirstOrDefault(m => m.Id == id).Genre2 = (MovieGenre)genre2;
            this.db.Movies.FirstOrDefault(m => m.Id == id).Genre3 = (MovieGenre)genre3;
            this.db.Movies.FirstOrDefault(m => m.Id == id).ReleaseDate = releaseDate;
            this.db.Movies.FirstOrDefault(m => m.Id == id).Length = length;
            this.db.Movies.FirstOrDefault(m => m.Id == id).IMDbScore = imdbScore;
            this.db.Movies.FirstOrDefault(m => m.Id == id).RottenTomatoes = rottenTomatoes;
            this.db.Movies.FirstOrDefault(m => m.Id == id).IMDbUrl = imdbUrl;
            this.db.Movies.FirstOrDefault(m => m.Id == id).PosterUrl = posterUrl;
            this.db.Movies.FirstOrDefault(m => m.Id == id).TrailerUrl = trailerUrl;

            this.db.SaveChanges();
        }

        public void RemoveActorFromMovie(int actorId, int movieId)
        {
            this.db.ActorsMovies.Remove(this.db.ActorsMovies.FirstOrDefault(am => am.ActorId == actorId && am.MovieId == movieId));

            this.db.SaveChanges();
        }

        public ICollection<Actor> GetActorsWhoAreNotInThisMovie(int movieId)
        {
            List<Actor> result = new List<Actor>();

            foreach (var actor in this.db.Actors)
            {
                if (!this.db.ActorsMovies.Any(am => am.ActorId == actor.Id && am.MovieId == movieId))
                {
                    result.Add(actor);
                }
            }

            return result;
        }

        public void AddActorToMovie(int actorId, int movieId)
        {
            this.db.ActorsMovies.Add(new ActorMovie
            {
                ActorId = actorId,
                MovieId = movieId
            });

            this.db.SaveChanges();
        }

        public ICollection<SuggestedMovie> GetAllSuggestedMovies()
        {
            return this.db.SuggestedMovies.ToList();
        }

        public void RemoveSuggestedMovie(int id)
        {
            this.db.SuggestedMovies.Remove(this.db.SuggestedMovies.FirstOrDefault(s => s.Id == id));

            this.db.SaveChanges();
        }

        public int GetMovieId(string name)
        {
            return this.db.Movies.FirstOrDefault(m => m.Name == name).Id;
        }

        public bool Exists(int id)
        {
            return this.db.Movies.Any(m => m.Id == id);
        }

        public void BuyDigital(int id, string username)
        {
            if (this.db.LibraryMovies.Any(l => l.MovieId == id && l.UserId == this.db.Users.FirstOrDefault(u => u.UserName == username).Id))
            {
                return;
            }

            this.db.LibraryMovies.Add(new LibraryMovie
            {
                MovieId = id,
                UserId = this.db.Users.FirstOrDefault(u => u.UserName == username).Id
            });

            this.db.SaveChanges();
        }

        public bool IsMovieInUserLibrary(string username, int movieId)
        {
            return this.db.LibraryMovies.Any(l => l.MovieId == movieId && l.UserId == this.db.Users.FirstOrDefault(u => u.UserName == username).Id);
        }

        public ICollection<Review> GetMovieReviews(int movieId)
        {
            return this.db.Reviews.ToList().FindAll(x => x.MovieId == movieId);
        }

        public void AddComment(int movieId, string username, double score, string comment)
        {
            this.db.Reviews.Add(new Review
            {
                Comment = comment,
                MovieId = movieId,
                Score = score,
                UserId = this.db.Users.FirstOrDefault(u => u.UserName == username).Id
            });

            this.db.SaveChanges();
        }
    }
}
