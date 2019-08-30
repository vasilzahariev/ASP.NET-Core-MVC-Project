using System;
using System.Collections.Generic;
using System.Text;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public interface IActorsService
    {
        void CreateActor(string name);

        void CreateActor(string name, DateTime birthDate, string imdbUrl, string pictureUrl);

        Actor GetActor(int id);

        IEnumerable<Actor> GetAllActors();

        ICollection<Movie> GetActorsMoviesAndPosters(int actorId);
        void Edit(int id, string name, DateTime birthDate, string iMDbUrl, string pictureUrl);
        void Remove(int actorId);
    }
}
