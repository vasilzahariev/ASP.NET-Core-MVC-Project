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
    public class CartsServiceTests
    {
        [Fact]
        public void AddMovieToCartShouldAddMovieAndUserToCartMovies()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Carts_AddMovieToCart_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            ICartsService cartsService = new CartsService(db);

            db.Users.Add(new UMUser
            {
                UserName = "Tester"
            });

            db.SaveChanges();

            db.Movies.Add(new Movie
            {
                Name = "Test Movie"
            });

            db.SaveChanges();

            cartsService.AddMovieToCart(db.Users.ToList()[0].UserName, db.Movies.ToList()[0].Id);

            int cartMoviesCount = db.CartMovies.ToList().Count();
            string cartMoviesUserId = db.CartMovies.ToList()[0].UserId;
            int cartMoviesMovieId = db.CartMovies.ToList()[0].MovieId;

            Assert.Equal(1, cartMoviesCount);
            Assert.Equal(db.Users.ToList()[0].Id, cartMoviesUserId);
            Assert.Equal(db.Movies.ToList()[0].Id, cartMoviesMovieId);
        }

        [Fact]
        public void AddToQuantityShouldIncreaseTheQuantityWithOne()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Carts_AddToQuantity_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            ICartsService cartsService = new CartsService(db);

            db.Users.Add(new UMUser
            {
                UserName = "Test"
            });

            db.SaveChanges();

            db.CartMovies.Add(new CartMovie
            {
                MovieId = 1,
                UserId = db.Users.Last().Id,
                Quantity = 1
            });

            db.SaveChanges();

            cartsService.AddToQuantity("Test", 1);

            int quantity = db.CartMovies.Last().Quantity;

            Assert.Equal(2, quantity);
        }

        [Fact]
        public void GetAllMoviesFromUserCartShouldReturnDictionaryOfMoviesAndInts()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Carts_GetAllMoviesFromUserCart_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            ICartsService cartsService = new CartsService(db);

            db.Users.Add(new UMUser
            {
                UserName = "Test"
            });

            db.SaveChanges();

            db.Movies.Add(new Movie
            {
                Name = "Test Movie"
            });

            db.Movies.Add(new Movie
            {
                Name = "Test Movie 2"
            });

            db.Movies.Add(new Movie
            {
                Name = "Test Movie 3"
            });

            db.SaveChanges();

            db.CartMovies.Add(new CartMovie
            {
                UserId = db.Users.Last().Id,
                MovieId = db.Movies.ToList()[0].Id,
                Quantity = 1
            });

            db.CartMovies.Add(new CartMovie
            {
                UserId = db.Users.Last().Id,
                MovieId = db.Movies.ToList()[1].Id,
                Quantity = 1
            });

            db.CartMovies.Add(new CartMovie
            {
                UserId = db.Users.Last().Id,
                MovieId = db.Movies.ToList()[2].Id,
                Quantity = 1
            });

            db.SaveChanges();

            Dictionary<Movie, int> movies = cartsService.GetAllMoviesFromUserCart("Test");

            int moviesCount = movies.Count();

            Assert.Equal(3, moviesCount);
        }

        [Fact]
        public void RemoveFromQuantityShouldRemoveOneFromQuantity()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Carts_RemoveFromQuantity_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            ICartsService cartsService = new CartsService(db);

            db.Users.Add(new UMUser
            {
                UserName = "Test"
            });

            db.SaveChanges();

            db.CartMovies.Add(new CartMovie
            {
                MovieId = 1,
                UserId = db.Users.Last().Id,
                Quantity = 5
            });

            db.SaveChanges();

            cartsService.RemoveFromQuantity("Test", 1);

            int quantity = db.CartMovies.Last().Quantity;

            Assert.Equal(4, quantity);
        }

        [Fact]
        public void RemoveFromQuantityShouldMakeTheQuantityStayAtOne()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Carts_RemoveFromQuantity2_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            ICartsService cartsService = new CartsService(db);

            db.Users.Add(new UMUser
            {
                UserName = "Test"
            });

            db.SaveChanges();

            db.CartMovies.Add(new CartMovie
            {
                MovieId = 1,
                UserId = db.Users.Last().Id,
                Quantity = 1
            });

            db.SaveChanges();

            cartsService.RemoveFromQuantity("Test", 1);

            int quantity = db.CartMovies.Last().Quantity;

            Assert.Equal(1, quantity);
        }

        [Fact]
        public void RemoveMovieFromCartShouldRemoveAMovieFromTheCart()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Carts_RemoveMovieFromCart_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            ICartsService cartsService = new CartsService(db);

            db.Users.Add(new UMUser
            {
                UserName = "Test"
            });

            db.SaveChanges();

            db.Movies.Add(new Movie
            {
                Name = "Test Movie"
            });

            db.Movies.Add(new Movie
            {
                Name = "Test Movie 2"
            });

            db.SaveChanges();

            db.CartMovies.Add(new CartMovie
            {
                UserId = db.Users.Last().Id,
                MovieId = db.Movies.ToList()[0].Id,
                Quantity = 1
            });

            db.CartMovies.Add(new CartMovie
            {
                UserId = db.Users.Last().Id,
                MovieId = db.Movies.ToList()[1].Id,
                Quantity = 1
            });

            db.SaveChanges();

            cartsService.RemoveMovieFromCart("Test", db.Movies.FirstOrDefault(m => m.Name == "Test Movie 2").Id);

            int cartMoviesCount = db.CartMovies.ToList().FindAll(c => c.UserId == db.Users.Last().Id).Count();

            Assert.Equal(1, cartMoviesCount);
        }
    }
}
