using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Addresses
{
    public class AddressEditModel
    {
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
        public int Postcode { get; set; }
    }
}
