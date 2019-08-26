using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateMovies.Models
{
    public class CartMovie
    {
        public string UserId { get; set; }

        public virtual UMUser User { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int Quantity { get; set; }
    }
}
