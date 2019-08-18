using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Information { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Postcode { get; set; }
    }
}
