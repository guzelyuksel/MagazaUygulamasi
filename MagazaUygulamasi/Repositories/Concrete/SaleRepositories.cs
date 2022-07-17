using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace MagazaUygulamasi.Repositories.Concrete
{
    public class SaleRepositories : BaseSaleRepositories
    {
        public override void Add(Sale newSale)
        {
            if (newSale != null)
                SeedData.Sales.Add(newSale);
        }

        public override void Delete(int id)
        {
            if (SeedData.Sales.Any(x => x.Id == id))
                SeedData.Sales.Remove(SeedData.Sales.First(x => x.Id == id));
        }

        public override void Update(int id, Sale sale)
        {
            if (SeedData.Sales.All(x => x.Id != sale.Id))
                return;
            SeedData.Sales.Remove(SeedData.Sales.First(x => x.Id == sale.Id));
            SeedData.Sales.Add(sale);
        }

        public override Sale GetById(int id) => SeedData.Sales.FirstOrDefault(x => x.Id == id);

        public override List<Sale> GetAll() => SeedData.Sales;

        public override List<Sale> GetByCustomerId(int customerId) => SeedData.Sales.FindAll(x => x.CustomerId == customerId);
    }
}