using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.Areas.Admin.ViewModels
{
    public class MoviesEditModel
    {
        public int Id { get; set; }

        [Display(Name = "Movie's  name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Online Price")]
        [Required]
        public double OnlinePrice { get; set; }

        [Display(Name = "Blu-Ray Price")]
        [Required]
        public double BluRayPrice { get; set; }

        [Display(Name = "DVD Price")]
        [Required]
        public double DvdPrice { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Directors")]
        [Required]
        public string Directors { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public MovieGenre Genre { get; set; }

        [Display(Name = "Genre 2")]
        public MovieGenre Genre2 { get; set; }

        [Display(Name = "Genre 3")]
        public MovieGenre Genre3 { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Movie's Length")]
        public int Length { get; set; }

        [Required]
        [Range(0, 10)]
        [Display(Name = "IMDb Score")]
        public double IMDbScore { get; set; }

        [Display(Name = "Rotten Tomatoes Score")]
        [Required]
        [Range(0, 100)]
        public int RottenTomatoes { get; set; }

        [Display(Name = "IMDb's Page Url")]
        [Required]
        public string IMDbUrl { get; set; }

        [Display(Name = "Movie's Poster Url")]
        [Required]
        public string PosterUrl { get; set; }

        [Display(Name = "Movie's Trailer Url")]
        [Required]
        public string TrailerUrl { get; set; }

        public List<Actor> Actors { get; set; }

        public List<Actor> ActorWANFTM { get; set; }

        public string Result { get; set; }
    }
}
