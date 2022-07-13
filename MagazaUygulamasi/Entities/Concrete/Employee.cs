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
                if (value.Year - DateTime.Now.Year < 18)
                    throw new Exception("Employees under the age of 18 cannot be allowed!");
                _birthDate = value;
            }
        }

        private DateTime _dateOfStart;

        public DateTime DateOfStart
        {
            get => _dateOfStart;
            set
            {
                if (value.Day > DateTime.Now.Day)
                    throw new Exception("Start date cannot be future date !");
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
                if (value.Length != 10)
                    throw new Exception("Phone Number must be 10 digits !");
                foreach (char c in value)
                    if (!char.IsDigit(c))
                        throw new Exception("Phone Number must be digits !");
                _phoneNumber = value;
            }
        }

        public Employee(int id) => Id = id;
    }
}