using System;
using System.Collections.Generic;
using System.Text;
using CarAdverts.Core.Entities;

namespace CarAdverts.Core.Interfaces
{
    public interface IAdvertService
    {
        Advert Add(Advert advert);

        Advert FindById(int id);

        void Update(Advert advert);

        void Delete(int id);

        ICollection<Advert> GetAll(string orderBy);
    }
}
