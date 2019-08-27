using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Addresses
{
    public class AddressModelView
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string AdditionalInforamtion { get; set; }

        public int Postcode { get; set; }
    }
}
