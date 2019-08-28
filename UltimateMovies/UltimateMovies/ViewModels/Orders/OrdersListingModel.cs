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

        public ICollection<CartMovieViewModel> CartMovies { get; set; }

        public UserViewModel ResipientInformation { get; set; }

        public double OverallCartPrice()
        {
            double result = 0;

            foreach (var cartMovie in this.CartMovies)
            {
                result += cartMovie.OverallPrice();
            }

            return result;
        }
    }
}
