using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UltimateMovies.Models.Enums;
using System.Text;

namespace UltimateMovies.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
        public double OnlinePrice { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
        public double BluRayPrice { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
        public double DvdPrice { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Directors { get; set; }

        [Required]
        public MovieGenre Genre { get; set; }

        public MovieGenre Genre2 { get; set; }

        public MovieGenre Genre3 { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Length should be between 1 and 1000 mins!")]
        public int Length { get; set; }

        [Required]
        [Range(0.00, 10.00, ErrorMessage = "The IMDb score should be between 0.00 and 10.00")]
        public double IMDbScore { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The Rotten Tomatoes score should be between 0 and 100")]
        public int RottenTomatoes { get; set; }

        [Required]
        public string IMDbUrl { get; set; }

        [Required]
        public string TrailerUrl { get; set; }

        [Required]
        public string PosterUrl { get; set; }

        public virtual ICollection<ActorMovie> Actors { get; set; }

        public virtual ICollection<WishListMovie> WishList { get; set; }

        public virtual ICollection<CartMovie> Cart { get; set; }

        public virtual ICollection<LibraryMovie> Libraries { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
