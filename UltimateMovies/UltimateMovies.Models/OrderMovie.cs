using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateMovies.Models
{
    public class OrderMovie
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
