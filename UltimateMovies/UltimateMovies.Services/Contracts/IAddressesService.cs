using System;
using System.Collections.Generic;
using System.Text;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public interface IAddressesService
    {
        void CreateANewAddress(string country, string city, string street, string additionalInformation, int postCode, string username);

        List<Address> GetAllUserAddresses(string username);

        void RemoveAddress(int addressId);
    }
}
