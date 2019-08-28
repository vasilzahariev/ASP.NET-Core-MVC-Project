using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UltimateMovies.Models.Enums
{
    public enum OrderStatus
    {
        Unfinished = 1,
        Unprocessed = 2,
        Procesed = 3,
        Delivered = 4
    }
}
