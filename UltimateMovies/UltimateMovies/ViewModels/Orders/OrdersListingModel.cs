using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models;

namespace UltimateMovies.ViewModels.Orders
{
    public class OrdersListingModel
    {
        public ICollection<AddressViewModel> Addresses { get; set; }

        public ICollection<CartMovieViewModel> Movies { get; set; }
    }
}
