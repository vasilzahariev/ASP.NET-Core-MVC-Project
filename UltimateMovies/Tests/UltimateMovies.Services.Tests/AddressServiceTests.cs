using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;
using Xunit;

namespace UltimateMovies.Services.Tests
{
    public class AddressServiceTests
    {
        [Fact]
        public void CreateANewAddressShouldCreateANewAddress()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Addresses_CreateANewAddress_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IAddressesService addressesService = new AddressService(db);

            db.Users.Add(new UMUser
            {
                UserName = "TestUser"
            });

            db.SaveChanges();

            addressesService.CreateANewAddress("Country", "City", "Street", "Additional Information", 1111, "TestUser");

            int addressesCount = db.Addresses.ToList().Count();

            Assert.Equal(1, addressesCount);
        }

        [Fact]
        public void EditAddressShouldChangeAddressValues()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Addresses_EditAddress_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IAddressesService addressesService = new AddressService(db);

            db.Addresses.Add(new Address
            {
                Country = "Country",
                City = "City",
                Street = "Street",
                AdditionalInformation = "Additional Information",
                Postcode = 1111
            });

            db.SaveChanges();

            addressesService.EditAddress("co", "ci", "st", "ai", 11, db.Addresses.Last().Id);

            Assert.Equal("co", db.Addresses.Last().Country);
            Assert.Equal("ci", db.Addresses.Last().City);
            Assert.Equal("st", db.Addresses.Last().Street);
            Assert.Equal("ai", db.Addresses.Last().AdditionalInformation);
            Assert.Equal(11, db.Addresses.Last().Postcode);
        }

        [Fact]
        public void GetAddressShoudlReturnAddress()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Addresses_GetAddress_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IAddressesService addressesService = new AddressService(db);

            db.Addresses.Add(new Address
            {
                Country = "Country"
            });

            db.SaveChanges();

            Address address = addressesService.GetAddress(db.Addresses.Last().Id);

            Assert.Equal("Country", address.Country);
        }

        [Fact]
        public void GetAllUserAddressesShouldReturnACollectionOfAddresses()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Addresses_GetAllUserAddresses_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IAddressesService addressesService = new AddressService(db);

            db.Users.Add(new UMUser
            {
                UserName = "Test"
            });

            db.SaveChanges();

            db.Addresses.Add(new Address
            {
                Country = "Bulgaria",
                UserId = db.Users.FirstOrDefault(u => u.UserName == "Test").Id
            });

            db.Addresses.Add(new Address
            {
                Country = "Bulgaria",
                UserId = db.Users.FirstOrDefault(u => u.UserName == "Test").Id
            });

            db.SaveChanges();

            List<Address> addresses = addressesService.GetAllUserAddresses("Test").ToList();

            int addressesCount = addresses.Count();

            Assert.Equal(2, addressesCount);
        }

        [Fact]
        public void RemoveAddressShouldRemoveAnAddressFromTheDatabase()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Addresses_RemoveAddress_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IAddressesService addressesService = new AddressService(db);

            db.Addresses.Add(new Address { Country = "Test 1" });
            db.Addresses.Add(new Address { Country = "Test 2" });

            db.SaveChanges();

            addressesService.RemoveAddress(db.Addresses.Last().Id);

            int addressesCount = db.Addresses.ToList().Count();
            string leftAddressCountry = db.Addresses.ToList()[0].Country;

            Assert.Equal(1, addressesCount);
            Assert.Equal("Test 1", leftAddressCountry);
        }
    }
}
