using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace MagazaUygulamasi.Repositories.Concrete
{
    public class ProductRepositories : BaseProductRepositories
    {
        public override void Add(Product newProduct)
        {
            if (newProduct != null)
                SeedData.Products.Add(newProduct);
        }

        public override void Delete(int id)
        {
            if (SeedData.Products.Any(x => x.Id == id))
                SeedData.Products.Remove(SeedData.Products.First(x => x.Id == id));
        }

        public override void Update(int id, Product product)
        {
            if (SeedData.Products.All(x => x.Id != product.Id))
                return;
            SeedData.Products.Remove(SeedData.Products.First(x => x.Id == product.Id));
            SeedData.Products.Add(product);
        }

        public override Product GetById(int id) => SeedData.Products.FirstOrDefault(x => x.Id == id);

        public override List<Product> GetAll() => SeedData.Products;

        public override List<Product> GetByCategoryId(int categoryId) => SeedData.Products.FindAll(x => (int)x.CategoryId == categoryId);

        public override bool Sell(int id, int quantity)
        {
            var product = SeedData.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return false;
            if (product.UnitsInStock < quantity)
                return false;
            product.UnitsInStock -= quantity;
            return true;
        }
    }
}