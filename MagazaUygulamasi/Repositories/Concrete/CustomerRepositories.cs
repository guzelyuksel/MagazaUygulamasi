using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace MagazaUygulamasi.Repositories.Concrete
{
    public class CustomerRepositories : BaseCustomerRepositories
    {
        public override void Add(Customer customer)
        {
            if (customer != null)
                SeedData.Customers.Add(customer);
        }

        public override void Update(Customer customer)
        {
            if (SeedData.Customers.All(x => x.Id != customer.Id))
                return;
            SeedData.Customers.Remove(SeedData.Customers.First(x => x.Id == customer.Id));
            SeedData.Customers.Add(customer);
        }

        public override void Delete(int id)
        {
            if (SeedData.Customers.Any(x => x.Id == id))
                SeedData.Customers.Remove(SeedData.Customers.First(x => x.Id == id));
        }

        public override Customer GetById(int id) => SeedData.Customers.FirstOrDefault(x => x.Id == id);

        public override List<Customer> GetAll() => SeedData.Customers;
    }
}