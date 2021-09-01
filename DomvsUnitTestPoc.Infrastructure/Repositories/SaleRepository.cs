using AutoMapper;
using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Domain.Interfaces;
using DomvsUnitTestPoc.Infrastructure.Constants;
using DomvsUnitTestPoc.Infrastructure.Contexts;
using DomvsUnitTestPoc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Infrastructure.Repositories
{
    public class SaleRepository : RepositoryBase, ISaleRepository
    {
        private readonly IMapper _mapper;

        public SaleRepository(
            TransactionContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(Sale sale)
        {
            if (string.IsNullOrWhiteSpace(sale?.ProductName)
               || sale?.ProductPrice <= 0
               || sale?.ProductQuantity <= 0
               || sale?.Id > 0)
            {
                throw new Exception(InfrastructureConstants.InvalidData);
            }
            var entity = _mapper.Map<SaleEntity>(sale);
            await _dbContext.AddAsync(entity);
        }

        public async Task UpdateAsync(Sale sale)
        {
            if (string.IsNullOrWhiteSpace(sale.ProductName)
               || sale.ProductPrice <= 0
               || sale.ProductQuantity <= 0
               || sale.Id <= 0)
            {
                throw new Exception("Dados inválidos");
            }
            var entity = _mapper.Map<SaleEntity>(sale);
            var exists = await _dbContext.ProductEntity.FirstOrDefaultAsync(a => a.Id == entity.Id);
            if (exists == null)
            {
                throw new Exception("Produto não pode ser alterado pois não existe");
            }
            entity.Id = exists.Id;
            _dbContext.Entry(exists).CurrentValues.SetValues(entity);
        }

        public async Task DeleteAsync(Sale sale)
        {
            if (sale.Id <= 0)
            {
                throw new Exception("Dados inválidos");
            }
            var entity = _mapper.Map<SaleEntity>(sale);
            var exists = await _dbContext.ProductEntity.FirstOrDefaultAsync(a => a.Id == entity.Id);
            if (exists == null)
            {
                throw new Exception("Produto não pode ser excluído pois não existe");
            }
            _dbContext.ProductEntity.Remove(exists);
        }

        public async Task<Sale> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new Exception("Dados inválidos");
            }
            var exists = await _dbContext.SaleEntity.FirstOrDefaultAsync(a => a.Id == id);
            var entity = _mapper.Map<Sale>(exists);
            return entity;
        }

        public async Task<PagedModel<Sale>> ListAsync(int size, int start, int page)
        {
            if (size <= 0
                || start < 0
                || page < 0)
            {
                throw new Exception("Dados inválidos");
            }
            var count = await _dbContext.SaleEntity.CountAsync();
            var result = await _dbContext.SaleEntity.Skip(start).Take(size).ToListAsync();
            var response = result.Select(a => _mapper.Map<Sale>(a)).ToList();
            return new PagedModel<Sale>()
            {
                CurrentPage = page,
                PageSize = size,
                TotalItems = count,
                TotalPages = (count / size) + 1,
                Items = response
            };
        }
    }
}
