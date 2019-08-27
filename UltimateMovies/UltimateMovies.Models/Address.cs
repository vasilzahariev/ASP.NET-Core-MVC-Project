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
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string AdditionalInformation { get; set; }

        [Required]
        public string Postcode { get; set; }

        public int UserId { get; set; }

        public virtual UMUser User { get; set; }
    }
}
