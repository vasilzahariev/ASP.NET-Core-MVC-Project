using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Addresses
{
    public class AddressListingModel
    {
        public ICollection<AddressModelView> Addresses { get; set; }
    }
}
