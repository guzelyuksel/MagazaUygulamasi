﻿using MagazaUygulamasi.Entities.Concrete;
using System.Collections.Generic;

namespace MagazaUygulamasi.Repositories.Abstract
{
    public abstract class BaseEmployeeRepositories
    {
        public abstract void Add(Employee newEmployee);
        public abstract void Delete(int id);
        public abstract void Update(Employee employee);
        public abstract Employee GetById(int id);
        public abstract List<Employee> GetAll();
        public abstract int GetTotalSales(int id);
        public abstract int CalculateBonus(int id);
        
    }
}