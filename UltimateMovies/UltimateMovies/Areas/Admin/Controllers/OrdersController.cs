using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Areas.Admin.ViewModels;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;
using UltimateMovies.Services;

namespace UltimateMovies.Areas.Admin.Controllers
{
    public class OrdersController : AdminController
    {
        private readonly IOrdersService ordersService;
        private readonly IAddressesService addressesService;
        private readonly IMoviesService moviesService;
        private readonly IUsersService usersService;

        public OrdersController(IOrdersService ordersService, IAddressesService addressesService, IMoviesService moviesService, IUsersService usersService)
        {
            this.ordersService = ordersService;
            this.addressesService = addressesService;
            this.moviesService = moviesService;
            this.usersService = usersService;
        }

        [HttpGet("/Admin/Orders/")]
        public IActionResult Index()
        {
            OrdersList model = new OrdersList
            {
                UnprocessedOrders = this.ordersService.GetAllOrders()
                                                        .FindAll(o => o.OrderStatus == OrderStatus.Unprocessed)
                                                        .Select(o => new UnprocessedOrder
                                                        {
                                                            Id = o.Id,
                                                            DeliveryType = o.DeliveryType,
                                                            OrderDate = (DateTime)o.OrderDate,
                                                            OrderStatus = o.OrderStatus,
                                                            PaymentType = o.PaymentType
                                                        }).ToList(),
                ProcessedOrders = this.ordersService.GetAllOrders()
                                                        .FindAll(o => o.OrderStatus == OrderStatus.Procesed)
                                                        .Select(o => new ProcessedOrder
                                                        {
                                                            Id = o.Id,
                                                            DeliveryType = o.DeliveryType,
                                                            ProcesedDate = (DateTime)o.ProcesedDate,
                                                            OrderStatus = o.OrderStatus,
                                                            PaymentType = o.PaymentType
                                                        }).ToList()
            };

            return View(model);
        }

        [HttpGet("/Admin/Orders/Delivered")]
        public IActionResult Delivered()
        {
            DeliveredOrdersList model = new DeliveredOrdersList
            {
                DeliveredOrders = this.ordersService.GetAllOrders()
                                                        .FindAll(o => o.OrderStatus == OrderStatus.Delivered)
                                                        .Select(o => new DeliveredOrderViewModel
                                                        {
                                                            Id = o.Id,
                                                            DeliveredDate = (DateTime)o.DeliveredDate,
                                                            DeliveryType = o.DeliveryType,
                                                            OrderStatus = o.OrderStatus,
                                                            PaymentType = o.PaymentType
                                                        }).OrderByDescending(o => o.DeliveredDate).ToList()
            };

            return this.View(model);
        }

        [HttpGet("/Admin/Orders/Details/{orderId}")]
        public IActionResult Details(int orderId)
        {
            Order order = this.ordersService.GetOrder(orderId);
            UMUser user = this.usersService.GetUserById(order.UserId);

            OrderDetailsViewModel model = new OrderDetailsViewModel
            {
                Id = order.Id,
                Address = this.addressesService.GetAddress(order.DeliveryAddressId),
                CartMovies = this.ordersService.GetOrderMovies(order.Id).Select(x => new CartMovieViewModel
                {
                    Id = x.MovieId,
                    Name = this.moviesService.GetMovie(x.MovieId).Name,
                    PosterUrl = this.moviesService.GetMovie(x.MovieId).PosterUrl,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList(),
                CartPrice = order.TotalPrice - order.DeliveryPrice,
                DeliveredDate = order.DeliveredDate,
                DeliveryPrice = order.DeliveryPrice,
                DeliveryType = order.DeliveryType,
                OrderDate = (DateTime)order.OrderDate,
                OrderStatus = order.OrderStatus,
                PaymentType = order.PaymentType,
                ProcesedDate = order.ProcesedDate,
                RecipientName = user.FirstName + " " + user.LastName,
                RecipientPhoneNumber = user.PhoneNumber,
                TotalPrice = order.TotalPrice
            };

            return this.View(model);
        }


        [HttpGet("/Admin/Orders/ProcessOrder/{id}")]
        public IActionResult ProcessOrder(int id)
        {
            this.ordersService.ProcessOrder(id);

            return this.Redirect("/Admin/Orders/");
        }

        [HttpGet("/Admin/Orders/DeliverOrder/{id}")]
        public IActionResult DeliverOrder(int id)
        {
            this.ordersService.DeliverOrder(id);

            return this.Redirect("/Admin/Orders/");
        }
    }
}