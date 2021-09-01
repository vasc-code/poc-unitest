using DomvsUnitTestPoc.Domain.Constants;
using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomvsUnitTestPoc.Domain.Core
{
    public class CreateSale
    {
        private readonly List<Product> products;
        private readonly List<Sale> sales;

        public CreateSale(List<Product> products, List<Sale> sales)
        {
            foreach (var sale in sales)
            {
                if (!products.Any(a => a.Id == sale.ProductId))
                {
                    throw new Exception(DomainConstants.ProductUnknow);
                }
            }
            foreach (var product in products)
            {
                var soldAmount = sales.Where(a => a.ProductId == product.Id).Sum(a => a.ProductQuantity);
                if (product.Quantity < soldAmount)
                {
                    throw new Exception(DomainConstants.ProductLessThanQuantity);
                }
            }
            this.products = products;
            this.sales = sales;
        }

        public CreateSaleResponse Calculate()
        {
            this.sales.ForEach(a => a.UpdateAt = DateTime.Now);
            var sales = this.sales;
            var products = new List<Product>();
            foreach (var product in this.products)
            {
                var soldAmount = sales.Where(a => a.ProductId == product.Id).Sum(a => a.ProductQuantity);
                product.Quantity = product.Quantity - soldAmount;
                product.UpdateAt = DateTime.Now;
                products.Add(product);
            }

            return new CreateSaleResponse(sales, products);
        }
    }
}
