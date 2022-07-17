using System.Collections.Generic;
using System.Linq;
using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Repositories.Abstract;

namespace MagazaUygulamasi.Repositories.Concrete
{
    public class EmployeeRepositories : BaseEmployeeRepositories
    {
        public override void Add(Employee newEmployee)
        {
            if (newEmployee != null)
                SeedData.Employees.Add(newEmployee);
        }

        public override void Delete(int id)
        {
            if (SeedData.Employees.Any(x => x.Id == id))
                SeedData.Employees.Remove(SeedData.Employees.First(x => x.Id == id));
        }

        public override void Update(Employee employee)
        {
            if (SeedData.Employees.All(x => x.Id != employee.Id))
                return;
            SeedData.Employees.Remove(SeedData.Employees.First(x => x.Id == employee.Id));
            SeedData.Employees.Add(employee);
        }

        public override Employee GetById(int id) => SeedData.Employees.FirstOrDefault(x => x.Id == id);

        public override List<Employee> GetAll() => SeedData.Employees;

        public override decimal GetTotalSales(int id)
        {
            var sales = SeedData.Sales.Where(x => x.EmployeeId == id);
            return sales.Sum(sale => sale.UnitPrice * sale.Quantity);
        }
    }
}