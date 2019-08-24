using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Users
{
    public class WishListListingModel
    {
        public ICollection<WishListModelView> WishList { get; set; }
    }
}
