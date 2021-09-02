using AutoMapper;
using Dapper;
using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Domain.Interfaces;
using DomvsUnitTestPoc.Infrastructure.Constants;
using DomvsUnitTestPoc.Infrastructure.Contexts;
using DomvsUnitTestPoc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DomvsUnitTestPoc.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(
            TransactionContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name)
               || product.Price <= 0
               || product.Quantity <= 0
               || product.Id > 0)
            {
                throw new Exception(InfrastructureConstants.InvalidData);
            }
            var entity = _mapper.Map<ProductEntity>(product);
            await _dbContext.AddAsync(entity);
        }

        public async Task UpdateAsync(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name)
               || product.Price <= 0
               || product.Quantity <= 0
               || product.Id <= 0)
            {
                throw new Exception(InfrastructureConstants.InvalidData);
            }
            var entity = _mapper.Map<ProductEntity>(product);
            var exists = await _dbContext.ProductEntity.FirstOrDefaultAsync(a => a.Id == entity.Id);
            if (exists == null)
            {
                throw new Exception(InfrastructureConstants.ProductNotExistsForUpdate);
            }
            entity.Id = exists.Id;
            _dbContext.Entry(exists).CurrentValues.SetValues(entity);
        }

        public async Task DeleteAsync(Product product)
        {
            if (product.Id <= 0)
            {
                throw new Exception(InfrastructureConstants.InvalidData);
            }
            var entity = _mapper.Map<ProductEntity>(product);
            var exists = await _dbContext.ProductEntity.FirstOrDefaultAsync(a => a.Id == entity.Id);
            if (exists == null)
            {
                throw new Exception(InfrastructureConstants.ProductNotExistsForDelete);
            }
            _dbContext.ProductEntity.Remove(exists);
        }

        public async Task<Product> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new Exception(InfrastructureConstants.InvalidData);
            }
            var exists = await _dbContext.ProductEntity.FirstOrDefaultAsync(a => a.Id == id);
            var entity = _mapper.Map<Product>(exists);
            return entity;
        }

        public async Task<PagedModel<Product>> ListAsync(int size, int start, int page)
        {
            if (size <= 0
                || start < 0
                || page < 0)
            {
                throw new Exception(InfrastructureConstants.InvalidData);
            }
            var count = await _dbContext.ProductEntity.CountAsync();
            var result = await _dbContext.ProductEntity.Skip(start).Take(size).ToListAsync();
            var response = result.Select(a => _mapper.Map<Product>(a)).ToList();
            return new PagedModel<Product>()
            {
                CurrentPage = page,
                PageSize = size,
                TotalItems = count,
                TotalPages = (count / size) + 1,
                Items = response
            };
        }

        public async Task<IList<Product>> ListAsync(IList<long> ids)
        {
            if (!ids?.Any() ?? false)
            {
                throw new Exception(InfrastructureConstants.InvalidData);
            }
            var result = await _dbContext.ProductEntity.Where(a => ids.Contains(a.Id)).ToListAsync();
            var response = result.Select(a => _mapper.Map<Product>(a)).ToList();
            return response;
        }

        public async Task<PagedModel<Product>> ListProductsAsync(string likeName)
        {
            using (var con = _dbContext.GetConnection())
            {
                var sql = "" +
                    " USE       domvsunittestpocdb;" +
                    " SELECT 	p.ID as Id," +
                    "           p.NOME as Name, " +
                    "           p.PRECO as Price, " +
                    "           p.QUANTIDADE as Quantity, " +
                    "           p.CRIADO_EM as CreateAt, " +
                    "           p.ATUALIZADO_EM as UpdateAt " +
                    " FROM 		produto p" +
                    " WHERE 	NOME like concat('%',@searchText,'%');" +
                    " SELECT 	p.ID as Id," +
                    "           p.NOME as Name, " +
                    "           p.PRECO as Price, " +
                    "           p.QUANTIDADE as Quantity, " +
                    "           p.CRIADO_EM as CreateAt, " +
                    "           p.ATUALIZADO_EM as UpdateAt " +
                    " FROM 		produto p" +
                    " WHERE 	PRECO > 2;";
                var reader = await con.QueryMultipleAsync(sql, new
                {
                    searchText = likeName ?? string.Empty
                }, commandType: CommandType.Text);
                var first = reader.Read<ProductEntity>().ToList();
                var second = reader.Read<ProductEntity>().ToList();
                first.AddRange(second);
                var mapped = first.Select(a => _mapper.Map<Product>(a)).ToList();
                var count = first.Count();

                return new PagedModel<Product>()
                {
                    CurrentPage = 1,
                    PageSize = 1,
                    TotalItems = count,
                    TotalPages = (count / 1) + 1,
                    Items = mapped
                };
            }
        }
    }
}
