using System;

namespace DomvsUnitTestPoc.Infrastructure.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
