using System.ComponentModel.DataAnnotations.Schema;

namespace DomvsUnitTestPoc.Infrastructure.Entities
{
    [Table("VENDAS")]
    public class SaleEntity : BaseEntity
    {
        [Column("NOME_PRODUTO")]
        public string ProductName { get; set; }

        [Column("ID_PRODUTO")]
        public long ProductId { get; set; }

        [Column("PRECO_PRODUTO")]
        public decimal ProductPrice { get; set; }

        [Column("QUANTIDADE_VENDIDA")]
        public int ProductQuantity { get; set; }

        public ProductEntity Product { get; set; }
    }
}
