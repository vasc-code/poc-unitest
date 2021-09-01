using AutoMapper;
using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Application.Queries
{
    public class ListProductQuery : IRequestHandler<ListProductCommand, PagedModel<Product>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ListProductQuery(
            IMapper mapper,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<PagedModel<Product>> Handle(ListProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.ListProductsAsync(request.Search);
        }
    }
}
