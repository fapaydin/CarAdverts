using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;
using CarAdverts.Core.SharedKernel;
using Newtonsoft.Json;

namespace CarAdverts.Core.Entities
{
    public class Advert : BaseEntity
    {
        public string Title { get; set; }
        [JsonIgnore]
        public FuelType Fuel { get; set; }
        public int Price { get; set; }
        public bool IsNew { get; set; }
        public int? Mileage { get; set; }

        private DateTime? _firstRegistration;
        public DateTime? FirstRegistration
        {
            get { return _firstRegistration; }
            set {
                if (value.HasValue)
                {
                    _firstRegistration = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day);
                }
                else
                {
                    _firstRegistration = null;
                }

            }

        }

        [NotMapped]
        public string FuelType
        {
            get { return Enum.GetName(typeof(FuelType), this.Fuel); }
            set
            {
                FuelType result;
                if (Enum.TryParse<FuelType>(value, true, out result))
                {   
                    if(Enum.IsDefined(typeof(FuelType), result))
                        this.Fuel = result;
                }
                else
                {
                    throw new ArgumentException($"FuelType is not defined: ${value}", "Fuel");
                }
            }
        }
    }
}
