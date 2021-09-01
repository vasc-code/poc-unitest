using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Domain.Interfaces
{
    public interface IProductRepository : IRepository
    {
        public Task CreateAsync(Product product);

        public Task UpdateAsync(Product product);

        public Task DeleteAsync(Product product);

        public Task<Product> GetAsync(long id);
        public Task<PagedModel<Product>> ListAsync(int size, int start, int page);
        public Task<IList<Product>> ListAsync(IList<long> ids);
        public Task<PagedModel<Product>> ListProductsAsync(string likeName);
    }
}
