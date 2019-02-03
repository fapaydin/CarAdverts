using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CarAdverts.Core.Entities;
using CarAdverts.Core.Interfaces;
using System.Reflection;
using System.Runtime.CompilerServices;
using CarAdverts.Core.Exceptions;

namespace CarAdverts.Core.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IAppLogger<IAdvertService> _logger;
        public AdvertService(IAdvertRepository advertRepository, IAppLogger<IAdvertService> appLogger)
        {
            _advertRepository = advertRepository;
            _logger = appLogger;
        }

        public Advert Add(Advert advert)
        {
            try
            {
                var ad = _advertRepository.Add(advert);
                return ad;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception at AdvertService.Add method", advert);
                throw new ApplicationException("An error occured when adding a new advert.", e);
            }
        }

        public Advert FindById(int id)
        {
            try
            {
                var ad = _advertRepository.FindById(id);
                if (ad == null) throw new NotFoundException("Advert", id);

                return ad;
            }
            catch (NotFoundException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                _logger.LogError("Exception at AdvertService.FindById method", id);
                throw new ApplicationException("An error occured when finding the advert.", e);
            }
        }

        public void Update(Advert advert)
        {
            try
            {
                _advertRepository.Update(advert);
            }
            catch (NotFoundException e)
            {
                _logger.LogError("NotFoundException at AdvertService.Update method", advert);
                throw e;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception at AdvertService.Update method", advert);
                throw new ApplicationException("An error occured when updating the advert.",e);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _advertRepository.Delete(id);
            }
            catch (NotFoundException e)
            {
                _logger.LogError("NotFoundException at AdvertService.Delete method", id);
                throw e;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception at AdvertService.Delete method", id);
                throw new ApplicationException("An error occured when deleting the advert.",e);
            }
           
        }

        public ICollection<Advert> GetAll(string orderBy)
        {
            try
            {
                return _advertRepository.GetAll(orderBy);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception at AdvertService.GetAll method", orderBy);
                throw new ApplicationException("An error occured when getting all adverts.",e);
            }
        }

    }
}
