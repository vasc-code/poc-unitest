using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomvsUnitTestPoc.Infrastructure.Entities
{
    public abstract class BaseEntity
    {
        [Column("ID")]
        public long Id { get; set; }

        [Column("CRIADO_EM")]
        public DateTime CreateAt { get; set; }

        [Column("ATUALIZADO_EM")]
        public DateTime? UpdateAt { get; set; }
    }
}
