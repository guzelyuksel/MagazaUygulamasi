using MagazaUygulamasi.Entities.Abstract;
using MagazaUygulamasi.Enums;
using System;

namespace MagazaUygulamasi.Entities.Concrete
{
    public class Employee : BaseEntities
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        private string _identificationNumber;

        public string IdentificationNumber
        {
            get => _identificationNumber;
            set
            {
                if (IsValidId(value))
                    _identificationNumber = value;
            }
        }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                //if (value.Year - DateTime.Now.Year < 18)
                //    throw new Exception("Employees under the age of 18 cannot be allowed!");
                _birthDate = value;
            }
        }

        private DateTime _dateOfStart;

        public DateTime DateOfStart
        {
            get => _dateOfStart;
            set
            {
                //if (value.Day > DateTime.Now.Day)
                //    throw new Exception("Start date cannot be future date !");
                _dateOfStart = value;
            }
        }

        public Genders Gender;

        public Positions Position;

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (IsvalidEmail(value))
                    _email = value;
            }
        }

        public Cities City { get; set; }

        public string Address { get; set; }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (IsValidPhoneNumber(value))
                    _phoneNumber = value;
            }
        }

        private const decimal _salary = 5500;

        public decimal Salary
        {
            get
            {
                decimal positionMultiplier = 1;
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
                var totalDays = (DateTime.Now - _dateOfStart).Days;
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