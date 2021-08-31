using System;

namespace DomvsUnitTestPoc.Domain.Entities
{
    public class Sale
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public Product Product { get; set; }
    }
}
