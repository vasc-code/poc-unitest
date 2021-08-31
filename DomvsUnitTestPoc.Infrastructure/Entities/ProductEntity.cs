using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomvsUnitTestPoc.Infrastructure.Entities
{
    [Table("Product")]
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<SaleEntity> Sales { get; set; }
    }
}
