using System;
using System.Collections.Generic;

namespace DomvsUnitTestPoc.Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
