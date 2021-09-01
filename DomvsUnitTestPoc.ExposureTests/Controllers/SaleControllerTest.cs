using AutoMapper;
using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Exposure;
using DomvsUnitTestPoc.Exposure.AutoMappers;
using DomvsUnitTestPoc.Exposure.Controllers;
using DomvsUnitTestPoc.Exposure.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace DomvsUnitTestPoc.ExposureTests.Controllers
{
    public class SaleControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public SaleControllerTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_Throws_MethodNotAllowed_StatusCode_When_Call_Create_With_Delete_Http_Verb()
        {
            //Act
            var response = await _client.DeleteAsync("Sale/Create");
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            Assert.Equal(string.Empty, content);
        }

        [Fact]
        public async Task Should_Throws_MethodNotAllowed_When_Call_StatusCode_Call_Create_With_Get_Http_Verb()
        {
            //Act
            var response = await _client.GetAsync("Sale/Create");
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            Assert.Equal(string.Empty, content);
        }

        [Fact]
        public async Task Should_Throws_MethodNotAllowed_StatusCode_When_Call_Create_With_Patch_Http_Verb()
        {
            //Arrange
            List<SaleModel> body = null;

            //Act
            var response = await _client.PatchAsync("Sale/Create", JsonContent.Create(body));
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            Assert.Equal(string.Empty, content);
        }

        [Fact]
        public async Task Should_Throws_MethodNotAllowed_StatusCode_When_Call_Create_With_Post_Http_Verb()
        {
            //Arrange
            List<SaleModel> body = null;

            //Act
            var response = await _client.PostAsync("Sale/Create", JsonContent.Create(body));
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("A non-empty request body is required.", content);
        }

        [Fact]
        public async Task Should_Throws_MethodNotAllowed_StatusCod_When_Call_Create_With_Put_Http_Verbe()
        {
            //Arrange
            List<SaleModel> body = null;

            //Act
            var response = await _client.PutAsync("Sale/Create", JsonContent.Create(body));
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            Assert.Equal(string.Empty, content);
        }

        [Fact]
        public async Task Should_Receive_Accepted_StatusCod_When_Call_Create_With_Correcly_Parametters()
        {
            //Arrange
            var iLogger = Substitute.For<ILogger<ProductController>>();
            var createSaleHandler = Substitute.For<IRequestHandler<CreateSaleCommand, bool>>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<AutoMappingExposure>();
            });
            var mapper = mapperConfig.CreateMapper();

            var controller = new SaleController(
                mapper,
                iLogger,
                createSaleHandler);

            List<SaleModel> body = new List<SaleModel>()
            {
                new SaleModel()
                {
                    Id = 345,
                    ProductId = 1,
                    ProductName = "Teste",
                    ProductPrice = 10.34m,
                    ProductQuantity = 2
                }
            };
            CreateSaleCommand received = null;

            createSaleHandler.Handle(Arg.Any<CreateSaleCommand>(), default).Returns((parametter) =>
            {
                received = (CreateSaleCommand)parametter[0];
                return Task.FromResult(true);
            });

            //Act
            IActionResult response = null;
            Func<Task> run = async () =>
            {
                response = await controller.Create(body);
            };

            //Assert
            await run.Should().NotThrowAsync<Exception>();
            response.Should().BeOfType<AcceptedResult>();
            response.Should().NotBeNull();
            await createSaleHandler.Received(1).Handle(Arg.Any<CreateSaleCommand>(), default);
            received.Should().NotBeNull();
            received.Sales.Should().HaveCount(1);
            var receivedFirst = received.Sales.FirstOrDefault();
            receivedFirst.Id.Should().Be(body.First().Id);
            receivedFirst.ProductId.Should().Be(body.First().ProductId);
            receivedFirst.ProductName.Should().Be(body.First().ProductName);
            receivedFirst.ProductPrice.Should().Be(body.First().ProductPrice);
            receivedFirst.ProductQuantity.Should().Be(body.First().ProductQuantity);
        }
    }
}
