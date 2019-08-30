using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.Areas.Admin.ViewModels
{
    public class ProcessedOrder
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime ProcesedDate { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
