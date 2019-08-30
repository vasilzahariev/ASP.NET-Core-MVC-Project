using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateMovies.Areas.Admin.ViewModels
{
    public class OrdersList
    {
        public IEnumerable<UnprocessedOrder> UnprocessedOrders { get; set; }

        public IEnumerable<ProcessedOrder> ProcessedOrders { get; set; }
    }
}
