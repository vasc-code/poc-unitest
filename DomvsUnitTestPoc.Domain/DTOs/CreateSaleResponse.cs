using DomvsUnitTestPoc.Domain.Entities;
using System.Collections.Generic;

namespace DomvsUnitTestPoc.Domain.DTOs
{
    public class CreateSaleResponse
    {
        public IList<Sale> Sales { get; }
        public IList<Product> Products { get; }

        public CreateSaleResponse(IList<Sale> sales, IList<Product> products)
        {
            Sales = sales;
            Products = products;
        }
    }
}
