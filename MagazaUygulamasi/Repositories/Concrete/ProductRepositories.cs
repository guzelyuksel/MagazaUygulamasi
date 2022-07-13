using System.Collections.Generic;
using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Repositories.Abstract;

namespace MagazaUygulamasi.Repositories.Concrete
{
    public class ProductRepositories : BaseProductRepositories
    {
        public override void Add(Product newProduct)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(int id, Product employe)
        {
            throw new System.NotImplementedException();
        }

        public override Product GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public override List<Product> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public override List<Product> GetByCategoryId(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public override bool Sell(int id, int quantity)
        {
            throw new System.NotImplementedException();
        }
    }
}