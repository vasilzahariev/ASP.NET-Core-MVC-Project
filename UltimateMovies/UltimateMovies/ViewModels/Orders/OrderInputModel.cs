using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.ViewModels.Orders
{
    public class OrderInputModel
    {
        public string ResipientName { get; set; }

        public int AddressId { get; set; }

        public string PhoneNumber { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public PaymentType PaymentType { get; set; }

        public double CartPrice { get; set; }
    }
}
