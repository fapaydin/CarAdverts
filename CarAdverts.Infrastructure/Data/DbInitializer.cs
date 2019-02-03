using System;
using System.Collections.Generic;
using System.Text;
using CarAdverts.Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace CarAdverts.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void SeedDb(AdvertContext ctx)
        {
            //ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            if(!ctx.Adverts.Any())
                SeedAdverts(ctx);
        }

        private static void SeedAdverts(AdvertContext ctx)
        {

            var adverts = new[]
            {
                new Advert
                {
                    Title = "Audi A4 Avant", Fuel = FuelType.Diesel, Price = 15000, IsNew = false, Mileage = 35000,
                    FirstRegistration = new DateTime(2015, 10, 1)
                },
                new Advert
                {
                    Title = "Audi A3", Fuel = FuelType.Diesel, Price = 5000, IsNew = false, Mileage = 135000,
                    FirstRegistration = new DateTime(2015, 10, 1)
                },
                new Advert
                {
                    Title = "Audi A2", Fuel = FuelType.Gasoline, Price = 3000, IsNew = false, Mileage = 235000,
                    FirstRegistration = new DateTime(2013, 10, 1)
                },
                new Advert
                {
                    Title = "Audi A5", Fuel = FuelType.Diesel, Price = 25000, IsNew = false, Mileage = 5000,
                    FirstRegistration = new DateTime(2012, 10, 1)
                },
                new Advert
                {
                    Title = "Volkswagen Golf", Fuel = FuelType.Diesel, Price = 15000, IsNew = false, Mileage = 1000,
                    FirstRegistration = new DateTime(2011, 10, 1)
                },

                new Advert {Title = "Toyota Camry 1.6", Fuel = FuelType.Gasoline, Price = 20000, IsNew = true}
            };
            ctx.Adverts.AddRange(adverts);
            ctx.SaveChanges();
        }
    }
}
