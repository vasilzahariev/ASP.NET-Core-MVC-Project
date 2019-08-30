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
                PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png"
            };

            this.db.Actors.Add(actor);

            this.db.SaveChanges();
        }

        public void CreateActor(string name, DateTime birthDate, string imdbUrl, string pictureUrl)
        {
            this.db.Actors.Add(new Actor
            {
                Name = name,
                BirthDate = birthDate,
                ImdbUrl = imdbUrl,
                PictureUrl = pictureUrl
            });

            this.db.SaveChanges();
        }

        public void Edit(int id, string name, DateTime birthDate, string iMDbUrl, string pictureUrl)
        {
            this.db.Actors.FirstOrDefault(a => a.Id == id).Name = name;
            this.db.Actors.FirstOrDefault(a => a.Id == id).BirthDate = birthDate;
            this.db.Actors.FirstOrDefault(a => a.Id == id).ImdbUrl = iMDbUrl;
            this.db.Actors.FirstOrDefault(a => a.Id == id).PictureUrl = pictureUrl;

            this.db.SaveChanges();
        }

        public Actor GetActor(int id)
        {
            if (!this.db.Actors.Any(a => a.Id == id))
            {
                return null;
            }

            return this.db.Actors.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Movie> GetActorsMoviesAndPosters(int actorId)
        {
            return this.db.ActorsMovies.ToList().FindAll(am => am.ActorId == actorId).Select(am => this.db.Movies.FirstOrDefault(m => m.Id == am.MovieId)).ToList();
        }

        public IEnumerable<Actor> GetAllActors()
        {
            return this.db.Actors;
        }

        public void Remove(int actorId)
        {
            this.db.ActorsMovies.RemoveRange(this.db.ActorsMovies.ToList().FindAll(am => am.ActorId == actorId));

            this.db.Actors.Remove(this.db.Actors.FirstOrDefault(a => a.Id == actorId));

            this.db.SaveChanges();
        }
    }
}
