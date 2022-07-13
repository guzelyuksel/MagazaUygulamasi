using MagazaUygulamasi.Entities.Concrete;
using System.Collections.Generic;

namespace MagazaUygulamasi.Repositories.Abstract
{
    public abstract class BaseProductRepositories
    {
        public abstract void Add(Product newProduct);
        public abstract void Delete(int id);
        public abstract void Update(int id, Product employe);
        public abstract Product GetById(int id);
        public abstract List<Product> GetAll();
        public abstract List<Product> GetByCategoryId(int categoryId);
        public abstract bool Sell(int id, int quantity);
    }
}