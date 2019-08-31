using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Movies
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public double Score { get; set; }

        public string Comment { get; set; }

        public string Username { get; set; }

        public int MovieId { get; set; }

        public string UserId { get; set; }
    }
}
