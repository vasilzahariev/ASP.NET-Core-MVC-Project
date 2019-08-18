using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models
{
    public class Library
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        //public virtual ICollection<Show> Shows { get; set; }
    }
}
