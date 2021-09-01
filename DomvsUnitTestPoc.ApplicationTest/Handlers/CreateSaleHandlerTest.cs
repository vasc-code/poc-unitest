using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Application.Handlers;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DomvsUnitTestPoc.ApplicationTest.Handlers
{
    public class CreateSaleHandlerTest
    {
        [Fact]
        public void Should_Success_When_Execute_CreateSaleHandler_With_Correcly_Parametters()
        {
            //Arrange
            var productRepository = Substitute.For<IProductRepository>();
            var saleRepository = Substitute.For<ISaleRepository>();
            var createSaleHandler = new CreateSaleHandler(productRepository, saleRepository);

            productRepository.UpdateAsync(Arg.Any<Product>()).Returns(Task.FromResult(true));
            saleRepository.UpdateAsync(Arg.Any<Sale>()).Returns(Task.FromResult(true));
            productRepository.ListAsync(Arg.Any<List<long>>())
                .Returns(Task.FromResult((IList<Product>)new List<Product>()
                    {
                        new Product()
                        {
                            CreateAt = DateTime.Now,
                            Id = 1,
                            Name = "xpto",
                            Price = 10,
                            Quantity = 20
                        }
                    }));

            var command = new CreateSaleCommand(new List<Sale>()
            {
                new Sale()
                {
                    CreateAt = DateTime.Now,
                    Id = 1,
                    ProductId = 1,
                    ProductName = "xpto",
                    ProductPrice = 10,
                    ProductQuantity = 10
                }
            });

            //Act
            bool? result = null;
            Func<Task> act = async () =>
            {
                result = await createSaleHandler.Handle(command, default);
            };

            //Assertd
            act.Should().NotThrowAsync<Exception>();
            saleRepository.Received(1).BeginTransactionAsync();
            productRepository.Received(1).ListAsync(Arg.Any<List<long>>());
            productRepository.Received(1).UpdateAsync(Arg.Any<Product>());
            saleRepository.Received(1).CreateAsync(Arg.Any<Sale>());
            saleRepository.Received(1).CommitAsync();
            saleRepository.Received(0).RollbackAsync();
        }
    }
}
