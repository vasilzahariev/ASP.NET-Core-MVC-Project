using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateMovies.Models
{
    public class LibraryMovie
    {
        public string UserId { get; set; }

        public virtual UMUser User { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
