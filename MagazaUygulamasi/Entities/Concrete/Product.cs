using System;
using MagazaUygulamasi.Entities.Abstract;
using MagazaUygulamasi.Enums;

namespace MagazaUygulamasi.Entities.Concrete
{
    public class Product : BaseEntities
    {
        private string _productName;

        public string ProductName
        {
            get => _productName;
            set
            {
                if (value.Length > 50)
                    throw new Exception("Product name cannot be longer than 50 characters!");
                if (value.Length < 3)
                    throw new Exception("Product name cannot be shorter than 3 characters!");
                _productName = value;
            }
        }

        private string _productDescription;

        public string ProductDescription
        {
            get => _productDescription;
            set
            {
                if (value.Length > 500)
                    throw new Exception("Product description cannot be longer than 500 characters!");
                if (value.Length < 30)
                    throw new Exception("Product description cannot be shorter than 30 characters!");
                _productDescription = value;
            }
        }

        public Categories CategoryId { get; set; }

        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (value < 0)
                    throw new Exception("Unit price cannot be negative!");
                _unitPrice = value;
            }
        }

        private int _unitsInStock;

        public int UnitsInStock
        {
            get => _unitsInStock;
            set
            {
                if (value < 0)
                    throw new Exception("Unit in stock cannot be negative !");
                _unitsInStock = value;
            }
        }

        private int _unitsOnOrder;

        public int UnitsOnOrder
        {
            get => _unitsOnOrder;
            set
            {
                if (value < _unitsInStock)
                {
                    _unitsInStock = 0;
                    throw new Exception($"{ProductName} has out of order ! You can sell only {_unitsInStock} !");
                }
                _unitsOnOrder = value;
            }
        }

        public Product(int id) => Id = id;
    }
}