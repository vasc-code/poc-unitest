using AutoMapper;
using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Exposure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Exposure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        private readonly IRequestHandler<CreateSaleCommand, bool> _createSaleHandler;

        public SaleController(
            IMapper mapper,
            ILogger<ProductController> logger,
            IRequestHandler<CreateSaleCommand, bool> createSaleHandler)
        {
            _mapper = mapper;
            _logger = logger;
            _createSaleHandler = createSaleHandler;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(IList<SaleModel> sales)
        {
            var param = sales.Select(a => _mapper.Map<Sale>(a)).ToList();
            var command = new CreateSaleCommand(param);
            var response = await _createSaleHandler.Handle(command, default);
            return Accepted(response);
        }
    }
}
