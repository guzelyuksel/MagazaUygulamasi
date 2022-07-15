using System;

namespace MagazaUygulamasi.Entities.Abstract
{
    public class BaseEntities
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; } = DateTime.Now;
    }
}