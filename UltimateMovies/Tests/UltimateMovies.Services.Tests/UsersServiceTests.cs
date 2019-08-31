using Microsoft.AspNetCore.Identity;
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
    public class UsersServiceTests
    {
        [Fact]
        public void AddMovieToWishListShouldAddMovieToWishListMovies()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Users_AddMovieToWishList_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IUsersService usersService = new UsersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.Movies.Add(new Movie { Name = "Test 1" });

            db.SaveChanges();

            usersService.AddMovieToWishList("Tester", db.Movies.Last().Id);

            int wishListMoviesCount = db.WishListMovies.Count();

            Assert.Equal(1, wishListMoviesCount);
        }

        [Fact]
        public void AddPhoneToUserShouldChangeOrAddPhoneToUser()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Users_AddPhoneToUser_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IUsersService usersService = new UsersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            usersService.AddPhoneToUser("Tester", "080011100");

            Assert.Equal("080011100", db.Users.Last().PhoneNumber);
        }

        [Fact]
        public void GetMoviesFromWishListShouldReturnCollectionOfMovies()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Users_GetMoviesFromWishList_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IUsersService usersService = new UsersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.WishListMovies.Add(new WishListMovie { MovieId = 1, UserId = db.Users.Last().Id });
            db.WishListMovies.Add(new WishListMovie { MovieId = 2, UserId = db.Users.Last().Id });
            db.WishListMovies.Add(new WishListMovie { MovieId = 3, UserId = "101" });

            db.SaveChanges();

            List<Movie> movies = usersService.GetMoviesFromWishList("Tester").ToList();

            int moviesCount = movies.Count();

            Assert.Equal(2, moviesCount);
        }

        [Fact]
        public void RemoveMovieFromWishListShouldRemoveWishListMovieFromRemoveWishListMoviesDB()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Users_RemoveMovieFromWish_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IUsersService usersService = new UsersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.Movies.Add(new Movie { Name = "Test" });

            db.SaveChanges();

            db.WishListMovies.Add(new WishListMovie { MovieId = db.Movies.Last().Id, UserId = db.Users.Last().Id });

            db.SaveChanges();

            usersService.RemoveMovieFromWishList("Tester", db.Movies.Last().Id);

            Assert.Equal(0, db.WishListMovies.Count());
        }
    }
}
