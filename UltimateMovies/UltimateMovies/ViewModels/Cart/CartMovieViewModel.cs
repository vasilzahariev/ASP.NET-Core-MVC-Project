using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Cart
{
    public class CartMovieViewModel
    {
        public string MovieName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
