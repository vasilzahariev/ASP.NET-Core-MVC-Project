using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Orders
{
    public class UserViewModel
    {
        [Display(Name = "Recipient Name")]
        [Required]
        [Phone]
        public string FullName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
