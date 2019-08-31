using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Users
{
    public class LibraryListingModel
    {
        public IEnumerable<LibraryMovieViewModel> Movies { get; set; }
    }
}
