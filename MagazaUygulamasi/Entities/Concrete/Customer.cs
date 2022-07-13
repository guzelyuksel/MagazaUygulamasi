using MagazaUygulamasi.Entities.Abstract;
using MagazaUygulamasi.Enums;
using System;

namespace MagazaUygulamasi.Entities.Concrete
{
    public class Customer : BaseEntities
    {
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value.Trim().Length < 3)
                    throw new Exception("First name must be at least 3 characters !");
                _firstName = value;
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Trim().Length < 3)
                    throw new Exception("Last name must be at least 3 characters !");
                _lastName = value;
            }
        }

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

        public DateTime BirthDate { get; set; }

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

        public Customer(int id) => Id = id;
    }
}