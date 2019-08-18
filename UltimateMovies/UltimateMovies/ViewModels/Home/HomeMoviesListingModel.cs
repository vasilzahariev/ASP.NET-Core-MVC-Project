using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Home
{
    public class HomeMoviesListingModel
    {
        public IEnumerable<HomeMovieModelView> HomeMovies { get; set; }
    }
}
