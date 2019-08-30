using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.Areas.Admin.ViewModels
{
    public class CartMovieViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PosterUrl { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double OverallPrice()
        {
            return this.Price * this.Quantity;
        }
    }
}
