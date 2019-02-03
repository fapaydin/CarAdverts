using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FluentValidation;

namespace CarAdverts.Core.Entities
{
    public class AdvertValidation : AbstractValidator<Advert>
    {
        public AdvertValidation()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().Length(0, 256);
            RuleFor(x => x.Fuel).NotNull().IsInEnum<Advert,FuelType>();
            RuleFor(x => x.IsNew).NotNull();
            RuleFor(x => x.Price).NotNull();
            RuleFor(x => x.Mileage).NotNull().When(t => t.IsNew == false);
            RuleFor(t => t.FirstRegistration).NotNull().When(t => t.IsNew == false);
            RuleFor(x => x.Mileage).Null().When(t => t.IsNew == true).WithMessage("Mileage should be 0  or null when for a new car.");
            RuleFor(x => x.FirstRegistration).Null().When(t => t.IsNew)
                .WithMessage("FirstRegistration should be null for a new car.");

            RuleFor(x => x.FuelType).Custom(((s, context) =>
            {
                var fuelTypes = Enum.GetNames(typeof(FuelType));
                if (!fuelTypes.Any(s.Contains))
                    context.AddFailure("Not valid FuelType");
            }));
        }
    }
}
