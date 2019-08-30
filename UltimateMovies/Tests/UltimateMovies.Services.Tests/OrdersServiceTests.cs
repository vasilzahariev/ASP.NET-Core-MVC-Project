using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;
using Xunit;

namespace UltimateMovies.Services.Tests
{
    public class OrdersServiceTests
    {
        [Fact]
        public void GetAllUserAddressesShouldReturnCollectionOfAddresses()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Orders_GetAllUserAddresses_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IOrdersService ordersService = new OrdersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.Addresses.Add(new Address { UserId = db.Users.ToList()[0].Id });
            db.Addresses.Add(new Address { UserId = db.Users.ToList()[0].Id });
            db.Addresses.Add(new Address { UserId = db.Users.ToList()[0].Id });

            db.SaveChanges();

            ICollection<Address> addresses = ordersService.GetAllUserAddresses("Tester");

            int addressCount = addresses.Count();

            Assert.Equal(3, addressCount);
        }

        [Fact]
        public void GetUserShouldReturnUser()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Orders_GetUser_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IOrdersService ordersService = new OrdersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            UMUser user = ordersService.GetUser("Tester");

            Assert.Equal("Tester", user.UserName);
        }

        [Fact]
        public void GetCartMoviesShouldReturnADictionaryOfMoviesAndInts()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Orders_GetCartMovies_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IOrdersService ordersService = new OrdersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.Movies.Add(new Movie { Name = "Test1" });
            db.Movies.Add(new Movie { Name = "Test2" });
            db.Movies.Add(new Movie { Name = "Test3" });

            db.SaveChanges();

            db.CartMovies.Add(new CartMovie
            {
                MovieId = db.Movies.ToList()[0].Id,
                UserId = db.Users.Last().Id,
                Quantity = 1
            });

            db.CartMovies.Add(new CartMovie
            {
                MovieId = db.Movies.ToList()[1].Id,
                UserId = db.Users.Last().Id,
                Quantity = 1
            });

            db.CartMovies.Add(new CartMovie
            {
                MovieId = db.Movies.ToList()[2].Id,
                UserId = db.Users.Last().Id,
                Quantity = 1
            });

            db.SaveChanges();

            Dictionary<Movie, int> cart = ordersService.GetCartMovies("Tester");

            int cartCount = cart.Count();

            Assert.Equal(3, cartCount);
        }

        [Fact]
        public void CreateAnOrderShouldAddAnOrderToTheDatabase()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Orders_CreateAnOrder_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IOrdersService ordersService = new OrdersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.Addresses.Add(new Address { UserId = db.Users.Last().Id });

            db.SaveChanges();

            db.Movies.Add(new Movie { Name = "Test" });

            db.SaveChanges();

            db.CartMovies.Add(new CartMovie
            {
                MovieId = db.Movies.Last().Id,
                UserId = db.Users.Last().Id,
                Quantity = 1
            });

            db.SaveChanges();

            ordersService.CreateAnOrder(10, DeliveryType.Normal, PaymentType.CashOnDelivery, db.Addresses.Last().Id, "Tester", "0888888888");

            Assert.Equal(1, db.Orders.Count());
        }

        [Fact]
        public void PayAnOrderShouldChangeTheOrderStatus()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Orders_PayAnOrder_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IOrdersService ordersService = new OrdersService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.Orders.Add(new Order { UserId = db.Users.Last().Id, OrderStatus = OrderStatus.Unfinished });

            db.SaveChanges();

            ordersService.PayAnOrder("Tester", db.Orders.Last().Id);

            Assert.Equal(OrderStatus.Unprocessed, db.Orders.Last().OrderStatus);
        }

        [Fact]
        public void ProcessOrderShouldChangeTheOrderStatusAndSetTheProcessDateToNow()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Orders_ProcessOrder_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IOrdersService ordersService = new OrdersService(db);

            db.Orders.Add(new Order
            {
                OrderStatus = OrderStatus.Unprocessed,
                OrderDate = DateTime.Now
            });

            db.SaveChanges();

            ordersService.ProcessOrder(db.Orders.Last().Id);

            int isNull = db.Orders.Last().ProcesedDate != null ? 1 : 2;

            Assert.Equal(OrderStatus.Procesed, db.Orders.Last().OrderStatus);
            Assert.Equal(1, isNull);
        }

        [Fact]
        public void DeliverOrderShouldChangeTheOrderStatusAndSetTheProcessDateToNow()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Orders_DeliverOrder_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IOrdersService ordersService = new OrdersService(db);

            db.Orders.Add(new Order
            {
                OrderStatus = OrderStatus.Unprocessed,
                OrderDate = DateTime.Now,
                ProcesedDate = DateTime.Now
            });

            db.SaveChanges();

            ordersService.DeliverOrder(db.Orders.Last().Id);

            int isNull = db.Orders.Last().DeliveredDate != null ? 1 : 2;

            Assert.Equal(OrderStatus.Delivered, db.Orders.Last().OrderStatus);
            Assert.Equal(1, isNull);
        }
    }
}
