using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CarAdverts.Core.Entities;
using CarAdverts.Core.Exceptions;
using CarAdverts.Core.Interfaces;
using CarAdverts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAdverts.Infrastructure.Repositories
{
    public class AdvertRepository: IAdvertRepository
    {
        private readonly AdvertContext _context;
        public AdvertRepository(AdvertContext advertContext)
        {
            _context = advertContext;
        }
        public Advert Add(Advert advert)
        {
            _context.Adverts.Add(advert);
            _context.SaveChanges();
            return advert;
        }

        public Advert FindById(int id)
        {
            return _context.Adverts.Find(id);
        }

        public void Update(Advert advert)
        {

          
            try
            {
                var ad = _context.Adverts.FirstOrDefault(t => t.Id == advert.Id);
                if (ad == null)
                {
                    throw new NotFoundException(typeof(Advert).Name, "id");
                }
                else
                {
                    ad.FuelType = advert.FuelType;
                    ad.FirstRegistration = advert.FirstRegistration;
                    ad.Fuel = advert.Fuel;
                    ad.IsNew = advert.IsNew;
                    ad.Mileage = advert.Mileage;
                    ad.Price = advert.Price;
                    ad.Title = advert.Title;
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        public void Delete(int id)
        {
            Advert advert = FindById(id);
            if (advert != null)
            {
                _context.Adverts.Remove(advert);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(typeof(Advert).Name, "id");
            }
            
        }
        

        public ICollection<Advert> GetAll(string orderBy = null)
        {
            var query = _context.Adverts.AsQueryable();
            
            if (orderBy != null)
            {
                //order by lambda generation
                var orderByField = (dynamic)CreateExpression(typeof(Advert), orderBy);
                query = Queryable.OrderBy(query, orderByField);
            }
            else
            {
               query = query.OrderBy(t => t.Id);
            }
            return query.ToList();
        }

        

        private LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "type");

            Expression body = param;
            try
            {
                foreach (var member in propertyName.Split('.'))
                {
                    body = Expression.PropertyOrField(body, member);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Field not found",ex);
            }
            return Expression.Lambda(body, param);
        }
    }
}
