using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.Areas.Admin.ViewModels
{
    public class DeliveredOrdersList
    {
        public IEnumerable<DeliveredOrderViewModel> DeliveredOrders { get; set; }
    }
}
