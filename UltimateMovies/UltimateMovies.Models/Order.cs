using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UltimateMovies.Models.Enums;

namespace UltimateMovies.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ProcesedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        [Required]
        public double DeliveryPrice { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public DeliveryType DeliveryType { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        public string UserId { get; set; }

        public virtual UMUser User { get; set; }

        public int DeliveryAddressId { get; set; }

        public virtual Address DeliveryAddress { get; set; }

        public virtual ICollection<OrderMovie> Movies { get; set; }
    }
}
