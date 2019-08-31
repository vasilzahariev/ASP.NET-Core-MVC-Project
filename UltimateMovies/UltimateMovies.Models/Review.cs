using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string Comment { get; set; }

        [Required]
        [Range(0, 10)]
        public double Score { get; set; }

        public string UserId { get; set; }

        public virtual UMUser User { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
