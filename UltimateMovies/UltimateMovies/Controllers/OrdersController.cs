using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UltimateMovies.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        [HttpGet("/Orders/Create")]
        public IActionResult Create()
        {
            return View();
        }
    }
}