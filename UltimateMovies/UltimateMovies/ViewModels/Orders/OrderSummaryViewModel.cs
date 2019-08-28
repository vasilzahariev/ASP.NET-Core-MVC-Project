using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.ViewModels.Orders
{
    public class OrderSummaryViewModel
    {
        public string FullName { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }

        public PaymentType PaymentType { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public double DeliveryPrice { get; set; }

        public int OrderId { get; set; }

        public ICollection<CartMovieViewModel> CartMovies { get; set; }

        public double OverallCartPrice()
        {
            return this.CartMovies.Sum(c => c.OverallPrice());
        }
    }
}
