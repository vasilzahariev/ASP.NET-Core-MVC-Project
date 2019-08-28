using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.ViewModels.Orders
{
    public class UserOrderViewModel
    {
        public int OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentType PaymentType { get; set; }

        public double TotalPrice { get; set; }
    }
}
