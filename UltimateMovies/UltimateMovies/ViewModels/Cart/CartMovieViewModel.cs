using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Cart
{
    public class CartMovieViewModel
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string MoviePoster { get; set; }

        public double SumOverAllPrice()
        {
            return this.Price * this.Quantity;
        }
    }
}
