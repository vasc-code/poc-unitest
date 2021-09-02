﻿// <auto-generated />
using System;
using DomvsUnitTestPoc.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DomvsUnitTestPoc.Infrastructure.Migrations
{
    [DbContext(typeof(TransactionContext))]
    partial class TransactionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("DomvsUnitTestPoc.Infrastructure.Entities.ProductEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CRIADO_EM");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("NOME");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("PRECO");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("QUANTIDADE");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("ATUALIZADO_EM");

                    b.HasKey("Id");

                    b.ToTable("PRODUTO");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreateAt = new DateTime(2021, 9, 2, 16, 24, 48, 990, DateTimeKind.Local).AddTicks(8873),
                            Name = "Lápis Faber Castel",
                            Price = 1.29m,
                            Quantity = 100
                        });
                });

            modelBuilder.Entity("DomvsUnitTestPoc.Infrastructure.Entities.SaleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CRIADO_EM");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("ID_PRODUTO");

                    b.Property<string>("ProductName")
                        .HasColumnType("longtext")
                        .HasColumnName("NOME_PRODUTO");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("PRECO_PRODUTO");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int")
                        .HasColumnName("QUANTIDADE_VENDIDA");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("ATUALIZADO_EM");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("VENDAS");
                });

            modelBuilder.Entity("DomvsUnitTestPoc.Infrastructure.Entities.SaleEntity", b =>
                {
                    b.HasOne("DomvsUnitTestPoc.Infrastructure.Entities.ProductEntity", "Product")
                        .WithMany("Sales")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DomvsUnitTestPoc.Infrastructure.Entities.ProductEntity", b =>
                {
                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
