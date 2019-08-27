using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimateMovies.Models;
using UltimateMovies.Services;
using UltimateMovies.ViewModels.Addresses;

namespace UltimateMovies.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly IAddressesService addressesService;

        public AddressesController(IAddressesService addressesService)
        {
            this.addressesService = addressesService;
        }

        [HttpGet("/Addresses/")]
        public IActionResult Index()
        {
            AddressListingModel model = new AddressListingModel
            {
                Addresses = this.addressesService.GetAllUserAddresses(this.User.Identity.Name)
                                        .Select(a => new AddressModelView
                                        {
                                            Id = a.Id,
                                            Country = a.Country,
                                            City = a.City,
                                            Postcode = a.Postcode,
                                            AdditionalInforamtion = a.AdditionalInformation,
                                            Street = a.Street
                                        }).ToList()
            };

            return View(model);
        }

        [HttpGet("/Addresses/Create")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("/Addresses/Create")]
        public IActionResult Create(AddressInputModel model)
        {
            this.addressesService.CreateANewAddress(model.Country, model.City, model.Street, model.AdditionalInformation, model.Postcode, this.User.Identity.Name);

            return this.Redirect("/Addresses/");
        }

        [HttpGet("/Addresses/Remove/{addressId}")]
        public IActionResult Remove(int addressId)
        {
            this.addressesService.RemoveAddress(addressId);

            return this.Redirect("/Addresses/");
        }

        [HttpGet("/Addresses/Edit/{addressId}")]
        public IActionResult Edit(int addressId)
        {
            return this.View();
        }
    }
}