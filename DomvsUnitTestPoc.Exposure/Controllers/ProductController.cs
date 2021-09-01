using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Exposure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Exposure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRequestHandler<CreateProductCommand, bool> _createProductHandler;
        private readonly IRequestHandler<ListProductCommand, PagedModel<Product>> _listProductQuery;

        public ProductController(
            ILogger<ProductController> logger,
            IRequestHandler<CreateProductCommand, bool> createProductHandler,
            IRequestHandler<ListProductCommand, PagedModel<Product>> listProductQuery)
        {
            _logger = logger;
            _createProductHandler = createProductHandler;
            _listProductQuery = listProductQuery;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateModel model)
        {
            var command = new CreateProductCommand(model.Name, model.Price, model.Quantity);
            var response = await _createProductHandler.Handle(command, default);
            return Ok();
        }

        //TODO alterar produto
        //TODO excluir produto
        //TODO obter produto

        [HttpPost]
        [Route("List")]
        public async Task<IActionResult> List(string search)
        {
            var command = new ListProductCommand(search);
            var response = await _listProductQuery.Handle(command, default);
            return Ok(response);
        }
    }
}
