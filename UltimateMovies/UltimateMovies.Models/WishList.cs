using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public int UserId { get; set; }

        public virtual UMUser User { get; set; }
    }
}
