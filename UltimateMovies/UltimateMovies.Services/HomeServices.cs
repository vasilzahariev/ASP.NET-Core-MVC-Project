using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public class HomeServices : IHomeServices
    {
        private UltimateMoviesDbContext db;

        public HomeServices(UltimateMoviesDbContext db)
        {
            this.db = db;
        }

        public string GetImageUrl(int id)
        {
            return this.db.Images.FirstOrDefault(x => x.Id == id).ImageUrl;
        }

        public List<Movie> GetMovies()
        {
            return this.db.Movies.ToList();
        }
    }
}
