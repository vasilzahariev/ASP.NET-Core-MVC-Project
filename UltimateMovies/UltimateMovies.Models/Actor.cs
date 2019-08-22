using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UltimateMovies.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string ImdbUrl { get; set; }

        public string PictureUrl { get; set; }

        public virtual ICollection<ActorMovie> Movies { get; set; }
    }
}
