using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomvsUnitTestPoc.Infrastructure.Entities
{
    [Table("PRODUTO")]
    public class ProductEntity : BaseEntity
    {
        [Column("NOME")]
        public string Name { get; set; }
        
        [Column("PRECO")]
        public decimal Price { get; set; }
        
        [Column("QUANTIDADE")]
        public int Quantity { get; set; }
        
        public ICollection<SaleEntity> Sales { get; set; }
    }
}
