using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.ViewModels.Orders
{
    public class OrderInputModel
    {
        [Required]
        public string ResipientName { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public DeliveryType DeliveryType { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        [Required]
        public double CartPrice { get; set; }
    }
}
