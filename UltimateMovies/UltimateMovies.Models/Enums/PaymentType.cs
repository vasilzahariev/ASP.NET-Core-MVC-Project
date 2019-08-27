using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models.Enums
{
    public enum PaymentType
    {
        Online = 1,
        [Display(Name = "Cash On Delivery")]
        CashOnDelivery = 2
    }
}
