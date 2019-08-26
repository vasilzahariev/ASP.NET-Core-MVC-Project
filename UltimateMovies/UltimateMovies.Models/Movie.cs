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
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public double OnlinePrice { get; set; }

        [Required]
        public double BluRayPrice { get; set; }

        [Required]
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
        public int Length { get; set; }

        [Required]
        public double IMDbScore { get; set; }

        [Required]
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
    }
}
