using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.ViewModels.Orders
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public Address Address { get; set; }

        public double DeliveryPrice { get; set; }

        public double CartPrice { get; set; }

        public double TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentType PaymentType { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ProcesedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        public ICollection<CartMovieViewModel> CartMovies { get; set; }
    }
}
