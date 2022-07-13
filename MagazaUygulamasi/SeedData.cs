using MagazaUygulamasi.Entities.Concrete;
using System.Collections.Generic;

namespace MagazaUygulamasi
{
    public static class SeedData
    {
        // product list
        public static List<Product> products = new List<Product>()
        {
            new Product(1){
                ProductName = "Baskılı Kılıf",
                ProductDescription = "Baskılı kılıf Açıklaması 12345",
                UnitPrice = 100,
                CategoryId = 1,
                UnitsInStock = 100,
                UnitsOnOrder = 2
            },
        };
        // employee list
        public static List<Employee> employees = new List<Employee>();
        // customer list
        public static List<Customer> customers = new List<Customer>();

        public static void ResetData()
        {

        }
    }
}
