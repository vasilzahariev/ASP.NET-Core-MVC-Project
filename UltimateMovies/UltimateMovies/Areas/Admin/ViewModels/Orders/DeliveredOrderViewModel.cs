using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.Areas.Admin.ViewModels
{
    public class DeliveredOrderViewModel
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime DeliveredDate { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
