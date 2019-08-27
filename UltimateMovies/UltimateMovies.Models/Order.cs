using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }



        public string UserId { get; set; }

        public virtual UMUser User { get; set; }
    }
}
