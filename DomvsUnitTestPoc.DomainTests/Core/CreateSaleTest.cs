using DomvsUnitTestPoc.Domain.Constants;
using DomvsUnitTestPoc.Domain.Core;
using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DomvsUnitTestPoc.DomainTests.Core
{
    public class CreateSaleTest
    {
        [Fact]
        public void Should_Success_When_Execute_CreateSale_With_Correcly_Parametters()
        {
            //Arrange
            var products = new List<Product>()
            {
                 new Product()
                 {
                     CreateAt = DateTime.Now,
                     Id = 1,
                     Name = "xpto",
                     Price = 10,
                     Quantity = 100
                 }
            };
            var sales = new List<Sale>()
            {
                new Sale()
                {
                    CreateAt = DateTime.Now,
                    Id = 1,
                    ProductId = 1,
                    ProductName = "xpto",
                    ProductPrice = 10,
                    ProductQuantity = 50
                }
            };

            //Act
            CreateSaleResponse createSaleResponse = null;
            Action run = () =>
            {
                var createSale = new CreateSale(products, sales);
                createSaleResponse = createSale.Calculate();
            };

            //Assert
            run.Should().NotThrow<Exception>();
            createSaleResponse.Should().NotBeNull();
            createSaleResponse.Sales.Should().HaveCount(1);
            createSaleResponse.Products.Should().HaveCount(1);
            var product = createSaleResponse.Products.First();
            product.Quantity.Should().Be(50);
            product.UpdateAt.Should().NotBeNull();
            product.UpdateAt.Value.Date.Should().Be(DateTime.Now.Date);
            var sale = createSaleResponse.Sales.First();
            product.UpdateAt.Value.Date.Should().Be(DateTime.Now.Date);
        }
        [Fact]
        public void Should_Throws_When_Execute_CreateSale_With_Quantity_Less_Than_Saled_Parametters()
        {
            //Arrange
            var products = new List<Product>()
            {
                 new Product()
                 {
                     CreateAt = DateTime.Now,
                     Id = 1,
                     Name = "xpto",
                     Price = 10,
                     Quantity = 10
                 }
            };
            var sales = new List<Sale>()
            {
                new Sale()
                {
                    CreateAt = DateTime.Now,
                    Id = 1,
                    ProductId = 1,
                    ProductName = "xpto",
                    ProductPrice = 10,
                    ProductQuantity = 30
                }
            };

            //Act
            CreateSaleResponse createSaleResponse = null;
            Action run = () =>
            {
                var createSale = new CreateSale(products, sales);
                createSaleResponse = createSale.Calculate();
            };

            //Assert
            run.Should().Throw<Exception>().WithMessage(DomainConstants.ProductLessThanQuantity);
        }
        [Fact]
        public void Should_Throws_When_Execute_CreateSale_With_No_Product_Parametters()
        {
            //Arrange
            var products = new List<Product>();
            var sales = new List<Sale>()
            {
                new Sale()
                {
                    CreateAt = DateTime.Now,
                    Id = 1,
                    ProductId = 1,
                    ProductName = "xpto",
                    ProductPrice = 10,
                    ProductQuantity = 30
                }
            };

            //Act
            CreateSaleResponse createSaleResponse = null;
            Action run = () =>
            {
                var createSale = new CreateSale(products, sales);
                createSaleResponse = createSale.Calculate();
            };

            //Assert
            run.Should().Throw<Exception>().WithMessage(DomainConstants.ProductUnknow);
        }
    }
}
