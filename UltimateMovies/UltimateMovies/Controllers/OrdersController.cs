using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;
using UltimateMovies.Services;
using UltimateMovies.ViewModels.Orders;

namespace UltimateMovies.Controllers
{
    //TODO: Add a barier, so if your cart is empty it shouldn't go here


    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IAddressesService addressesService;
        private readonly IUsersService usersService;
        private readonly IMoviesService moviesService;

        public OrdersController(IOrdersService ordersService, IAddressesService addressesService, IUsersService usersService, IMoviesService moviesService)
        {
            this.ordersService = ordersService;
            this.addressesService = addressesService;
            this.usersService = usersService;
            this.moviesService = moviesService;
        }

        [HttpGet("/Orders/Create")]
        public IActionResult Create()
        {
            OrdersListingModel model = new OrdersListingModel
            {
                Addresses = this.ordersService.GetAllUserAddresses(this.User.Identity.Name).Select(x => new AddressViewModel
                {
                    Id = x.Id,
                    AdditionalInformation = x.AdditionalInformation,
                    City = x.City,
                    Country = x.Country,
                    Postcode = x.Postcode,
                    Street = x.Street
                }).ToList(),
                ResipientInformation = new UserViewModel
                {
                    FullName = this.ordersService.GetUser(this.User.Identity.Name).FirstName + " " + this.ordersService.GetUser(this.User.Identity.Name).LastName,
                    PhoneNumber = this.ordersService.GetUser(this.User.Identity.Name).PhoneNumber
                },
                CartMovies = this.ordersService.GetCartMovies(this.User.Identity.Name).Select(x => new CartMovieViewModel
                {
                    Id = x.Key.Id,
                    Name = x.Key.Name,
                    PosterUrl = x.Key.PosterUrl,
                    Price = x.Key.BluRayPrice,
                    Quantity = x.Value
                }).ToList()
            };

            return View(model);
        }

        [HttpPost("/Orders/Create")]
        public IActionResult Create(OrderInputModel input)
        {
            this.ordersService.CreateAnOrder(input.CartPrice, input.DeliveryType, input.PaymentType, input.AddressId, this.User.Identity.Name, input.PhoneNumber);

            return this.Redirect("/Orders/Summary");
        }

        [HttpGet("/Orders/Summary")]
        public IActionResult Summary()
        {
            Order order = this.ordersService.GetLastOrder(this.User.Identity.Name);
            UMUser user = this.usersService.GetUser(this.User.Identity.Name);

            if (order.OrderStatus == OrderStatus.Unfinished)
            {
                ViewData["OrderId"] = order.Id;

                return this.View("Pay");
            }

            OrderSummaryViewModel model = new OrderSummaryViewModel
            {
                Address = this.addressesService.GetAddress(order.DeliveryAddressId),
                DeliveryPrice = order.DeliveryPrice,
                DeliveryType = order.DeliveryType,
                OrderId = order.Id,
                PaymentType = order.PaymentType,
                Phone = user.PhoneNumber,
                FullName = user.FirstName + " " + user.LastName,
                CartMovies = this.ordersService.GetOrderMovies(order.Id).Select(x => new CartMovieViewModel
                {
                    Id = x.MovieId,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Name = this.moviesService.GetMovie(x.MovieId).Name,
                    PosterUrl = this.moviesService.GetMovie(x.MovieId).PosterUrl
                }).ToList()
            };

            return this.View(model);
        }

        [HttpGet("/Orders/Pay")]
        public IActionResult Pay()
        {
            return this.View();
        }

        [HttpPost("/Orders/Pay")]
        public IActionResult Pay(int orderId)
        {
            this.ordersService.PayAnOrder(this.User.Identity.Name, orderId);

            return this.Redirect("/Orders/Summary");
        }
    }
}