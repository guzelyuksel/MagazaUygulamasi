using System;
using MagazaUygulamasi.Entities.Abstract;
using MagazaUygulamasi.Enums;

namespace MagazaUygulamasi.Entities.Concrete
{
    public class Customer : BaseEntities
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdentificationNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public Cities City { get; set; }

        public Genders Gender { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Customer(int id) => Id = id;
    }
}