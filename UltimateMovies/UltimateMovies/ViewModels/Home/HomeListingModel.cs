using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.ViewModels.Home
{
    public class HomeListingModel
    {
        public IEnumerable<HomeMovieModelView> HomeMovies { get; set; }

        public MovieGenre GenreFilter { get; set; }

        public string SearchString { get; set; }

        public int MoviesCount { get; set; }

        public string OrderBy { get; set; }

        public List<string> OrderByOptions { get; set; }

        public List<string> OrderByOptionsValues { get; set; }
    }
}
