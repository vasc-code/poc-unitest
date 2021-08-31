using System.ComponentModel.DataAnnotations.Schema;

namespace DomvsUnitTestPoc.Infrastructure.Entities
{
    [Table("Sale")]
    public class SaleEntity : BaseEntity
    {
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public ProductEntity Product { get; set; }
    }
}
