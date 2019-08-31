using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Movies
{
    public class DigitalMoviePayModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string PosterUrl { get; set; }
    }
}
