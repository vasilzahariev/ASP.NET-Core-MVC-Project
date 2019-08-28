using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.ViewModels.Orders
{
    public class UserOrdersListingModel
    {
        public ICollection<UserOrderViewModel> Orders { get; set; }
    }
}
