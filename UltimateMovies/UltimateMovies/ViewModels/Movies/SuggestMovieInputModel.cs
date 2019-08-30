using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Movies
{
    public class SuggestMovieInputModel
    {
        [Required]
        public string Name { get; set; }

        [Display(Name = "IMDb Url")]
        [Required]
        public string IMDbUrl { get; set; }
    }
}
