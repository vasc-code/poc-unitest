using DomvsUnitTestPoc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;

namespace DomvsUnitTestPoc.Infrastructure.Contexts
{
    public class TransactionContext : DbContext
    {
        public readonly string connectionString;

        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
            connectionString = this.Database.GetConnectionString();
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(this.connectionString);
        }

        public DbSet<SaleEntity> SaleEntity { get; set; }
        public DbSet<ProductEntity> ProductEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleEntity>().HasKey(a => a.Id);
            modelBuilder.Entity<SaleEntity>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<SaleEntity>().Property(a => a.Id).UseMySqlIdentityColumn();
            modelBuilder.Entity<SaleEntity>().HasOne(a => a.Product).WithMany(a => a.Sales).HasForeignKey(a => a.ProductId).IsRequired();

            modelBuilder.Entity<ProductEntity>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductEntity>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProductEntity>().Property(a => a.Id).UseMySqlIdentityColumn();

            modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity[]
                {
                    new ProductEntity()
                    {
                        CreateAt = DateTime.Now,
                        Id = 1,
                        Name = "Lápis Faber Castel",
                        Price = 1.29m,
                        Quantity = 100
                    }
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
