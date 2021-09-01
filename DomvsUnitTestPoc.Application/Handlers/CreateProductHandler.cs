using AutoMapper;
using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(
            IMapper mapper,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            product.CreateAt = DateTime.Now;
            await _productRepository.CreateAsync(product);
            var affecteds = await _productRepository.SaveAsync();
            return affecteds > 0;
        }
    }
}
