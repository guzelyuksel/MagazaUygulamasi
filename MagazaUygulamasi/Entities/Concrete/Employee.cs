using System;
using MagazaUygulamasi.Entities.Abstract;
using MagazaUygulamasi.Enums;

namespace MagazaUygulamasi.Entities.Concrete
{
    public class Employee : BaseEntities
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdentificationNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DateOfStart { get; set; }

        public Genders Gender;

        public Positions Position;

        public string Email { get; set; }

        public Cities City { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int TotalSales { get; set; }

        private const decimal _salary = 5500;

        public decimal Salary
        {
            get
            {
                var positionMultiplier = TotalSales > 9 ? 1.1m : 1m;

                switch (Position)
                {
                    case Positions.Admin:
                        positionMultiplier = 1.7m;
                        break;
                    case Positions.Manager:
                        positionMultiplier = 1.4m;
                        break;
                    case Positions.SalesPerson:
                        positionMultiplier = 1.2m;
                        break;
                    case Positions.CashierPerson:
                        positionMultiplier = 1.1m;
                        break;
                    default:
                        positionMultiplier = 1;
                        break;
                }
                var totalDays = (DateTime.Now - DateOfStart).Days;
                switch (totalDays % 365)
                {
                    case int days when days <= 2 && days > 0:
                        return _salary * 1.05m * positionMultiplier;
                    case int days when days > 2 && days <= 5:
                        return _salary * 1.15m * positionMultiplier;
                    case int days when days > 5 && days <= 10:
                        return _salary * 1.25m * positionMultiplier;
                    case int days when days > 10:
                        return _salary * 1.35m * positionMultiplier;
                    default:
                        return 0;
                }
            }
        }

        public Employee(int id) => Id = id;
    }
}