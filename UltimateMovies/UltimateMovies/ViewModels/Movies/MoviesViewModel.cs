using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.ViewModels.Movies
{
    public class MoviesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double OnlinePrice { get; set; }

        public double BlueRayPrice { get; set; }

        public double DvdPrice { get; set; }

        public string Description { get; set; }

        public string Directors { get; set; }

        public MovieGenre Genre { get; set; }

        public MovieGenre Genre2 { get; set; }

        public MovieGenre Genre3 { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Length { get; set; }

        public double IMDbScore { get; set; }

        public int RottenTomatoes { get; set; }

        public string IMDbUrl { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public string PosterUrl { get; set; }

        public string TrailerUrl { get; set; }

        public bool IsInUserWishList { get; set; }

        public bool IsInUserLibrary { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}
