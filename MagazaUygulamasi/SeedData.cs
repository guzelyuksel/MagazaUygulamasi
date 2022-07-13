using MagazaUygulamasi.Entities.Concrete;
using MagazaUygulamasi.Enums;
using System;
using System.Collections.Generic;

namespace MagazaUygulamasi
{
    public static class SeedData
    {
        public static List<Product> Products;
        public static List<Employee> Employees;
        public static List<Customer> Customers;

        public static void Generate()
        {
            Products = new List<Product>()
            {
                new Product(1){
                    ProductName = "Test Product 1 Accessories",
                    ProductDescription = "Test Product 1 Accessories Description 1234567890",
                    UnitPrice = 100,
                    CategoryId = Categories.Accessories,
                    UnitsInStock = 100,
                    UnitsOnOrder = 2,
                    ExpirationDate = DateTime.Now.AddDays(10)
                },
                new Product(2)
                {
                    ProductName = "Test Product 1 Books",
                    ProductDescription = "Test Product 1 Books Description 1234567890",
                    UnitPrice = 20,
                    CategoryId = Categories.Books,
                    UnitsInStock = 10,
                    UnitsOnOrder = 0,
                    ExpirationDate = DateTime.Now.AddYears(10)
                },
                new Product(3)
                {
                    ProductName = "Test Product 1 Home",
                    ProductDescription = "Test Product 1 Home Description 1234567890",
                    UnitPrice = 450,
                    CategoryId = Categories.Home,
                    UnitsInStock = 12,
                    UnitsOnOrder = 11,
                    ExpirationDate = DateTime.Now.AddYears(5)
                },
                new Product(4)
                {
                    ProductName = "Test Product 1 Electronics",
                    ProductDescription = "Test Product 1 Electronics Description 1234567890",
                    UnitPrice = 2000,
                    CategoryId = Categories.Electronics,
                    UnitsInStock = 8,
                    UnitsOnOrder = 5,
                    ExpirationDate = DateTime.Now.AddYears(2)
                },
                new Product(5)
                {
                    ProductName = "Test Product 1 Food",
                    ProductDescription = "Test Product 1 Food Description 1234567890",
                    UnitPrice = 12,
                    CategoryId = Categories.Food,
                    UnitsInStock = 100,
                    UnitsOnOrder = 56,
                    ExpirationDate = DateTime.Now.AddMonths(1)
                },
                new Product(6)
                {
                    ProductName = "Test Product 1 All",
                    ProductDescription = "Test Product 1 All Description 1234567890",
                    UnitPrice = 0.99m,
                    CategoryId = Categories.All,
                    UnitsInStock = 120,
                    UnitsOnOrder = 12,
                    ExpirationDate = DateTime.Now.AddDays(5),
                },
                new Product(7){
                    ProductName = "Test Product 2 Accessories",
                    ProductDescription = "Test Product 2 Accessories Description 1234567890",
                    UnitPrice = 90,
                    CategoryId = Categories.Accessories,
                    UnitsInStock = 20,
                    UnitsOnOrder = 1,
                    ExpirationDate = DateTime.Now.AddDays(12)
                },
                new Product(8)
                {
                    ProductName = "Test Product 2 Books",
                    ProductDescription = "Test Product 2 Books Description 1234567890",
                    UnitPrice = 18,
                    CategoryId = Categories.Books,
                    UnitsInStock = 8,
                    UnitsOnOrder = 2,
                    ExpirationDate = DateTime.Now.AddYears(8)
                },
                new Product(9)
                {
                    ProductName = "Test Product 2 Home",
                    ProductDescription = "Test Product 2 Home Description 1234567890",
                    UnitPrice = 250,
                    CategoryId = Categories.Home,
                    UnitsInStock = 20,
                    UnitsOnOrder = 2,
                    ExpirationDate = DateTime.Now.AddYears(2)
                },
                new Product(10)
                {
                    ProductName = "Test Product 2 Electronics",
                    ProductDescription = "Test Product 2 Electronics Description 1234567890",
                    UnitPrice = 4500,
                    CategoryId = Categories.Electronics,
                    UnitsInStock = 11,
                    UnitsOnOrder = 2,
                    ExpirationDate = DateTime.Now.AddYears(3)
                },
                new Product(11)
                {
                    ProductName = "Test Product 2 Food",
                    ProductDescription = "Test Product 2 Food Description 1234567890",
                    UnitPrice = 16,
                    CategoryId = Categories.Food,
                    UnitsInStock = 85,
                    UnitsOnOrder = 12,
                    ExpirationDate = DateTime.Now.AddMonths(2)
                },
                new Product(12)
                {
                    ProductName = "Test Product 2 All",
                    ProductDescription = "Test Product 2 All Description 1234567890",
                    UnitPrice = 1.99m,
                    CategoryId = Categories.All,
                    UnitsInStock = 12,
                    UnitsOnOrder = 1,
                    ExpirationDate = DateTime.Now.AddDays(7),
                }
            };

            Employees = new List<Employee>
            {
                new Employee(1)
                {
                    FirstName = "Yüksel",
                    LastName = "Güzel",
                    IdentificationNumber = "12345678901",
                    BirthDate = DateTime.Parse("28/09/1992"),
                    DateOfStart = DateTime.Parse("28/09/2012"),
                    Gender = Genders.Male,
                    Position = Positions.Admin,
                    Email = "yuksel.guzel@bilgeadamboost.com",
                    City = Cities.Ankara,
                    Address = "Random address for Yüksel Güzel",
                    PhoneNumber = "05555555555",
                },
                new Employee(2)
                {
                    FirstName = "Marta",
                    LastName = "Howard",
                    IdentificationNumber = "12345678901",
                    BirthDate = DateTime.Parse("24/07/1960"),
                    DateOfStart = DateTime.Parse("25/07/2000"),
                    Gender = Genders.Female,
                    Position = Positions.CashierPerson,
                    Email = "MartaSHoward@teleworm.us",
                    City = Cities.İstanbul,
                    Address = "Random address for Marta Howard",
                    PhoneNumber = "01111111111",
                },
                new Employee(3)
                {
                    FirstName = "Wilson",
                    LastName = "Robinson",
                    IdentificationNumber = "12345678901",
                    BirthDate = DateTime.Parse("11/10/1993"),
                    DateOfStart = DateTime.Parse("15/01/2015"),
                    Gender = Genders.Male,
                    Position = Positions.SalesPerson,
                    Email = "joe2012@hotmail.com",
                    City = Cities.İzmir,
                    Address = "Random address for Wilson Robinson",
                    PhoneNumber = "01111111112",
                },
                new Employee(4)
                {
                    FirstName = "Mary",
                    LastName = "Jardine",
                    IdentificationNumber = "12345678901",
                    BirthDate = DateTime.Parse("12/11/1983"),
                    DateOfStart = DateTime.Parse("11/03/2003"),
                    Gender = Genders.Female,
                    Position = Positions.Manager,
                    Email = "al2005@hotmail.com",
                    City = Cities.Bursa,
                    Address = "Random address for Mary Jardine",
                    PhoneNumber = "01111111113",
                }
            };

            Customers = new List<Customer>()
            {
                new Customer(1)
                {
                    FirstName = "Bradley",
                    LastName = "Straub",
                    IdentificationNumber = "12345678901",
                    BirthDate = DateTime.Parse("25/02/1976"),
                    City = Cities.Siirt,
                    Gender = Genders.Male,
                    Address = "Random address for Bradle Straub",
                    PhoneNumber = "05555555555",
                    Email = "mattie1994@gmail.com",
                },
                new Customer(2)
                {
                    FirstName = "Frederick",
                    LastName = "Waddell",
                    IdentificationNumber = "12345678901",
                    BirthDate = DateTime.Parse("05/07/1970"),
                    City = Cities.Hakkari,
                    Gender = Genders.Male,
                    Address = "Random address for Frederick Waddell",
                    PhoneNumber = "05555555555",
                    Email = "jamal_bergstr@yahoo.com",
                },
                new Customer(3)
                {
                    FirstName = "Jeffrey",
                    LastName = "Hartzler",
                    IdentificationNumber = "12345678901",
                    BirthDate = DateTime.Parse("21/08/1950"),
                    City = Cities.Kars,
                    Gender = Genders.Male,
                    Address = "Random address for Jeffrey Hartzler",
                    PhoneNumber = "05555555555",
                    Email = "bradley_huds@hotmail.com",
                }
            };
        }
    }
}
