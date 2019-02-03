using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarAdverts.Core.Entities;
using CarAdverts.Core.Exceptions;
using CarAdverts.Core.Interfaces;
using CarAdverts.Core.Services;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using Xunit;

namespace UnitTests.ApplicationCore.Services.AdvertService
{
    public class AdvertServiceTests
    {
        private readonly Mock<IAdvertRepository> _mockRepository;
        private readonly Mock<IAppLogger<IAdvertService>> _mockLogger;
        private readonly CarAdverts.Core.Services.AdvertService advertService;
        public AdvertServiceTests()
        {
            #region MockDataDefinition
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


            #endregion

            _mockRepository = new Mock<IAdvertRepository>();
            _mockLogger = new Mock<IAppLogger<IAdvertService>>();

            #region MockSetups
            _mockRepository.Setup(t => t.GetAll(null)).Returns(adverts);
            _mockRepository.Setup(t => t.FindById(It.IsAny<int>()))
                .Returns((int i) => adverts.Single(t => t.Id == i));
            _mockRepository.Setup(t => t.Add(It.IsAny<Advert>())).Returns((Advert a) =>
            {
                a.Id = adverts.Count + 1;
                adverts.Add(a);
                return a;
            });

            _mockRepository.Setup(t => t.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                Advert a = adverts.FirstOrDefault(t => t.Id == id);
                if (a == null)
                {
                    throw new NotFoundException(typeof(Advert).Name, "id");
                }
                else
                {
                    adverts.Remove(a);
                }
            });

            _mockRepository.Setup(t => t.Update(It.IsAny<Advert>())).Callback((Advert ad) =>
            {
                Advert a = adverts.FirstOrDefault(t => t.Id == ad.Id);
                if (a == null)
                {
                    throw new NotFoundException(typeof(Advert).Name, "id");
                }
                else
                {
                    a.FuelType = ad.FuelType;
                    a.FirstRegistration = ad.FirstRegistration;
                    a.Fuel = ad.Fuel;
                    a.IsNew = ad.IsNew;
                    a.Price = ad.Price;
                    a.Title = ad.Title;
                    a.Mileage = ad.Mileage;
                }
            });
            #endregion


            advertService = new CarAdverts.Core.Services.AdvertService(_mockRepository.Object, _mockLogger.Object);
        }


        [Fact]
        public void ShouldFindById()
        {
            var adv = advertService.FindById(1);
            Assert.Equal(1, adv.Id);
        }

        [Fact]
        public void ShouldAddNewAdvert()
        {
            Advert a = new Advert()
            {
                FuelType = "Gasoline",
                FirstRegistration = new DateTime(2012,10,02),
                Title = "Golf Avant",
                IsNew = false,
                Mileage = 25000,
                Price = 12000
            };

            var adv = advertService.Add(a);
            _mockRepository.Verify(x=> x.Add(It.IsAny<Advert>()), Times.Once);
            Assert.True(adv.Id == 6);

        }
        [Fact]
        public void ShouldNotUpdateAdvertWithWrongId()
        {
            Advert a = new Advert()
            {
                Id= 12,
                FuelType = "Gasoline",
                FirstRegistration = new DateTime(2012, 10, 02),
                Title = "Golf Avant",
                IsNew = false,
                Mileage = 25000,
                Price = 12000
            };
             Assert.Throws<NotFoundException>(()=> advertService.Update(a));
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
            advertService.Update(a);
            Assert.Equal(advertService.FindById(5).Title, a.Title );
            Assert.Equal(advertService.FindById(5).FuelType, a.FuelType);
            Assert.Equal(advertService.FindById(5).Mileage, a.Mileage);
            Assert.Equal(advertService.FindById(5).Price, a.Price);
            Assert.Equal(advertService.FindById(5).IsNew, a.IsNew);
            Assert.Equal(advertService.FindById(5).FirstRegistration, a.FirstRegistration);
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
            advertService.Add(a);
            advertService.Delete(6);

            Assert.Throws<ApplicationException>(() => advertService.FindById(6));
        }

        [Fact]
        public void ShouldThrowExceptionDeleteWhenAdvertDoesNotExists()
        {
            
            Assert.Throws<NotFoundException>(() => advertService.Delete(6));
        }




    }
}
