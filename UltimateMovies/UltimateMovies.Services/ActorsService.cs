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
                PictureId = 2
            };

            this.db.Actors.Add(actor);

            this.db.SaveChanges();
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
