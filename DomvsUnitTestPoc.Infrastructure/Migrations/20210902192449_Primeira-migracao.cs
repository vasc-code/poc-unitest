using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomvsUnitTestPoc.Infrastructure.Migrations
{
    public partial class Primeiramigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PRECO = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ATUALIZADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VENDAS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME_PRODUTO = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ID_PRODUTO = table.Column<long>(type: "bigint", nullable: false),
                    PRECO_PRODUTO = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QUANTIDADE_VENDIDA = table.Column<int>(type: "int", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ATUALIZADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENDAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VENDAS_PRODUTO_ID_PRODUTO",
                        column: x => x.ID_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "PRODUTO",
                columns: new[] { "ID", "CRIADO_EM", "NOME", "PRECO", "QUANTIDADE", "ATUALIZADO_EM" },
                values: new object[] { 1L, new DateTime(2021, 9, 2, 16, 24, 48, 990, DateTimeKind.Local).AddTicks(8873), "Lápis Faber Castel", 1.29m, 100, null });

            migrationBuilder.CreateIndex(
                name: "IX_VENDAS_ID_PRODUTO",
                table: "VENDAS",
                column: "ID_PRODUTO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VENDAS");

            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
