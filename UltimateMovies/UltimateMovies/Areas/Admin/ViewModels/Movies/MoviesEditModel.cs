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
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Online Price")]
        [Required]
        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
        public double OnlinePrice { get; set; }

        [Display(Name = "Blu-Ray Price")]
        [Required]
        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
        public double BluRayPrice { get; set; }

        [Display(Name = "DVD Price")]
        [Required]
        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
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
        [Range(1, 1000, ErrorMessage = "Length should be between 1 and 1000 mins!")]
        public int Length { get; set; }

        [Required]
        [Display(Name = "IMDb Score")]
        [Range(0.00, 10.00, ErrorMessage = "The IMDb score should be between 0.00 and 10.00")]
        public double IMDbScore { get; set; }

        [Display(Name = "Rotten Tomatoes Score")]
        [Required]
        [Range(0, 100, ErrorMessage = "The Rotten Tomatoes score should be between 0 and 100")]
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
