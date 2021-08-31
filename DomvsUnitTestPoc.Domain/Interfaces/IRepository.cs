using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Domain.Interfaces
{
    public interface IRepository
    {
        public Task BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollbackAsync();
        public Task<int> SaveAsync();
    }
}
