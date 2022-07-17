using MagazaUygulamasi.Entities.Abstract;

namespace MagazaUygulamasi.Entities.Concrete
{
    public class Sale : BaseEntities
    {
        public int ProductId { get; set; }
        
        public int CustomerId { get; set; }
        
        public int EmployeeId { get; set; }
        
        public int Quantity { get; set; }
        
        public Sale(int id) => Id = id;
    }
}