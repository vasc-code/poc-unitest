using DomvsUnitTestPoc.Domain.Interfaces;
using DomvsUnitTestPoc.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Infrastructure.Repositories
{
    public abstract class RepositoryBase : IRepository
    {
        protected readonly TransactionContext _dbContext;
        private IDbContextTransaction _transaction;

        protected RepositoryBase(TransactionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _dbContext.Database.BeginTransactionAsync();
            }
            else
            {
                throw new Exception("The transaction has alredy started previously");
            }
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            else
            {
                throw new Exception("The transaction was not started previously");
            }

            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            else
            {
                throw new Exception("The transaction was not started previously");
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
