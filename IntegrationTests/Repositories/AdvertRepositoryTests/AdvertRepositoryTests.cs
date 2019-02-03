using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarAdverts.Core.Entities;
using CarAdverts.Core.Exceptions;
using CarAdverts.Infrastructure.Data;
using CarAdverts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests.Repositories.AdvertRepositoryTests
{
    public class AdvertRepositoryTests
    {
        private readonly AdvertRepository _advertRepository;
        private readonly AdvertContext _context;
        public AdvertRepositoryTests()
        {
            var dbObtions = new DbContextOptionsBuilder<AdvertContext>()
                .UseInMemoryDatabase(databaseName: "AdvertTest")
                .Options;
            _context = new AdvertContext(dbObtions);


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            #region SeedData
            //Some mock adverts;
            var adverts = new[]
            {
                new Advert
                {
                    Id=1, Title = "Audi A4 Avant", Fuel = FuelType.Diesel, Price = 15000, IsNew = false, Mileage = 35000,
                    FirstRegistration = new DateTime(2015, 10, 1)
                },
                new Advert
                {
                    Id=2, Title = "Audi A3", Fuel = FuelType.Diesel, Price = 5000, IsNew = false, Mileage = 135000,
                    FirstRegistration = new DateTime(2015, 10, 1)
                },
                new Advert
                {
                    Id=3, Title = "Audi A2", Fuel = FuelType.Gasoline, Price = 3000, IsNew = false, Mileage = 235000,
                    FirstRegistration = new DateTime(2013, 10, 1)
                },
                new Advert
                {
                    Id=4, Title = "Audi A5", Fuel = FuelType.Diesel, Price = 25000, IsNew = false, Mileage = 5000,
                    FirstRegistration = new DateTime(2012, 10, 1)
                },
                new Advert
                {
                    Id=5, Title = "Volkswagen Golf", Fuel = FuelType.Diesel, Price = 15000, IsNew = false, Mileage = 1000,
                    FirstRegistration = new DateTime(2011, 10, 1)
                }
            }.ToList();

            _context.Adverts.AddRange(adverts);
            _context.SaveChanges();
            #endregion
            _advertRepository = new AdvertRepository(_context);
        }

        [Fact]
        public void ShouldFindById()
        {
            var adv = _advertRepository.FindById(1);
            Assert.Equal(1, adv.Id);

        }

        [Fact]
        public void ShouldNotFindByIdNotExistingAdvert()
        {
            var adv = _advertRepository.FindById(10);
            Assert.Null(adv);
        }

        [Fact]
        public void ShouldAddNewAdvert()
        {
            Advert a = new Advert()
            {
                Id = 20,
                FuelType = "Gasoline",
                FirstRegistration = new DateTime(2012, 10, 02),
                Title = "Golf Avant",
                IsNew = false,
                Mileage = 25000,
                Price = 12000
            };

            var adv = _advertRepository.Add(a);
            Assert.True(adv.Title == a.Title);
        }
        [Fact]
        public void ShouldNotUpdateAdvertWithWrongId()
        {
            Advert a = new Advert()
            {
                Id = 12,
                FuelType = "Gasoline",
                FirstRegistration = new DateTime(2012, 10, 02),
                Title = "Golf Avant",
                IsNew = false,
                Mileage = 25000,
                Price = 12000
            };
            Assert.Throws<NotFoundException>(() => _advertRepository.Update(a));
        }

        [Fact]
        public void ShouldUpdateAdvertCorrectly()
        {
            Advert a = new Advert()
            {
                Id = 5,
                FuelType = "Gasoline",
                FirstRegistration = new DateTime(2012, 10, 02),
                Title = "Golf Avant",
                IsNew = false,
                Mileage = 25000,
                Price = 12000
            };
            _advertRepository.Update(a);
            Assert.Equal(a.Title,_advertRepository.FindById(5).Title);
            Assert.Equal(a.FuelType,_advertRepository.FindById(5).FuelType);
            Assert.Equal(a.Mileage,_advertRepository.FindById(5).Mileage);
            Assert.Equal(a.Price ,_advertRepository.FindById(5).Price);
            Assert.Equal(a.IsNew ,_advertRepository.FindById(5).IsNew);
            Assert.Equal(a.FirstRegistration,_advertRepository.FindById(5).FirstRegistration);
        }

        [Fact]
        public void ShouldDeleteAdvert()
        {
            Advert a = new Advert()
            {
                Id = 6,
                FuelType = "Gasoline",
                FirstRegistration = new DateTime(2012, 10, 02),
                Title = "Golf Avant",
                IsNew = false,
                Mileage = 25000,
                Price = 12000
            };
            _advertRepository.Add(a);
            _advertRepository.Delete(6);

            Assert.Null(_advertRepository.FindById(6));
        }

        [Fact]
        public void ShouldThrowExceptionDeleteWhenAdvertDoesNotExists()
        {

            Assert.Throws<NotFoundException>(() => _advertRepository.Delete(6));
        }

    }
}
