using System;
using System.Collections.Generic;
using System.Text;
using CarAdverts.Core.Entities;
using FluentValidation.TestHelper;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.AdvertTests
{
    public class AdvertEntityTests
    {
        private readonly AdvertValidation validator;

        public AdvertEntityTests()
        {
            validator = new AdvertValidation();
        }
        [Fact]
        public void FuelEnumToStringTypeEquals()
        {
            Advert advert = new Advert();
            advert.Fuel = FuelType.Diesel;
            Assert.Equal("Diesel",advert.FuelType);
        }

        [Fact]
        public void FuelTypeStringToEnumTypeEquals()
        {
            Advert advert = new Advert();
            advert.FuelType = "Gasoline";
            Assert.Equal(FuelType.Gasoline, advert.Fuel);
        }

        [Fact]
        public void ShouldHaveErrorWhenTitleIsEmptyOrNull()
        {
            validator.ShouldHaveValidationErrorFor(t => t.Title, null as string);
            validator.ShouldHaveValidationErrorFor(t => t.Title, string.Empty);
        }

        [Fact]
        public void ShouldHaveErrorWhenTitlesLengthBiggerThan256()
        {
            validator.ShouldHaveValidationErrorFor(t => t.Title, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu nunc quis est vulputate euismod. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam mauris sem, egestas id lectus pharetra, vulputate consequat turpis. Donec tincidunt tincidunt congue. Donec nisl risus, lacinia ut tincidunt a, sagittis tempus nibh. In lectus magna, ornare vitae luctus vitae, mattis a eros. Integer vel ornare elit, vitae molestie eros. Praesent at maximus lorem, ac scelerisque mi. Morbi vulputate lacus a accumsan commodo. Nulla facilisi. Vivamus in lorem luctus, tempor tellus a, dignissim purus. Phasellus facilisis ultricies ligula, ac tempor urna mollis facilisis. Etiam id nisi.");
        }
        
        
        [Fact]
        public void ShouldNotHaveErrorWhenMileageIsNullWhenIsNewTrue()
        {
            validator.ShouldNotHaveValidationErrorFor(x=> x.Mileage, new Advert() {IsNew = true, Mileage = null});
        }

        [Fact]
        public void ShouldHaveErrorWhenMileageIsNullWhenIsNewFalse()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Mileage, new Advert() { IsNew = false, Mileage = null });
        }

        [Fact]
        public void ShouldNotHaveErrorWhenFirstRegistrationIsNullWhenIsNewTrue()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.FirstRegistration, new Advert() { IsNew = true, FirstRegistration = null });
        }

        [Fact]
        public void ShouldHaveErrorWhenFirstRegistrationIsNullWhenIsNewFalse()
        {
            validator.ShouldHaveValidationErrorFor(x => x.FirstRegistration, new Advert() { IsNew = false, FirstRegistration = null });
        }

    }
}
