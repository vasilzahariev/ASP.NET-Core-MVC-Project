using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Cart
{
    public class CartListingModel
    {
        public IEnumerable<CartMovieViewModel> CartMovies { get; set; }

        public double SumAll()
        {
            double result = 0;

            foreach (var cartMovie in this.CartMovies)
            {
                result += cartMovie.Price;
            }

            return result;
        }
    }
}
