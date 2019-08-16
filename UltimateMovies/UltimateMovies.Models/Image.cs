using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
