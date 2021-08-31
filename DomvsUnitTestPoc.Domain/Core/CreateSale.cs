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
                    throw new Exception("Um produto do carrinho não está cadastrado.");
                }
            }
            foreach (var product in products)
            {
                var soldAmount = sales.Where(a => a.ProductId == product.Id).Sum(a => a.ProductQuantity);
                if (product.Quantity < soldAmount)
                {
                    throw new Exception("Um produto não tem quantidade suficiente no estoque.");
                }
            }
            this.products = products;
            this.sales = sales;
        }

        public CreateSaleResponse Calculate()
        {
            var sales = this.sales;
            var products = new List<Product>();
            foreach (var product in this.products)
            {
                var soldAmount = sales.Where(a => a.ProductId == product.Id).Sum(a => a.ProductQuantity);
                product.Quantity = product.Quantity - soldAmount;
                products.Add(product);
            }

            return new CreateSaleResponse(sales, products);
        }
    }
}
