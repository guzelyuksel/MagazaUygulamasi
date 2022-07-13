using System.Collections.Generic;
using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Repositories.Abstract;

namespace MagazaUygulamasi.Repositories.Concrete
{
    public class EmployeeRepositories : BaseEmployeeRepositories
    {
        public override void Add(Employee newEmploye)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(int id, Employee employe)
        {
            throw new System.NotImplementedException();
        }

        public override Employee GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public override List<Employee> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public override int GetTotalSales(int id)
        {
            throw new System.NotImplementedException();
        }

        public override int CalculateBonus(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}