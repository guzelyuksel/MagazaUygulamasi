using System.Collections.Generic;
using MagazaUygulamasi.Entities.Concrete;

namespace MagazaUygulamasi.Repositories.Abstract
{
    public abstract class BaseSaleRepositories
    {
        public abstract void Add(Sale newSale);
        public abstract void Delete(int id);
        public abstract void Update(int id, Sale sale);
        public abstract Sale GetById(int id);
        public abstract List<Sale> GetAll();
        public abstract List<Sale> GetByCustomerId(int customerId);
    }
}