using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;
using Xunit;

namespace UltimateMovies.Services.Tests
{
    public class HomeServiceTests
    {
        [Fact]
        public void GetMoviesShouldReturnACollectionOfMovies()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Home_GetMovies_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IHomeServices homeServices = new HomeServices(db);

            db.Movies.Add(new Movie { Name = "Test 1" });
            db.Movies.Add(new Movie { Name = "Test 2" });
            db.Movies.Add(new Movie { Name = "Test 3" });
            db.Movies.Add(new Movie { Name = "Test 4" });

            db.SaveChanges();

            List<Movie> movies = homeServices.GetMovies().ToList();

            int moviesCount = movies.Count();

            Assert.Equal(4, moviesCount);
        }

        [Fact]
        public void IsMovieInUserWishlistShouldReturnABoolean()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Home_IsMovieInUserWishlist_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IHomeServices homeServices = new HomeServices(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.Movies.Add(new Movie { Name = "Test 1" });
            db.Movies.Add(new Movie { Name = "Test 2" });

            db.SaveChanges();

            db.WishListMovies.Add(new WishListMovie
            {
                MovieId = db.Movies.ToList()[0].Id,
                UserId = db.Users.Last().Id
            });

            db.SaveChanges();

            string movie1 = homeServices.IsMovieInUserWishlist("Tester", db.Movies.ToList()[0].Id) ? "In" : "Out";
            string movie2 = homeServices.IsMovieInUserWishlist("Tester", db.Movies.ToList()[1].Id) ? "In" : "Out";

            Assert.Equal("In", movie1);
            Assert.Equal("Out", movie2);
        }
    }
}
