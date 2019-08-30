using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models;

namespace UltimateMovies.ViewModels.Actors
{
    public class ActorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImdbUrl { get; set; }

        public string PictureUrl { get; set; }

        public ICollection<Movie> Movies { get; set; }

    }
}
