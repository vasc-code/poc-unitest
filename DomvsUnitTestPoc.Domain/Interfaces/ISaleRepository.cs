using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Domain.Interfaces
{
    public interface ISaleRepository : IRepository
    {

        public Task CreateAsync(Sale sale);

        public Task UpdateAsync(Sale sale);

        public Task DeleteAsync(Sale sale);

        public Task<Sale> GetAsync(long id);

        public Task<PagedModel<Sale>> ListAsync(int size, int start, int page);
    }
}
