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
            string posterUrl)
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
                IMDbUrl = imdbUrl
            };

            if (genre2 != null)
            {
                movie.Genre2 = (MovieGenre)genre2;
            }

            if (genre3 != null)
            {
                movie.Genre3 = (MovieGenre)genre3;
            }

            IImageService imageService = new ImageService(this.db);

            if (!imageService.ContainsImage(posterUrl))
            {
                imageService.CreateImage(posterUrl);
            }

            movie.PosterId = imageService.GetImageByUrl(posterUrl);

            this.db.Movies.Add(movie);

            this.db.SaveChanges();

            this.ParseActors(actors, movie.Id);
        }

        private void ParseActors(string actors, int movieId)
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
                    MovieId = movieId
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
    }
}
