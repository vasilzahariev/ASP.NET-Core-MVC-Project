using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public class ActorsService : IActorsService
    {
        private UltimateMoviesDbContext db;

        public ActorsService(UltimateMoviesDbContext db)
        {
            this.db = db;
        }
        public void CreateActor(string name)
        {
            Actor actor = new Actor
            {
                Name = name,
                BirthDate = new DateTime(1, 1, 1, 0, 0, 0),
                ImdbUrl = "https://www.imdb.com/",
                PictureId = 1
            };

            IImageService imageService = new ImageService(this.db);

            actor.Picture = imageService.GetImage(actor.PictureId);

            this.db.Actors.Add(actor);

            this.db.SaveChanges();
        }

        public Actor GetActor(int id)
        {
            return this.db.Actors.FirstOrDefault(a => a.Id == id);
        }

        public Image GetActorPicture(int pictureId)
        {
            return this.db.Images.FirstOrDefault(i => i.Id == pictureId);
        }

        public IDictionary<int, KeyValuePair<string, string>> GetActorsMoviesAndPosters(int actorId)
        {
            Dictionary<int, KeyValuePair<string, string>> result = new Dictionary<int, KeyValuePair<string, string>>();

            foreach (var am in this.db.ActorsMovies)
            {
                if (am.ActorId == actorId)
                {
                    result[am.MovieId] = new KeyValuePair<string, string>
                        (this.db.Movies.FirstOrDefault(m => m.Id == am.MovieId).Name,
                         this.db.Images.FirstOrDefault(i =>
                                        i.Id == this.db.Movies.FirstOrDefault(m => m.Id == am.MovieId).PosterId).ImageUrl);
                }
            }

            return result;
        }

        public IEnumerable<Actor> GetAllActors()
        {
            return this.db.Actors;
        }

        private int ActorsCount()
        {
            int count = 0;

            foreach (var actor in this.db.Actors)
            {
                count++;
            }

            return count;
        }
    }
}
