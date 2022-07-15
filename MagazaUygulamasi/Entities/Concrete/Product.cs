using System;
using MagazaUygulamasi.Entities.Abstract;
using MagazaUygulamasi.Enums;

namespace MagazaUygulamasi.Entities.Concrete
{
    public class Product : BaseEntities
    {
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public Categories CategoryId { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitsOnOrder { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Product(int id) => Id = id;
    }
}