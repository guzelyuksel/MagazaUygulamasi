using System.Collections.Generic;
using MagazaUygulamasi.Entities.Concrete;

namespace MagazaUygulamasi.Repositories.Abstract
{
    public abstract class BaseCustomerRepositories
    {
        public abstract void Add(Customer customer);
        public abstract void Update(Customer customer);
        public abstract void Delete(int id);
        public abstract Customer GetById(int id);
        public abstract List<Customer> GetAll();

    }
}