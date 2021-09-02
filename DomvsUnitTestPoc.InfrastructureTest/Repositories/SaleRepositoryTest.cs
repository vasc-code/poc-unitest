using AutoMapper;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Infrastructure.AutoMappers;
using DomvsUnitTestPoc.Infrastructure.Constants;
using DomvsUnitTestPoc.Infrastructure.Contexts;
using DomvsUnitTestPoc.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DomvsUnitTestPoc.InfrastructureTest.Repositories
{
    public class SaleRepositoryTest
    {
        [Fact]
        public void Should_Throws_When_Execute_CreateAsync_With_Null_Parametters()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(a =>
            {
                a.AddProfile(new AutoMappingInfrastructure());
            });
            var mapper = mapperConfig.CreateMapper();
            var optionsDb = new DbContextOptionsBuilder<TransactionContext>().UseInMemoryDatabase(databaseName: "DomvsUnitTestPocDb").Options;
            using (var contextx = new TransactionContext(optionsDb))
            {
                var repository = new SaleRepository(contextx, mapper);
                Sale sale = null;

                //Act
                Func<Task> act = async () =>
                {
                    await repository.CreateAsync(sale);
                };

                //Assert
                act.Should().ThrowAsync<Exception>().WithMessage(InfrastructureConstants.InvalidData);
            }
        }

        [Fact]
        public void Should_Success_When_Execute_CreateAsync_With_Correcly_Parametters()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(a =>
            {
                a.AddProfile(new AutoMappingInfrastructure());
            });
            var mapper = mapperConfig.CreateMapper();
            var optionsDb = new DbContextOptionsBuilder<TransactionContext>().UseInMemoryDatabase(databaseName: "teste").Options;
            using (var contextx = new TransactionContext(optionsDb))
            {
                var repository = new SaleRepository(contextx, mapper);
                Sale sale = new Sale()
                {
                    CreateAt = DateTime.Now,
                    Id = 0,
                    ProductId = 1,
                    ProductName = "xpto",
                    ProductPrice = 10,
                    ProductQuantity = 10
                };

                //Act
                Func<Task> act = async () =>
                {
                    await repository.CreateAsync(sale);
                    await repository.SaveAsync();
                };

                //Assert
                act.Should().NotThrowAsync<Exception>();
                contextx.SaleEntity
                    .CountAsync()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult()
                    .Should()
                    .BeGreaterOrEqualTo(1);
            }
        }
    }
}
