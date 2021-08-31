using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Domain.Core;
using DomvsUnitTestPoc.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Application.Handlers
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;

        public CreateSaleHandler(
            IProductRepository productRepository,
            ISaleRepository saleRepository)
        {
            _productRepository = productRepository;
            _saleRepository = saleRepository;
        }
        public async Task<bool> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.ListAsync(request.Sales.Select(a => a.ProductId).ToList());
            var createSale = new CreateSale(products.ToList(), request.Sales.ToList());
            var response = createSale.Calculate();

            try
            {
                await _saleRepository.BeginTransactionAsync();
                foreach (var item in response.Sales)
                {
                    await _saleRepository.UpdateAsync(item);
                }
                foreach (var item in response.Products)
                {
                    await _productRepository.UpdateAsync(item);
                }
                await _saleRepository.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await _saleRepository.RollbackAsync();
                throw;
            }
            return false;
        }
    }
}
