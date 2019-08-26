using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models
{
    public class UMUser : IdentityUser
    {
        public UMUser()
        {
            this.Addresses = new HashSet<Address>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        //public int LibraryId { get; set; }

        //public virtual Library Library { get; set; }

        public virtual ICollection<CartMovie> Cart { get; set; }

        public virtual ICollection<WishListMovie> WishList { get; set; }
    }
}
