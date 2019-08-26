using System;
using System.Collections.Generic;
using System.Text;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public interface IActorsService
    {
        void CreateActor(string name);

        Actor GetActor(int id);

        IEnumerable<Actor> GetAllActors();

        IDictionary<int, KeyValuePair<string, string>> GetActorsMoviesAndPosters(int actorId);
    }
}
